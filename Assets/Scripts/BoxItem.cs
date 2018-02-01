using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxItem : Item {

    Collider2D myCollider;

    [HideInInspector]
    public float mass;
    [HideInInspector]
    public int itemType = 3;

    protected override void Start()
    {
        myCollider = GetComponent<Collider2D>();

        nowhere = new Vector3(-900, -900, -900);

        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(true);
    }

    public override void FixedUpdate()
    {
        //Don't add anything here
    }

    public void GetBox()
    {
        mass = GetComponent<Rigidbody2D>().mass;
        transform.position = nowhere;
        gameObject.SetActive(false);
    }

    public override void Throw()
    {
        rb2d.gravityScale = 1;
        base.Throw();
    }
}
