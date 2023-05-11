using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer sprite;
    #region Variables
    private float horizontal;
    //private float speed;
    //private float jumpPower;
    //private float spintMult = 1.5f;
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

    [SerializeField] private PlayerChara stats;
    [SerializeField] private FightBehaviour fight;
    public PlayerClass playerClass;
    private void Start()
    {
        playerClass = GetComponent<PlayerClass>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerChara>();
        fight = GetComponent<FightBehaviour>();
        groundCheck = GameObject.Find("groundCheck");
    }

    void Update()
    {
        if (stats.isDead || stats.isStunned || stats.inDialogue || fight.isParry)
        {
            rb.velocity = Vector2.zero;      
            return;
        }

        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetKeyDown(KeyCode.W);

        if (fight.isBlocking)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }
        animator.SetFloat("Speed", Math.Abs(inputX));

        if (inputX > 0)
        {
            sprite.flipX = false;
        }
        else if (inputX < 0)
        {
            sprite.flipX = true;
        }

        if (!fight.isBlocking)
        {
            rb.velocity = new Vector2(inputX * playerClass.player.speed, rb.velocity.y);
            if (jumpInput && isGrounded() && stats.canJump()) 
            { 
                rb.velocity = new Vector2(rb.velocity.x, playerClass.player.jumpPower); 
                stats.spCurrent -= stats.jumpCost; 
            }

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
                animator.SetBool("isDash", true); 
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
                animator.SetBool("isJump", false);
            }
            else
            {
                animator.SetBool("isJump", true);
            }
            
            if (isSprinting()) 
            { 
                rb.velocity = new Vector2(inputX * playerClass.player.speed * playerClass.player.spintMult, rb.velocity.y);
            }

            if (!fight.isFighting) 
            { 
                if (inputX == 1f || inputX == -1f) 
                { 
                    faceDir = inputX;
                }
            }
        }
    }
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        animator.SetBool("isDash", false);
    }
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 1f,groundLayer);
    }

    public bool isSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public Vector2 FaceDirection()
    {
        if (faceDir == 1)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }
}
  