﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFeedback : MonoBehaviour {

    [SerializeField]
    private float cutoffMin = 0.99f, cutoffMax = 1;

    SpriteMask sprtMask;
    Vector2 minRadius;
    Vector2 originalRadius;
    float duration = 1;

    bool shrinking = false;


    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;

    private void Start()
    {
        originalRadius = transform.localScale;

        vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    private void FixedUpdate()
    {
        if (shrinking)
        {
            ReachRadius();

            if (noise.m_AmplitudeGain > 0)
            {
                noise.m_AmplitudeGain -= Time.deltaTime * 2;
                noise.m_FrequencyGain -= Time.deltaTime * 2;
            }
            else
            {
                noise.m_AmplitudeGain = 0;
                noise.m_FrequencyGain = 0;
            }
        }
    }

    public void ReachRadius(int playerHp, int playerMaxHp)
    {
        sprtMask = GetComponent<SpriteMask>();

        if (playerHp < playerMaxHp)
        {
            duration = 2;

            float newRadius = transform.localScale.x - ((transform.localScale.x/playerMaxHp) * (playerMaxHp - playerHp));

            minRadius = new Vector2(newRadius, newRadius);

            if (playerHp == 0)
            {
                minRadius = Vector2.zero;
                duration = 5;
            }

        }
        else
        {
            minRadius = originalRadius;
        }

        shrinking = true;
        CancelInvoke();
        InvokeRepeating("Animate", 0.2f, 0.2f);
    }

    private void ReachRadius()
    {
        if (Mathf.Abs(minRadius.x - transform.localScale.x) > 0.1f || minRadius == Vector2.zero)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, minRadius, Time.deltaTime * duration);
        }

        if (Mathf.Abs(originalRadius.x - transform.localScale.x) < 0.2 && Mathf.Abs(originalRadius.x - minRadius.x) < 0.2f)
        {
            CancelInvoke();
            shrinking = false;
        }
    }


    private void Animate()
    {
        sprtMask.alphaCutoff = Random.Range(cutoffMin, cutoffMax);
    }
}
