using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemySRC : MonoBehaviour
{
    #region Bacic Stats
    private float speed = 1f;
    private float pieces = 10f;
    private float distanceToLook = 10f;
    private float distanceToPlayer = 100f;
    #endregion

    #region Combat Stats
    public float health = 30f;
    private float damage = 20f;
    private float range = 2f;
    private float timeBeetwenAttack = 1f;
    private float startTimeBeetwenAttack = 0f;
    #endregion

    #region References
    private GameObject playerObj;
    private Rigidbody2D rb;
    #endregion

    #region Bools
    private bool isAttacking = false;
    private bool isBlocking = false;
    #endregion
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindWithTag("Player");
    }

    private void Death()
    {
        playerObj.GetComponent<PlayerChara>().getPieces(pieces);
        gameObject.SetActive(false);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
    
    public void TakeDamage(float damage)
    {
        if (gameObject != null)
        {
            health -= damage;
        }
    }

    private bool CanAttack()
    {
        if (distanceToPlayer <= range && playerObj.GetComponent<PlayerMovement>().isGrounded())
            return true;
        return false;
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = true;
        
        playerObj.GetComponent<PlayerChara>().takeDmg(damage);
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
        distanceToPlayer = Vector2.Distance(transform.position, playerObj.transform.position);
        if(health <= 0)
            Death();

        if (SeePlayer() && !CanAttack())
        {
            Move();
        }
        else if (CanAttack() && !isAttacking)
        {
            Attack();
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
