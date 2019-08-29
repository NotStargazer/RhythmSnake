using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    [SerializeField] AudioClip beat;
    [SerializeField] bool enableMetronome;

    public float bpm;

    int beatNumber;
    float timeBeforeNextBeat;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        bpm = 1 / bpm * 60f;
    }

    private void Update()
    {
        if (!enableMetronome)
        {
            return;
        }

        if (timeBeforeNextBeat <= 0)
        {
            timeBeforeNextBeat = bpm;
            if (beatNumber == 0)
            {
                source.pitch *= 2;
                source.PlayOneShot(beat);
                source.pitch /= 2;
            }
            else
            {
                source.PlayOneShot(beat);
            }

            beatNumber++;

            if (beatNumber > 3)
            {
                beatNumber = 0;
            }
        }

        timeBeforeNextBeat -= Time.deltaTime;
    }

    public void MetronomeUpdate()
    {

    }
}
