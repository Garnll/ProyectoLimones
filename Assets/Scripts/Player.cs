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

    private int hp;
    private float currentVelocity;
    private bool itemOnHand = false;
    private bool hurt = false;


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
        if (hurt)
        {
            Invulnerable();
        }

        Vector2 move = Vector2.zero;

        if (Input.GetButtonDown("Fire1"))
        {
            if (!itemOnHand)
            {
                PickUp();
            }
        }
        else if (Input.GetButtonUp("Fire1") && itemOnHand)
        {
            Throw();
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

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / currentVelocity);
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
            if (spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        targetVelocity = move * currentVelocity;
    }

    private void PickUp()
    {
        if (itemOnHand)
            return;

        int layerMask = 1 << LayerMask.NameToLayer("Items");
        float circleRadius = 2;

        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, circleRadius, layerMask);
        if (itemCollider != null)
        {
            //Hacer cosas referentes a coger el objeto, enviar a que el item haga algo
            itemOnHand = true;

            //Aquí hará la animación de coger el item y quedarse con él
        }
        else
        {
            itemOnHand = false;
        }
    }

    private void Throw()
    {
        if (itemOnHand)
            itemOnHand = false;
        else
            return;

        //Aqui hará la animación de tirar el item
        currentVelocity = maxVelocity;
    }

    /// <summary>
    /// Hurts the player once, leaving him in a state of invulnerability for 1 second.
    /// </summary>
    public void PlayerHurt()
    {
        if (!hurt)
        {
            CancelInvoke();
            hurt = true;
            hp--;

            Invoke("InvulnerabilityOff", 1);
            Invoke("RegainHp", 2f);
        }
    }

    private void PlayerDie()
    {
        //Aqui el jugador muere
    }

    private void Invulnerable()
    {
        Color alpha = spriteRenderer.color;
        alpha.a = Mathf.PingPong(alpha.a, 1);

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
        hp++;

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
