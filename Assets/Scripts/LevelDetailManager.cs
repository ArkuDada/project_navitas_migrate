using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelDetailManager : MonoBehaviour
{
    [Header("Preview")]
    [SerializeField] private Image previewImg;
    
    [Header("Time")] [SerializeField] private Image timeImg;
    [SerializeField] private TextMeshProUGUI timeText;
    [Header("Collect")] [SerializeField] private Image collectImg;
    [SerializeField] private TextMeshProUGUI collectText;
    [Header("Dash")] [SerializeField] private Image dashImg;
    [SerializeField] private TextMeshProUGUI dashText;

    private GameObject clickedButton;
    private LevelTracker lt;
    private LevelCondition lc;
    
    private string currentPreview;

    private void Start()
    {
        lt = LevelTracker.Instance;
    }

    public void PreviewLevel()
    {
        clickedButton = EventSystem.current.currentSelectedGameObject;
        currentPreview = clickedButton.name;
        
        print(currentPreview);
        lc = lt.GetCondition(currentPreview);

        previewImg.sprite = clickedButton.GetComponent<Image>().sprite;
        
        timeText.text = lc.timeLimit + "s";
        dashText.text = lc.dashCount.ToString();
        collectText.text = lc.starCount.ToString();

        if (lc.timePass)
        {
            timeImg.color = Color.green;
        }
        else
        {
            timeImg.color = Color.white;
        }

        if (lc.dashPass)
        {
            dashImg.color = Color.green;
        }
        else
        {
            dashImg.color = Color.white;
        }

        if (lc.starPass)
        {
            collectImg.color = Color.green;
        }
        else
        {
            collectImg.color = Color.white;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(currentPreview);
    }

}
