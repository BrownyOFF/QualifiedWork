using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    #region Variables
    private float horizontal;
    private float speed;
    private float jumpPower;
    private float spintMult = 1.5f;
    public float faceDir;
    
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;
    public Vector2 dashingDir;
    private bool isDashing;
    private bool _canDash = true;
    #endregion

    #region ChillStats
    public float speedChill = 8f;
    public float jumpPowerChill = 16f;
    public float dashingVelocityChill = 50f;
    public float dashingTimeChill = 0.05f;
    #endregion

    #region CombatStats
    public float speedBattle = 4f;
    public float jumpPowerBattle = 16f;
    public float dashingVelocityBattle = 120f;
    public float dashingTimeBattle = 0.5f;
    #endregion
    
    [SerializeField] private PlayerChara stats;
    [SerializeField] private FightBehaviour fight;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerChara>();
        fight = GetComponent<FightBehaviour>();
        groundCheck = GameObject.Find("groundCheck");
        assignStats(speedChill,jumpPowerChill,dashingVelocityChill,dashingTimeChill);
    }

    public void assignStats(float spd,float jmppwr, float dashvel,float dashtime)
    {
        speed = spd;
        jumpPower = jmppwr;
        dashingVelocity = dashvel;
        dashingTime = dashtime;
    }

    void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetKeyDown(KeyCode.W);

        //animator.SetFloat("Speed", Mathf.Abs());
        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        if (jumpInput && isGrounded() && stats.canJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            stats.spCurrent -= stats.jumpCost;
            
        }

         //if (isGrounded == false)
         //{
            //animator.SetBool("isJumping", true);
         //}
        //else 
        //{
            //animator.SetBool("isJumping", false);
        //}
        var dashInput = Input.GetKeyDown(KeyCode.F);
        if (dashInput && _canDash && stats.canDash())
        {
            isDashing = true;
            _canDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"),0);
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            stats.spCurrent -= stats.dashCost;
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        if (isGrounded())
        {
            _canDash = true;
        }

        if (isSprinting())
        {
            rb.velocity = new Vector2(inputX * speed * spintMult, rb.velocity.y);
        }

        if (!fight.isFighting)
        {
            if (inputX == 1f || inputX == -1f)
            {
                faceDir = inputX;
            }
        }
    }
    
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f,groundLayer);
    }

    public bool isSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}
  