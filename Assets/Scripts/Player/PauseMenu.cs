using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    [Header("Top")] [SerializeField]private TextMeshProUGUI pauseText;
    [SerializeField] private Button resumeB;
    [SerializeField] private Button nextB;
    
    [Header("Time")] [SerializeField] private Image timeImg;
    [SerializeField] private TextMeshProUGUI timeText;
    [Header("Collect")] [SerializeField] private Image collectImg;
    [SerializeField] private TextMeshProUGUI collectText;
    [Header("Dash")] [SerializeField] private Image dashImg;
    [SerializeField] private TextMeshProUGUI dashText;

    private Goal goal;
    private LevelTracker lt;
    private LevelCondition lc;

    private void Start()
    {
        goal = PlayerController.Instance.goal;
        lt = LevelTracker.Instance;
        lc = lt.GetCondition(SceneManager.GetActiveScene().name);
        
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (goal.clear)
        {
            Victory();
        }
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        lc = lt.GetCondition(SceneManager.GetActiveScene().name);
        UpdateProgressUI();

        pauseText.text = "Pause";
        resumeB.gameObject.SetActive(true);
        nextB.gameObject.SetActive(false);
        
    }

    private void Victory()
    {
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        lc = lt.GetCondition(SceneManager.GetActiveScene().name);
        UpdateProgressUI();
        
        pauseText.text = "Log clear!";
        resumeB.gameObject.SetActive(false);
        nextB.gameObject.SetActive(true);
        
    }

    private void UpdateProgressUI()
    {
        LevelProgress lp = goal.GetProgress();

        timeText.text = lc.timeLimit + "s";
        dashText.text = lc.dashCount.ToString();
        collectText.text = lc.starCount.ToString();

        if (lc.timePass)
        {
            timeImg.color = Color.green;
        }
        else if (lc.timeLimit > lp.curTime)
        {
            timeImg.color = Color.yellow;
        }
        else
        {
            timeImg.color = Color.red;
        }

        if (lc.dashPass)
        {
            dashImg.color = Color.green;
        }
        else if (lc.dashCount >= lp.curDashCount)
        {
            dashImg.color = Color.yellow;
        }
        else
        {
            dashImg.color = Color.red;
        }

        if (lc.starPass)
        {
            collectImg.color = Color.green;
        }
        else if (lp.curStarCount == lc.starCount)
        {
            collectImg.color = Color.yellow;
        }
        else
        {
            collectImg.color = Color.red;
        }
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        PlayerController.Instance.Respawn();
    }

    public void Menu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}