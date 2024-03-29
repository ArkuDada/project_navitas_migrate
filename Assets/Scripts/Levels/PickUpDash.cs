using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDash : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var ec = other.GetComponent<EnergyController>();
            if (ec.ExtraCharge())
            {
                gameObject.SetActive(false);
                
                var p = Instantiate(particle,transform.position,Quaternion.identity);
                Destroy(p,1.5f);
                
            }
        }
    }
}
