using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public float thowForce = 0.02f;
    [SerializeField]
    private GameObject spawnPoint;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    [HideInInspector]
    public bool onUse = false;

    private Vector3 nowhere;

    float maxProducedRadius = 2;

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

        rb2d.AddForce(transform.right * thowForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("golpeo aw");
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
