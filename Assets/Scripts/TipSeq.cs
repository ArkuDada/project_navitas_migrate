using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipSeq : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] tipPic;

    public enum TipStage
    {
        walk = 0,
        jump,
        dash,
        warn,
        back,
        chain,
        back2,
        land,
        compass,
        exit
    };

    private TipStage currentTip;

    private static TipSeq _instance;

    public static TipSeq Instance
    {
        get => _instance;
    }


// Start is called before the first frame update
    void Start()
    {
        _instance = this;
        currentTip = TipStage.walk;
    }

// Update is called once per frame
    void Update()
    {
        img.sprite = tipPic[(int)currentTip];
    }

    public void UpdateTip(TipStage tip)
    {
        if (tip == currentTip)
        {
            print(currentTip);
            currentTip++;
            if (currentTip == TipStage.back)
            {
                Time.timeScale = 0;
            }

            if (currentTip == TipStage.chain)
            {
                Time.timeScale = 1;
            }
        }
    }
}