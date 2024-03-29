using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashParticle : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - Player.position;
        var vel = ps.velocityOverLifetime;
        var minMaxCurve = vel.x;
        minMaxCurve.constant =  direction.x;
    }
}
