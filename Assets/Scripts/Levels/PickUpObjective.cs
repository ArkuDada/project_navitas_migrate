using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjective : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Goal goal;
    [SerializeField] private GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goal.PickupUpdate(this);
            gameObject.SetActive(false);
            
            Compass.Instance.RemoveMarker(GetComponent<Marker>());

            if (TipSeq.Instance)
            {
                TipSeq.Instance.UpdateTip(TipSeq.TipStage.compass);
            }
            
            var p = Instantiate(particle,transform.position,Quaternion.identity);
            Destroy(p,1.5f);
        }
    }
}
