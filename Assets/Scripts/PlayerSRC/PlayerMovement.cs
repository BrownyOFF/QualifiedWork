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
    public float faceDir;
    
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject wallCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;
    public Vector2 dashingDir;
    public float dashForce = 5f;
    public float dashCost = 25f;
    public float dashCD = 1f;
    public float dashCDcurr = 0f;
    private float lastDodgeTime = -1f;
    public bool isDashing;
    private bool _canDash = true;
    #endregion

    public CapsuleCollider2D coll;

    public GameObject player; 
    [SerializeField] private PlayerChara stats;
    [SerializeField] private FightBehaviour fight;
    public PlayerClass playerClass;
    public GameObject interestPoint;
    private void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");
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
        if (stats.isDead || stats.isStunned || stats.inDialogue || fight.isParry || fight.isAttacking )
        {
            rb.velocity = Vector2.zero;      
            return;
        }
        
        if (GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().inPause)
        {
            return;
        }
        
        if (isDashing && AtWall())
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        if(isDashing)
            return;
        
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetKeyDown(KeyCode.W);
        var dodgeInput = Input.GetKeyDown(KeyCode.F);

        if (fight.isBlocking)
        {
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return;
        }
        animator.SetFloat("Speed", Math.Abs(inputX));

        if (inputX > 0)
        {
            player.transform.localScale = new Vector3(6,6,1);
        }
        else if (inputX < 0)
        {
            player.transform.localScale = new Vector3(-6,6,1);
        }

        if (!fight.isBlocking)
        {
            rb.velocity = new Vector2(inputX * playerClass.speed, rb.velocity.y);
            if (jumpInput && isGrounded() && stats.canJump()) 
            { 
                rb.velocity = new Vector2(rb.velocity.x, playerClass.jumpPower); 
                stats.spCurrent -= stats.jumpCost; 
            }
        
            if (dodgeInput && CanDodge())
            {
                Dodge();
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
            

            if (!fight.isFighting) 
            { 
                if (inputX == 1f || inputX == -1f) 
                { 
                    faceDir = inputX;
                }
            }
        }
    }

    private bool CanDodge()
    {
        return Time.time - lastDodgeTime > dashCD && stats.spCurrent >= dashCost && rb.velocity != Vector2.zero && !isDashing;
    }

    private void Dodge()
    {
        lastDodgeTime = Time.time;
        stats.spCurrent -= dashCost;
        isDashing = true;
        coll.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.AddForce(FaceDirection() * dashForce, ForceMode2D.Impulse);
        animator.SetBool("isDash", true);
        StartCoroutine(StopDashing());
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        animator.SetBool("isDash", false);
        coll.isTrigger = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.5f,groundLayer);
    }

    public bool AtWall()
    {
        return Physics2D.OverlapCircle(wallCheck.transform.position, 0.1f, groundLayer);
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
  