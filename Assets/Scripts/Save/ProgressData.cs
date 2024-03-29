using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProgressData
{
    public List<bool> timePass = new List<bool>();
    public List<bool> dashPass = new List<bool>();
    public List<bool> starPass = new List<bool>();
    public List<bool> levelPass = new List<bool>();

    public ProgressData(LevelCondition[] lp)
    {
        for (int i = 0; i < lp.Length; i++)
        {
            timePass.Add(lp[i].timePass); 
            dashPass.Add(lp[i].dashPass); 
            starPass.Add(lp[i].starPass); 
            levelPass.Add(lp[i].levelPass); 
        }
    }
}