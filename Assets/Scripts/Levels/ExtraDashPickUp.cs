using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDashPickUp : MonoBehaviour
{
    private EnergyController ec;

    void Start()
    {
        ec = PlayerController.Instance.gameObject.GetComponent<EnergyController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ec.ExtraCharge();
            gameObject.SetActive(false);
        }
    }
}