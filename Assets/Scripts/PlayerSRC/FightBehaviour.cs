using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private PlayerChara chara;
    private LayerMask enemyMask;
    private GameObject eye;
    private float rayDistanse = 5f;
    public bool isFighting = false;
    public bool isAttacking = false;
    public bool isBlocking = false;
    public bool isParry = false;
    
    public Camera cam;
    private float timeBtwAttack;
    private float startTimeBtwAttack = 0.5f;
    private float parryStartTime = 0f;
    private float parryTime = 0.3f;
    private GameObject attackPos;
    public float attackRange = 3f;
    private float damage = 10f;

    void Start()
    {
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        eye = GameObject.Find("eyeCheck");
        enemyMask = LayerMask.GetMask("Enemy");
        attackPos = GameObject.Find("attackPos");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public bool canAttackCheck()
    {
        if (!isBlocking && timeBtwAttack <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public bool canBlockCheck()
    {
        if (!isAttacking)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void attack()
    {
        isAttacking = true;
        timeBtwAttack = startTimeBtwAttack;
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, enemyMask);
        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            enemyToDamage[i].GetComponent<EnemySRC>().TakeDamage(damage);
        }
    }

    public void block()
    {
        if(isBlocking)
            return;
        isBlocking = true;
        if (CanParry())
        {
            
        }

    }

    private bool CanParry()
    {
        if (parryStartTime <= parryTime)
            return true;
        else
            return false;
    }
    void Update()
    {
        if(chara.isDead)
            return;
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttackCheck())
            {
                attack();
            }
            else
            {
                isAttacking = false;
                timeBtwAttack -= Time.deltaTime;
            }
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            if (canBlockCheck())
            {
                block();
            }
        }
        else
        {
            isBlocking = false;
        }

        if (isBlocking)
        {
            parryStartTime += Time.deltaTime;
        }
    }
}