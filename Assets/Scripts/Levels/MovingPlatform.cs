using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private GameObject platform;
    [SerializeField] private List<PlatformWaypoint> waypoints;

    private PlayerController pc;
    private BoxCollider bc;


    private Transform platformPos;
    private PlatformWaypoint start;
    private PlatformWaypoint stop;
    private int current;

    private float startTime;
    private float moveDistance;

    public bool playerContact = false;

    private void Start()
    {
        pc = PlayerController.Instance;
        bc = GetComponent<BoxCollider>();

        current = 0;
        SetDirection(0);
    }

    void Update()
    {
        if (platform.transform.position.Equals(stop.position))
        {
           
            SetDirection(current++);
            
        }
        else
        {
            bc.center = platform.transform.localPosition;

            float distTraveled = (Time.time - startTime) * speed;

            float moveAmount = distTraveled / moveDistance;

            platform.transform.position = Vector3.Lerp(start.position, stop.position, moveAmount);
            
            if (playerContact)
            {
                Vector3 force = (stop.position - start.position).normalized;
                pc.AttachPlatform(force * speed);
            }
        }
    }

    public void SetDirection(int index)
    {
        startTime = Time.time;
        start = waypoints[current % waypoints.Count];
        stop = waypoints[(current + 1) % waypoints.Count];
        moveDistance = Vector3.Distance(start.position, stop.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerContact = false;
        }
    }
}