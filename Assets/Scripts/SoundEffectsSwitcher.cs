using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsSwitcher : MonoBehaviour {

    public AudioSource mainSongAudioSource;
    public AudioSource realSongAudioSource;

    public AudioSource throwAudioSource;
    public AudioClip[] throwClip = new AudioClip[8];

    private int clipToPlay = 0;
    private int changesCount = 0;


    private void Start()
    {
        mainSongAudioSource.volume = 1;
        mainSongAudioSource.Play();

        realSongAudioSource.volume = 0;
        realSongAudioSource.Play();

        InvokeRepeating("RealityCheck", 0.1f, 0.1f);
    }

    private void Update()
    {
        AnalizePosition();
    }

    public void AnalizePosition()
    {
        float currentSongTime = mainSongAudioSource.time;

        if (currentSongTime < 11)
        {
            clipToPlay = 0;
            changesCount = 0;
            return;
        }

        if (11 + (3 * changesCount) < currentSongTime)
        {
            changesCount++;

            clipToPlay = (changesCount % 8) - 1;        
        }

    }

    private void ChangeClip()
    {
        //throwAudioSource.clip = throwClip[clipToPlay];
    }

    public void PlayThrow()
    {
        Debug.Log("Play this clip: " + clipToPlay + " at change " + changesCount);

        //throwAudioSource.PlayOneShot(throwClip[clipToPlay]);
    }

    private void RealityCheck()
    {
        GameObject[] existent = GameObject.FindGameObjectsWithTag("Reality");
        if (existent.Length > 0)
        {

            RealityComes();
        }
        else
        {
            RealityFades();
        }
    }

    private void RealityComes()
    {
        if (mainSongAudioSource.volume > 0.2f)
            mainSongAudioSource.volume -= Time.deltaTime * 3;

        if (realSongAudioSource.volume < 0.6f)
            realSongAudioSource.volume += Time.deltaTime * 3;
    }

    private void RealityFades()
    {
        if (mainSongAudioSource.volume < 1)
            mainSongAudioSource.volume += Time.deltaTime * 3;

        if (realSongAudioSource.volume > 0)
            realSongAudioSource.volume -= Time.deltaTime * 3;
    }

    public void PlayerDeath()
    {
        mainSongAudioSource.volume -= Time.deltaTime;

        realSongAudioSource.volume += Time.deltaTime;
        if (realSongAudioSource.volume > 0.5f)
        {
            realSongAudioSource.volume = 0.5f;
        }
    }

}
