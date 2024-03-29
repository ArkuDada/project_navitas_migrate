using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    private PlayerController pc;
    void Start()
    {
        pc = PlayerController.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             pc.gameObject.GetComponent<EnergyController>().debug = false;
        }
       
    }
}
