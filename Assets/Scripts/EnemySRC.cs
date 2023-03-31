using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class EnemySRC : MonoBehaviour
{
    #region Bacic Stats
    private float speed = 1f;
    private float pieces = 10f;
    private float distanceToLook = 10f;
    private float distanceToPlayer = 100f;
    #endregion

    #region Combat Stats
    public float health = 100f;
    private float damage = 20f;
    private float range = 2f;
    private float timeBeetwenAttack = 1f;
    private float startTimeBeetwenAttack = 0f;
    private float enduranceCurrent = 0f;
    private float enduranceMax = 30f;
    private float timeToUnstun = 2f;
    private float startTimeToUnstun = 0f;
    private float startTimeToUnHit = 0f;
    private float timeToUnHit = 3f;
    #endregion

    #region References
    private GameObject playerObj;
    private GameObject bar;
    private GameObject frame;
    private float barScaleCurrent;
    private float barScaleMax;
    private float scaleDiff;
    private Rigidbody2D rb;
    private Animator animCont;
    private bool isDead = false;
    #endregion

    #region Bools
    private bool isAttacking = false;
    private bool isBlocking = false;
    private bool isTakedHitRecently = false;
    private bool isStunned = false;
    private bool isRegenerating = false;
    #endregion
    
    void Start()
    {
        animCont = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindWithTag("Player");
        var enduranceBar = transform.GetChild(1).gameObject;
        frame = enduranceBar.transform.GetChild(0).gameObject;
        bar = enduranceBar.transform.GetChild(1).gameObject;
        barScaleMax = bar.GetComponent<RectTransform>().localScale.x;
        scaleDiff = enduranceMax / barScaleMax;
    }

    public IEnumerator Death()
    {
        isDead = true;
        animCont.SetTrigger("Dead");
        playerObj.GetComponent<PlayerChara>().getPieces(pieces);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
    
    public void TakeDamage(float dmg)
    {
        if (gameObject != null)
        {
            if (isStunned) //hit after stunned
            {
                health -= dmg * 5f;
                enduranceCurrent = 0f;
                isStunned = false;
                return;
            }
            var dice = UnityEngine.Random.Range(0,1f); // dice to block or hit
            if (dice >= 0.5f) // block
            {
                enduranceCurrent += dmg * 1.25f;
                animCont.SetTrigger("Block");
            }
            else // hit
            {
                animCont.SetTrigger("Hurt");
                health -= dmg;
                enduranceCurrent += dmg * 0.5f;
            }
            isTakedHitRecently = true;
            startTimeToUnHit = 0;
            if (enduranceCurrent >= enduranceMax) // check if stunned
            {
                Debug.Log("Is Stunned");
                animCont.SetTrigger("isStunned");
                isStunned = true;
            }

            if (isAttacking)
            {
                StopCoroutine(Attack());
            }
        }
    }

    private bool CanAttack()
    {
        if (distanceToPlayer <= range && playerObj.GetComponent<PlayerMovement>().isGrounded())
            return true;
        return false;
    }

    private IEnumerator Attack()
    {
        animCont.SetTrigger("Attack");
        rb.velocity = Vector2.zero;
        isAttacking = true;
        yield return new WaitForSeconds(0.15f);
                    
        playerObj.GetComponent<PlayerChara>().takeDmg(damage);
        if (playerObj.GetComponent<FightBehaviour>().isParry)
        {
            enduranceCurrent += 5;
        }
    }

    private void Move()
    {
        Vector2 direction = (playerObj.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    private bool SeePlayer()
    {
        if (distanceToPlayer <= distanceToLook)
            return true;
        else
            return false;
    }

    private void BarRenderer()
    {
        barScaleCurrent = enduranceCurrent / scaleDiff;
        if (barScaleCurrent > barScaleMax)
            barScaleCurrent = barScaleMax;
        bar.GetComponent<RectTransform>().localScale = new Vector3(barScaleCurrent,bar.GetComponent<RectTransform>().localScale.y,bar.GetComponent<RectTransform>().localScale.z);
    }

    private IEnumerator RegenEndurance()
    {
        while (enduranceCurrent > 0)
        {
            enduranceCurrent -= 1;
            yield return new WaitForSeconds(0.5f);
        }
        enduranceCurrent = 0;
        isRegenerating = false;
    }
    
    void Update()
    {
        if (playerObj.GetComponent<PlayerChara>().isDead || isDead)
        {
            return;
        }
        
        if (health <= 0 && !isDead)
            StartCoroutine(Death());                                                        // Death check, simple as that
        
        BarRenderer();

        if (isStunned && startTimeToUnstun < timeToUnstun)
        {
            startTimeToUnstun += Time.deltaTime;                            // Stun count 
            return;                                                         //    and
        }                                                                   // change bool            
        if (startTimeToUnstun >= timeToUnstun || isStunned)
        {
            startTimeToUnstun = 0f;
            isStunned = false;
        }

        if (isTakedHitRecently && startTimeToUnHit < timeToUnHit)
        {
            if(isRegenerating)
                StopCoroutine(RegenEndurance());
            startTimeToUnHit += Time.deltaTime;                                       
        }                                                                   // Count To Unhit and regen endurance
        else
        {
            if(isRegenerating)
                StopCoroutine(RegenEndurance());
            StartCoroutine(RegenEndurance());
            isRegenerating = true;
        }                                                                   // Regen Endurance
        
        distanceToPlayer = Vector2.Distance(transform.position, playerObj.transform.position);
        

        if (SeePlayer() && !CanAttack())
        {
            Move();
            animCont.SetBool("IsMoving", true);
        }
        else if (CanAttack() && !isAttacking)
        {
            animCont.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        else
        {
            animCont.SetBool("IsMoving", false);
        }

        if (isAttacking)
        {
            startTimeBeetwenAttack += Time.deltaTime;
            if (startTimeBeetwenAttack >= timeBeetwenAttack)
            {
                isAttacking = false;
                startTimeBeetwenAttack = 0f;
            }
        }
    }
}
