using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyController : MonoBehaviour
{
    [Header("Newton Settings")] public int charge = 0;
    public int maxCharge = 3;
    private int usedCharge = 0;
    private float newtonDecay = 1f;
    public float newtonSpeed = 2f;
    public float dashMultiplier = 0.5f;
    public float dashTime = 1f;
    private Vector3 newtonVelocity;

    [Header("Crosshair CD")] [SerializeField]
    private Image crosshair;

    private float cooldownTimer;

    public float implodeValue = 1f;

    private int totalDashUsed = 0;

  

    public int TotalDashUsed
    {
        get => totalDashUsed;
        set => totalDashUsed = value;
    }

    [Header("DashEffect")] [SerializeField]
    private ParticleSystem dashForwardPS;

    [SerializeField] private ParticleSystem dashBackwardPS;

    [Header("Settings")] [SerializeField] private Transform look;
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private GameObject[] bars;
    [SerializeField] private Slider implodeBar;

    public bool debug = false;

    private PlayerController pc;
    private CharacterController cc;
    private PlayerAudio pa;

    private bool stableState = true;

    private bool imploded = false;
    private bool warned = false;

    public bool IsImploded
    {
        set => imploded = value;
        get => imploded;
    }

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
        pa = GetComponent<PlayerAudio>();

        charge = maxCharge;

        UpdateBarUI();
    }

    private void Update()
    {
        if (!stableState && !imploded && !debug)
        {
            if (charge < 4)
            {
                implodeValue -= Time.deltaTime / maxCharge * (1.15f - charge / maxCharge);

                cooldownTimer += Time.deltaTime;
                crosshair.fillAmount = cooldownTimer / dashTime;

                implodeBar.value = implodeValue;


                if (implodeValue <= 0)
                {
                    implodeValue = 0;
                    imploded = true;
                    warned = false;
                }
                else if (implodeValue <= 0.35 && !warned)
                {
                    warned = true;
                    pa.PlayWarn();
                    if (TipSeq.Instance)
                    {
                        TipSeq.Instance.UpdateTip(TipSeq.TipStage.warn);
                    }
                }
            }
        }

        if (pc.IsDashing)
        {
            cc.Move(newtonVelocity * newtonDecay * Time.deltaTime);
            newtonDecay -= Time.deltaTime / dashTime;
        }
    }

    private void UpdateBarUI()
    {
        counterText.text = charge.ToString();
        for (int i = 0; i < bars.Length; i++)
        {
            if (i < charge)
            {
                bars[i].SetActive(true);
            }
            else
            {
                bars[i].SetActive(false);
            }
        }
    }

    //mouse1
    public bool Discharge()
    {
        if (charge > 0)
        {
            Vector3 dashDirection = look.forward.normalized;

            stableState = false;
            charge--;
            UpdateBarUI();

            usedCharge++;

            newtonVelocity = dashDirection * newtonSpeed * (usedCharge * dashMultiplier);

            dashForwardPS.Play();
            pa.PlayDash(usedCharge);
            StartDash();
            return true;
        }

        return false;
    }

    //mouse2
    public bool Charge()
    {
        if (!stableState && usedCharge > 0)
        {
            Vector3 dashDirection = look.forward.normalized;
            newtonVelocity = dashDirection * (newtonSpeed * usedCharge * -0.8f);
            StartDash();

            warned = false;

            dashBackwardPS.Play();

            pa.PlayCharge();

            ResetCharge();
            return true;
        }

        return false;
    }

    public bool ExtraCharge()
    {
        if (charge < 5)
        {
            charge++;

            UpdateBarUI();

            return true;
        }

        return false;
    }

    public void Recharge()
    {
        if (stableState || debug)
        {
            imploded = false;

            if (charge < maxCharge)
            {
                pa.PlayRecharge();
                charge = maxCharge;
            }

            UpdateBarUI();
        }
    }

    public void ResetCharge()
    {
        usedCharge = 0;
        implodeValue = 1f;
        implodeBar.value = implodeValue;
        imploded = false;
        stableState = true;
    }

    public void RespawnCharge()
    {
        charge = maxCharge;
        UpdateBarUI();
        ResetCharge();
    }

    private void StartDash()
    {
        totalDashUsed++;
        pc.IsDashing = true;
        newtonDecay = 1;
        cooldownTimer = 0;

        if (!IsInvoking("EndDash"))
        {
            Invoke("EndDash", dashTime);
        }
    }

    private void EndDash()
    {
        cooldownTimer = dashTime;
        pc.IsDashing = false;
    }
}