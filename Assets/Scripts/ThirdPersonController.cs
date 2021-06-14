using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class ThirdPersonController : MonoBehaviourPunCallbacks
{
    public CharacterController controller;
    public Transform camera;
    public Transform groundCheck;

    public LayerMask groundMask;

    public static float jumpPower = 3f;
    public float groundDistance = 0.4f;
    public static float speed = 6f;
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animator animator;

    Rigidbody rb;
    PhotonView PV;
    [HideInInspector]
    public PlayerManager playerManager;


    Vector3 velocity;
    public static bool isGrounded = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isIdle", true);
        PV = GetComponentInParent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //jump power calculation
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }

        //adding gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f)
        {
            //mathamatical function, figures out what andgle to move towards
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            //creates a smooth turning angle 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);            
        }

        if (direction.x != 0 || direction.z != 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", true);
        }
        if (isGrounded == false)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isJumping", true);
        }
        else if(isGrounded == true)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;

        //if (//Touch lava)
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        playerManager.Die();
    }
}
