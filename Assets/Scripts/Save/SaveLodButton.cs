using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLodButton : MonoBehaviour
{
    public int id;

    public bool isSave;

    private Image img;
    private LevelTracker lt;
    private Button button;

    void Start()
    {
        lt = LevelTracker.Instance;

        img = GetComponent<Image>();

        button = GetComponent<Button>();

        button.onClick.AddListener(() => setUp());
    }

    private void Update()
    {
        img.color = id == lt.saveID ? new Color(0.125f, 0.85f, 0.4f) : new Color(1, 1, 1);
    }

    void Destroy()
    {
        button.onClick.RemoveListener(() => setUp());
    }

    private void setUp()
    {
        lt.ChangeProfile(id);
        if (isSave)
        {
            lt.SaveProgress();
        }
        else
        {
            lt.LoadProgress();
        }
    }
}