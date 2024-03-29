using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    public Sprite icon;
    public Image image;
    public Boolean affectByScale;
    public Vector2 position
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }
    
}