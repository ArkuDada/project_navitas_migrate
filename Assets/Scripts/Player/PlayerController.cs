using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get => _instance;
    }

    [Header("Movement Settings")] public float normalSpeed = 10f;
    public float airSpeed = 7.5f;
    private float speed = 10f;
    public float jumpHeight = 10f;


    public float gravity = -10f;

    private CharacterController cc;
    private EnergyController ec;
    private PlayerAudio pa;
    private TipSeq ts;

    [SerializeField] private Transform look;

    private Vector3 velocity;
    private bool isDashing = false;

    public bool IsDashing
    {
        get => isDashing;
        set => isDashing = value;
    }

    [Header("Grounds")] [SerializeField] private Transform grCheck;
    public float grDistant = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded = false;
    [SerializeField] private LayerMask stone;

    [SerializeField] private RTATimer timer;

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        ec = GetComponent<EnergyController>();
        pa = GetComponent<PlayerAudio>();
        ts = TipSeq.Instance;
        direction = new Vector3();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private Vector3 direction;

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None)
        {
            if (!CheckDead())
            {
                PlayerInput();
            }
        }
    }


    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(grCheck.position, grDistant, groundMask) ||
                     Physics.CheckSphere(grCheck.position, grDistant, stone);
        pa.onStone = Physics.CheckSphere(grCheck.position, grDistant, stone);
    }

    private void PlayerInput()
    {
        GroundCheck();


        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        bool isWalking = (isGrounded && (Math.Abs(x) > 0 || Math.Abs(z) > 0));
        pa.walking = isWalking; //play waking sound
        if (isWalking)
        {
            if (ts)
            {
                ts.UpdateTip(TipSeq.TipStage.walk);
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (ec)
            {
                ec.Recharge();
                if (ts)
                {
                    ts.UpdateTip(TipSeq.TipStage.land);
                }
            }
        }

        if (!isGrounded || isDashing)
        {
            speed = airSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        direction = (transform.right * x + transform.forward * z);
        cc.Move(direction * (speed * Time.deltaTime));


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (ts)
            {
                ts.UpdateTip(TipSeq.TipStage.jump);
            }
        }

        if (!isDashing)
        {
            if (ec)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    if (ec.Discharge())
                    {
                        velocity.y = -2f;
                        if (ts)
                        {
                            ts.UpdateTip(TipSeq.TipStage.dash);
                            ts.UpdateTip(TipSeq.TipStage.chain);
                        }
                    }
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    if (ec.Charge())
                    {
                        velocity.y = -2f;
                        if (ts)
                        {
                            ts.UpdateTip(TipSeq.TipStage.back);
                            ts.UpdateTip(TipSeq.TipStage.back2);
                        }
                    }
                }
            }

            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
        }
    }

    public void AttachPlatform(Vector3 force)
    {
        if (isGrounded)
        {
            cc.Move(force * Time.deltaTime);
        }
    }

    [Header("Dead")] [SerializeField] private Animator deadFlash;
    [SerializeField] private AudioSource deadSound;
    private bool deadSeq = true;

    private bool CheckDead()
    {
        if (ec)
        {
            if (ec.IsImploded)
            {
                if (deadSeq)
                {
                    Invoke("Respawn", deadSound.clip.length);
                    deadSeq = false;
                    deadSound.Play();
                    deadFlash.Play("Dead");
                }

                return true;
            }
        }

        if (Falloff())
        {
            Respawn();
            return true;
        }


        return false;
    }

    public SpawnPoint spawnPoint;
    public Goal goal;

    public void Respawn()
    {
        CancelInvoke("Respawn");
        deadSeq = true;
        cc.enabled = false;
        transform.localPosition = spawnPoint.RespawnPosition();
        transform.rotation = Quaternion.identity;
        cc.enabled = true;
        goal.ResetStage();
        look.GetComponent<MouseLook>().CameraReset();
        timer.RestartClock();

        if (ec)
        {
            ec.TotalDashUsed = 0;
            ec.RespawnCharge();
        }

        Compass.Instance.ResetMarker();
    }


    private bool Falloff()
    {
        return (transform.position.y < -30);
    }
}