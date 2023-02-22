using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    #region Stats
    public float moveSpeed = 5f;
    
    public float attackRange = 3f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;
    
    public float distanceToMove = 10f;
    
    public float blockDuration = 1f;
    public float blockCooldown = 3f;

    private bool isBlocking = false;
    private float blockTimer = 0f;
    private float blockCooldownTimer = 0f;
    
    public float hp = 50f;
    public float pieces = 10f;
    #endregion

    private float distanceToPlayer;
    private GameObject player;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hp <= 0)
        {
            death();    
        }
        
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        // Check if it's time to block again
        if (isBlocking)
        {
            blockTimer += Time.deltaTime;
            if (blockTimer >= blockDuration)
            {
                isBlocking = false;
                blockTimer = 0f;
            }
        }
        else
        {
            blockCooldownTimer += Time.deltaTime;
            if (blockCooldownTimer >= blockCooldown)
            {
                blockCooldownTimer = 0f;
            }
        }

        // Check if the player is attacking and the enemy is not already blocking or attacking
        if (distanceToPlayer <= attackRange && !isAttacking && !isBlocking && player.GetComponent<FightBehaviour>().isAttacking)
        {
            Block();
            Debug.Log("ENEMY BLOCK");
        }
        else if (distanceToPlayer < attackRange && !isAttacking)
        {
            Attack();
            Debug.Log("ENEMY ATTACK");
        }
        else if (distanceToPlayer <= distanceToMove && distanceToPlayer > attackRange)
        {
            Move();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                attackTimer = 0f;
            }
        }

    }

    private void Move()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        isAttacking = true;

        if (distanceToPlayer <= attackRange)
        {
            player.GetComponent<PlayerChara>().takeDmg(attackDamage);
        }

    }
    
    void Block()
    {
        isBlocking = true;
    }
    
    public void takingDamage(float damage)
    {
        if (!isBlocking)
        {
            // Take damage as normal
            hp -= damage;
        }
        else
        {
            blockTimer = 0f;
        }

    }

    public void destroy()
    {
        Destroy(gameObject);
    }
    
    private void death()
    {
        player.GetComponent<PlayerChara>().getPieces(pieces);
        Destroy(gameObject);
    }
}
