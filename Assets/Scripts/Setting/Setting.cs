using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public string valueName;
    public float valueDefault;
    public float valueStart;
    public float valueStop;
    private float value;
    [SerializeField] private Scrollbar _srollbar;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        if (PlayerPrefs.HasKey(valueName))
        {
            value = PlayerPrefs.GetFloat(valueName);
        }
        else
        {
            value = valueDefault;
            PlayerPrefs.SetFloat(valueName, value);
        }
        print((value - valueStart) / (valueStop - valueStart));
        _srollbar.value = (value - valueStart) / (valueStop - valueStart);
        text.text = value.ToString();
    }

    public void ChangeSetting(float amount)
    {
        value = (float)Math.Round((valueStart + (amount * (valueStop - valueStart))), 2);
        PlayerPrefs.SetFloat(valueName, value);
        text.text = value.ToString();

        PlayerPrefs.Save();
    }
}