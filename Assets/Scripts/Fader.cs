using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

    [SerializeField]
    private float cutoffMin = 0.99f, cutoffMax = 1;

    SpriteMask sprtMask;
    Vector2 maxRadius;
    float duration = 2;

    bool alreadyGrew = false;

    private void FixedUpdate()
    {
        Grow();
    }

    public void CreateSelf(Vector2 position, float itemVelocity)
    {
        sprtMask = GetComponent<SpriteMask>();
        transform.position = position;
        maxRadius = new Vector2(itemVelocity, itemVelocity);
        transform.localScale = Vector2.zero;

        InvokeRepeating("Animate", Time.deltaTime, 0.16f);
        Destroy(gameObject, (duration * 2) + (duration * 0.5f));
    }

    private void Grow()
    {
        if (Mathf.Abs(maxRadius.x - transform.localScale.x) > 0.1f && !alreadyGrew)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, maxRadius, Time.deltaTime * duration);
        }
        else
        {
            alreadyGrew = true;
            Shrink();
        }
    }
    
    private void Shrink()
    {
        if (transform.localScale.x > Mathf.Epsilon)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * duration);
        }
        else
        {
            Dissapear();
        }
    }

    private void Dissapear()
    {
        Destroy(this);
    }

    private void Animate()
    {
        sprtMask.alphaCutoff = Random.Range(cutoffMin, cutoffMax);
    }
}
