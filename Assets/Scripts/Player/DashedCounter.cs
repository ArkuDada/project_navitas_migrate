using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DashedCounter : MonoBehaviour
{
    [SerializeField] private EnergyController ec;
    [SerializeField] private TextMeshProUGUI dashcountText;

    // Update is called once per frame
    void Update()
    {
        dashcountText.text = ec.TotalDashUsed.ToString();
    }
}
