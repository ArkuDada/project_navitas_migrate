using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    
    public bool camMode;
    public GameObject flycam;
    
    public bool clear;
    private LevelTracker lt;

    [SerializeField] private EnergyController ec;
    [SerializeField] private RTATimer timer;

    [SerializeField] private List<GameObject> pickUpSetup;
    private List<PickUpObjective> pickUpTrack = new List<PickUpObjective>();
    private int objMaxAmount = 0;

    private void Start()
    {
        clear = false;
        PlayerController.Instance.goal = this;
        lt = LevelTracker.Instance;
        ec = PlayerController.Instance.gameObject.GetComponent<EnergyController>();
        ResetStage();

        if (camMode)
        {
            GameObject.FindWithTag("Player").gameObject.SetActive(false);
            Instantiate(flycam, Vector3.zero, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clear = true;
            timer.SetActiveTimer(false);
            lt.UpdateProgress(SceneManager.GetActiveScene().name, GetProgress());
        }
    }

    public void ResetStage()
    {
        clear = false;
        print(timer.rtaTime);
        pickUpTrack.Clear();
        foreach (var p in pickUpSetup)
        {
            if (p.GetComponent<PickUpObjective>() != null)
            {
                pickUpTrack.Add(p.GetComponent<PickUpObjective>());
            }

            p.SetActive(true);
        }

        if (GetComponent<SpecialPlatformManager>())
        {
            GetComponent<SpecialPlatformManager>().ResetPlatforms();
        }
        
        objMaxAmount = pickUpTrack.Count;
    }

    public void PickupUpdate(PickUpObjective pickUp)
    {
        pickUpTrack.Remove(pickUp);
    }

    public LevelProgress GetProgress()
    {
        LevelProgress lp;
        lp.curTime = timer.rtaTime;
        lp.curDashCount = ec.TotalDashUsed;
        lp.curStarCount = (objMaxAmount - pickUpTrack.Count);
        return lp;
    }
    
}

public struct LevelProgress
{
    public float curTime;
    public int curDashCount;
    public int curStarCount;
}