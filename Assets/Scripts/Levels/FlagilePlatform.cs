using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagilePlatform : MonoBehaviour
{
    public float delay = 3f;
    [SerializeField] private GameObject platform;
    [SerializeField] private bool destroyOnContact;
    public bool playerContact = false;
    private bool startDisappear = false;

    private void Update()
    {
        if (playerContact && !startDisappear && destroyOnContact)
        {
            startDisappear = true;
            Invoke(nameof(Disappear), delay);
        }
    }

    public void Disappear()
    {
        platform.SetActive(false);
    }

    public void Reappear()
    {
        CancelInvoke(nameof(Disappear));
        if (platform)
        {
            platform.SetActive(true);
        }

        startDisappear = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = false;
        }
    }
}