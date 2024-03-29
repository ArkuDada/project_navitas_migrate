using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefualtSetting : MonoBehaviour
{
    [SerializeField] private List<AudioSource> sources;

    void Start()
    {
        LevelTracker.Instance.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        LevelTracker.Instance.GetComponent<AudioSource>().volume = volume;
        foreach (var s in sources)
        {
            s.volume = volume;
        }
    }
}