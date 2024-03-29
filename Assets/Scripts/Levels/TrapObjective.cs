using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObjective : MonoBehaviour
{
    [SerializeField] private List<FlagilePlatform> fplist = new List<FlagilePlatform>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var fp in fplist)
            {
                fp.Disappear();
            }
        }
    }
}