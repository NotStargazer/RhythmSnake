using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Metronome : MonoBehaviour
{
    [SerializeField] AudioClip beat;
    [SerializeField] Snake snake;
    [SerializeField] Grid grid;
    [SerializeField] bool enableMetronomeSound;

    public float bpm;
    float speed;

    int beatNumber;
    float timeBeforeNextBeat;
    AudioSource source;

    private void Awake()
    {
        Assert.IsNotNull(snake, "Snake not attatched to metronome ticker");
        source = GetComponent<AudioSource>();
        speed = 1 / bpm * 60f;
    }

    private void Update()
    {
        if (timeBeforeNextBeat <= 0)
        {
            timeBeforeNextBeat = speed;
            MetronomeUpdate();
            if (beatNumber == 0)
            {
                source.pitch *= 2;
                if (enableMetronomeSound)
                {
                    source.PlayOneShot(beat);
                }
                source.pitch /= 2;
            }
            else
            {
                if (enableMetronomeSound)
                {
                    source.PlayOneShot(beat);
                }
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
        if (!snake)
        {
            return;
        }
        if (!snake.MoveSnake())
        {
            Destroy(snake.gameObject);
        }
        foreach (Transform child in snake.transform)
        {
            if (child.localPosition.x >= grid.width || child.localPosition.x < 0 || child.localPosition.y >= grid.height || child.localPosition.y < 0)
            {
                Destroy(snake.gameObject);
            }
            if (child.position == grid.transform.GetChild(1).position)
            {
                Destroy(grid.transform.GetChild(1).gameObject);
                grid.SpawnObjective(snake.transform);
                snake.Grow();
                UpdateBpm(10);
            }
        }
    }

    private void UpdateBpm(int amount)
    {
        bpm += amount;
        speed = 1 / bpm * 60f;
    }
}
