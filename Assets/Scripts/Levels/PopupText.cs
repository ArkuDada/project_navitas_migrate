using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private PlayerController pc;
    void Start()
    {
        pc = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = pc.transform.position;
        transform.LookAt(pos);
    }

    private void OnTriggerStay(Collider other)
    {
        text.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }
}
