using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    private float spintMult = 1.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dashingVelocity = 1f;
    [SerializeField] private float dashingTime = 1f;
    public Vector2 dashingDir;
    private bool isDashing;
    private bool _canDash = true;
    

    #endregion

    [SerializeField] private PlayerChara stats;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerChara>();
        groundCheck = GameObject.Find("groundCheck");
        assignStats(8f,16f,50f,0.05f);
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

        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        if (jumpInput && isGrounded() && stats.canJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            stats.sp -= stats.jumpCost;
        }
        
        var dashInput = Input.GetKeyDown(KeyCode.F);
        if (dashInput && _canDash && stats.canDash())
        {
            isDashing = true;
            _canDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            stats.sp -= stats.dashCost;
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
