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
    private float range = 1.3f;
    private float timeBeetwenAttack = 3f;
    private float startTimeBeetwenAttack = 0f;
    private float timeToUnstun = 2f;
    private float startTimeToUnstun = 0f;
    private float startTimeToUnHit = 0f;
    private float timeToUnHit = 3f;
    #endregion

    #region References
    private GameObject playerObj;
    private GameObject itemDrop;
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
        itemDrop = Resources.Load("ItemPickUp") as GameObject;
        animCont = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindWithTag("Player");
    }

    public IEnumerator Death()
    {
        isDead = true;
        animCont.SetTrigger("Dead");
        var pos = new Vector2(transform.position.x,transform.position.y);
        GameObject pickUp = Instantiate(itemDrop, pos, Quaternion.identity);
        pickUp.GetComponent<ItemPickUp>().id = 3;
        playerObj.GetComponent<PlayerChara>().getPieces(pieces);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator StunCD()
    {
        isStunned = true;
        yield return new WaitForSeconds(3f);
        isStunned = false;
    }
    
    public void TakeDamage(float dmg)
    {
        if (gameObject != null)
        {
            if (isStunned) //hit after stunned
            {
                health -= dmg * 5f;
                StopCoroutine(StunCD());
                isStunned = false;
                return;
            }
            
            animCont.SetTrigger("Hurt");
            health -= dmg;
            rb.AddForce(new Vector2(50000f, 0f),ForceMode2D.Impulse);
            
            isTakedHitRecently = true;
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
        yield return new WaitForSeconds(0.3f);
        if (playerObj.GetComponent<FightBehaviour>().isParry)
        {
            StartCoroutine(StunCD());
            
        }else
        {
            playerObj.GetComponent<PlayerChara>().takeDmg(damage, 0);
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

    void Update()
    {
        if (playerObj.GetComponent<PlayerChara>().isDead || isDead)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        if (health <= 0 && !isDead)
            StartCoroutine(Death());                                                        // Death check, simple as that
        
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

        distanceToPlayer = Vector2.Distance(transform.position, playerObj.transform.position);
        
        if (SeePlayer() && !CanAttack())
        {
            Move();
            animCont.SetBool("IsMoving", true);
        }
        else if (CanAttack() && !isAttacking)
        {
            rb.velocity = Vector2.zero;
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
