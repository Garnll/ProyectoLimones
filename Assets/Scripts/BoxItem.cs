using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxItem : Item {

    Collider2D myCollider;

    [HideInInspector]
    public float mass;

    protected override void Start()
    {
        myCollider = GetComponent<Collider2D>();

        nowhere = new Vector3(-900, -900, -900);

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameObject.SetActive(true);
    }

    public override void FixedUpdate()
    {
        //Here goes nothing
    }

    public void GetBox()
    {
        mass = GetComponent<Rigidbody2D>().mass;
        transform.position = nowhere;
        gameObject.SetActive(false);
    }

    public override void Throw()
    {
        base.Throw();
    }
}
