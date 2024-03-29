using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RTATimer : MonoBehaviour
{
    private static RTATimer _instance;

    public static RTATimer Instance
    {
        get => _instance;
    }

    // Start is called before the first frame update
    private float timeRemaining;

    public float rtaTime
    {
        get => timeRemaining;
    }

    private bool timerOn = true;

    public bool TimerOn
    {
        set => timerOn = value;
    }

    [SerializeField] private TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        RestartClock();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            timeRemaining += Time.deltaTime;
            DisplayTime(timeRemaining);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;

        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void RestartClock()
    {
        timeRemaining = 0;
        timerOn = true;
    }

    public void SetActiveTimer(bool status)
    {
        Debug.Log(status);
        timerOn = status;
    }
}