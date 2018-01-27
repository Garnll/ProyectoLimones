using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float thowForce = 700f;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject fader;
    [SerializeField]
    private float radiusMultiplicator = 2;
    [SerializeField]
    private float radiusMinimum = 10;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    [HideInInspector]
    public bool onUse = false;

    private Vector3 nowhere;

    // Use this for initialization
    void Start()
    {
        nowhere = new Vector3(-900, -900, -900);
        transform.position = nowhere;

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (rb2d.velocity.magnitude <= Mathf.Epsilon && onUse)
        {
            Dissapear();
        }
    }

    /// <summary>
    /// Activa su gameobject, cambia su transform al de su spawnpoint, y le da una fuerza.
    /// </summary>
    public void Throw()
    {
        gameObject.SetActive(true);
        onUse = true;

        Color alpha = spriteRenderer.color;
        alpha.a = 1;
        spriteRenderer.color = alpha;

        transform.position = spawnPoint.transform.position;

        rb2d.AddForce(spawnPoint.transform.right * thowForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float radiusFromVelocity = rb2d.velocity.sqrMagnitude * radiusMultiplicator;
        Debug.Log(radiusFromVelocity);
        if (radiusFromVelocity < radiusMinimum)
        {
            radiusFromVelocity = Random.Range(radiusMinimum - 1, radiusMinimum + 1);
        }

        GameObject newFader = Instantiate(fader, transform.position, transform.rotation) as GameObject;
        newFader.GetComponent<Fader>().CreateSelf(transform.position, radiusFromVelocity);

    }

    private void Dissapear()
    {
        Color alpha = spriteRenderer.color;
        float time = 0.5f;

        alpha.a -= Time.deltaTime * time;
        spriteRenderer.color = alpha;

        if (alpha.a <= 0)
        {
            onUse = false;
            transform.position = nowhere;
            gameObject.SetActive(false);
        }
    }
}
