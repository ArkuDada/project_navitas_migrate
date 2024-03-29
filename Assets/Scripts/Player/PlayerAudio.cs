using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Source")] [SerializeField]
    private AudioSource walkSource;

    [SerializeField] private AudioSource dashSource;
    [SerializeField] private AudioSource warnSource;
    [SerializeField] private AudioSource rechargeSource;

    [Header("Audio Clips")] [SerializeField]
    private List<AudioClip> walkingSounds;

    [SerializeField] private List<AudioClip> walkingSoundsStone;

    [SerializeField] private AudioClip dashingSound;
    [SerializeField] private AudioClip invertDashSound;

    public bool walking = false;
    public bool onStone = false;

    void Start()
    {
        walkSource.clip = walkingSounds[Random.Range(0, walkingSounds.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (walking && Time.timeScale != 0)
        {
            if (!walkSource.isPlaying)
            {
                if (onStone)
                {
                    walkSource.clip = walkingSoundsStone[Random.Range(0, walkingSoundsStone.Count)];
                }
                else
                {
                    walkSource.clip = walkingSounds[Random.Range(0, walkingSounds.Count)];
                }

                walkSource.Play();
            }
        }
        else
        {
            if (!walkSource.isPlaying)
            {
                walkSource.Pause();
            }
        }
    }

    public void PlayDash(int combo)
    {
        dashSource.clip = dashingSound;
        dashSource.pitch = 1 + (0.15f * (combo - 1));
        dashSource.Play();
    }

    public void PlayCharge()
    {
        dashSource.clip = invertDashSound;
        dashSource.pitch = 1;
        dashSource.Play();
    }

    public void PlayWarn()
    {
        warnSource.Play();
    }

    public void PlayRecharge()
    {
        if (!rechargeSource.isPlaying)
        {
            rechargeSource.Play();
        }
    }
}