using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

    SpriteMask sprtMask;
    float cutoffMin = 0.98f, cutoffMax = 1;
    Vector2 maxRadius;
    float duration = 5;

    public void CreateSelf(Vector2 position, float itemVelocity)
    {
        transform.position = position;
        maxRadius = new Vector2(itemVelocity, itemVelocity);
        transform.localScale = Vector2.zero;
    }

    private void Grow()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, maxRadius, Time.deltaTime / duration);
    }
}
