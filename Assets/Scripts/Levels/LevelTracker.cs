using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    private static LevelTracker _instance;

    public int saveID = 1;

    [SerializeField] private List<string> sceneNames;
    [SerializeField] private LevelCondition[] levelConditions;

    public static LevelTracker Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public LevelCondition GetCondition(string sceneName)
    {
        return levelConditions[sceneNames.IndexOf(sceneName)];
    }

    public void UpdateProgress(string sceneName, LevelProgress lp)
    {
        var lc = levelConditions[sceneNames.IndexOf(sceneName)];

        lc.timePass = (lp.curTime <= lc.timeLimit) || lc.timePass;

        lc.dashPass = (lp.curDashCount <= lc.dashCount) || lc.dashPass;

        lc.starPass = (lp.curStarCount == lc.starCount) || lc.starPass;

        lc.levelPass = true;

        levelConditions[sceneNames.IndexOf(sceneName)] = lc;
        print(lc.timePass + " " + lc.dashPass + " " + lc.starPass);
        
        SaveProgress();
    }

    public void ChangeProfile(int id)
    {
        saveID = id;
    }

    public void SaveProgress()
    {
        SaveSystem.SaveGame(levelConditions, saveID);
    }

    public void LoadProgress()
    {
        var loadLc = SaveSystem.LoadGame(saveID);
        for (int i = 0; i < levelConditions.Length; i++)
        {
            levelConditions[i].dashPass = loadLc.dashPass[i];
            levelConditions[i].timePass = loadLc.timePass[i];
            levelConditions[i].starPass = loadLc.starPass[i];
            levelConditions[i].levelPass = loadLc.levelPass[i];
        }
    }
}

[Serializable]
public struct LevelCondition
{
    public float timeLimit;
    public int dashCount;
    public int starCount;
    public bool timePass;
    public bool dashPass;
    public bool starPass;
    public bool levelPass;
}