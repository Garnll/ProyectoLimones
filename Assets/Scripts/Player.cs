using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PhysicsObject {

    [SerializeField]
    private int maxHp = 3;
    [SerializeField]
    private float maxVelocity = 7;
    [SerializeField]
    private float jumpTakeOffSpeed = 7;
    [SerializeField]
    private LifeFeedback lifeFeedback;

    private int hp;
    private float currentVelocity;
    private bool itemOnHand = false;
    private bool hurt = false;
    private bool dead = false;

    private ItemPool currentPoolUsed;


    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        currentVelocity = maxVelocity;
        hp = maxHp;
        itemOnHand = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        if (dead)
        {
            return;
        }

        if (hurt)
        {
            Color alpha = spriteRenderer.color;
            alpha.a = Mathf.PingPong(Time.time * 5, 1);

            Invulnerable(alpha);
        }

        Vector2 move = Vector2.zero;

        if (Input.GetButtonDown("Fire1") && itemOnHand)
        {
            Throw();            
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!itemOnHand)
            {
                PickUp();
            }
        }


        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        Move(move);

        if (!itemOnHand)
        {
            animator.SetBool("grounded", grounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / currentVelocity);
        }
    }

    private void Jump()
    {
        velocity.y = jumpTakeOffSpeed;
    }

    private void Move(Vector2 move)
    {
        if (currentVelocity > maxVelocity)
        {
            currentVelocity = maxVelocity;
        }

        if (move.x > 0.01f)
        {
            if (transform.rotation.y >= 1 || transform.rotation.y <= -1)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
        }
        else if (move.x < -0.01f)
        {
            if (transform.rotation.y < 1 && transform.rotation.y > -1)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
        }

        targetVelocity = move * currentVelocity;
    }

    /// <summary>
    /// Crea un OverlapCircle que busca objetos con el layer "Items". 
    /// Al encontrarlo lo guarda en una referencia y coge las propiedades de su Item correspondiente.
    /// </summary>
    private void PickUp()
    {
        if (itemOnHand)
            return;

        int layerMask = 1 << LayerMask.NameToLayer("Items");
        float circleRadius = 1;

        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, circleRadius, layerMask);
        if (itemCollider != null)
        {
            currentPoolUsed = itemCollider.GetComponent<ItemPool>();
            if (!currentPoolUsed.ItemAvailable())
            {
                itemOnHand = false;
                return;
            }

            currentPoolUsed.GetItemProperties();

            currentVelocity = maxVelocity - currentPoolUsed.mass/2;
            itemOnHand = true;

            //Aquí hará la animación de coger el item y quedarse con él
        }
        else
        {
            itemOnHand = false;
        }
    }

    /// <summary>
    /// Le dice a la currentItemPool que haga la función ThowItem.
    /// </summary>
    private void Throw()
    {
        if (!itemOnHand)
            return;

        //Aqui hará la animación de tirar el item

        currentPoolUsed.ThowItem();

        currentVelocity = maxVelocity;

        itemOnHand = false;
    }

    /// <summary>
    /// Hurts the player once, leaving him in a state of invulnerability for 1 second.
    /// </summary>
    public void PlayerHurt()
    {
        if (!hurt && !dead)
        {
            CancelInvoke();
            hurt = true;
            hp--;

            lifeFeedback.ReachRadius(hp, maxHp);

            if (hp <= 0)
            {
                PlayerDie();
            }

            Invoke("InvulnerabilityOff", 1);
            Invoke("RegainHp", 2f);
        }
    }

    private void PlayerDie()
    {
        CancelInvoke();
        dead = true;

        //Aqui el jugador muere
        Debug.Log("Player has died D:");
    }

    private void Invulnerable(Color alpha)
    {
        spriteRenderer.color = alpha;
    }

    private void InvulnerabilityOff()
    {
        Color alpha = spriteRenderer.color;
        alpha.a = 1;

        spriteRenderer.color = alpha;

        hurt = false;
    }

    private void RegainHp()
    {
        if (dead)
            return;

        hp++;

        lifeFeedback.ReachRadius(hp, maxHp);

        if (hp >= maxHp)
        {
            hp = maxHp;
        }
        else
        {
            Invoke("RegainHp", 0.5f);
        }
    }




}
