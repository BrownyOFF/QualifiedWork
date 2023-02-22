using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private LayerMask enemyMask;
    private GameObject eye;
    private float rayDistanse = 10f;
    public bool isFighting = false;
    public bool isAttacking = false;
    public bool isBlocking = false;
    private int tmpChangeStat = 1; // 0 - chill ; 1 - fight ; 2 - inactive
    
    public Camera cam;
    private float timeBtwAttack;
    private float startTimeBtwAttack;
    private GameObject attackPos;
    public float attackRange;
    private float damage;
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        eye = GameObject.Find("eyeCheck");
        enemyMask = LayerMask.GetMask("Enemy");
        attackPos = GameObject.Find("attackPos");
        attackRange = 3f;
        damage = 10f;
        startTimeBtwAttack = 0.5f;
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
            enemyToDamage[i].GetComponent<EnemyStat>().takingDamage(damage);
            if (enemyToDamage[i].GetComponent<EnemyStat>().hp <= 0)
                isFighting = false;
        }
    }

    public void block()
    {
        // Block Template
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttackCheck())
            {
                attack();
            }else
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
        
        enemyCheck();

        if (tmpChangeStat == 0)
        {
            cam.GetComponent<CameraFollow>().changeScale(cam.GetComponent<CameraFollow>().defaultScale);
            move.assignStats(move.speedChill,move.jumpPowerChill,move.dashingVelocityChill,move.dashingTimeChill);
            tmpChangeStat = 2;
        }
        else if (tmpChangeStat == 1)
        {
            cam.GetComponent<CameraFollow>().changeScale(cam.GetComponent<CameraFollow>().battleScale);
            move.assignStats(move.speedBattle,move.jumpPowerBattle,move.dashingVelocityBattle,move.dashingTimeBattle);
            tmpChangeStat = 2;
        }
    }

    private void enemyCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, new Vector2(move.faceDir, 0), rayDistanse, enemyMask);
        if (hit.collider == null)
        {
            if (isFighting)
            {
                isFighting = false;
                tmpChangeStat = 0;
            }
            else
                tmpChangeStat = 2;
        }
        else if (hit.collider.CompareTag("Enemy"))
        {
            isFighting = true;
            tmpChangeStat = 1;
        }
    }
}
