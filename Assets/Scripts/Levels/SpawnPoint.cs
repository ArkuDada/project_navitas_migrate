using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Vector3 pos;
    private void Start()
    {
        PlayerController.Instance.spawnPoint = this;
        pos = transform.localPosition;
    }
    

    public Vector3 RespawnPosition()
    {
        return pos;
    }

    private void OnTriggerStay(Collider other)
    {
        RTATimer.Instance.RestartClock();
    }
}
