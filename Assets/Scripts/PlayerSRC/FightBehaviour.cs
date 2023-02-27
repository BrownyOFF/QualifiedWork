using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private LayerMask enemyMask;
    private GameObject eye;
    private float rayDistanse = 5f;
    public bool isFighting = false;
    public bool isAttacking = false;
    public bool isBlocking = false;

    public Camera cam;
    private float timeBtwAttack;
    private float startTimeBtwAttack = 0.5f;
    private GameObject attackPos;
    public float attackRange = 3f;
    private float damage = 10f;

    void Start()
    {
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
        // Block Template
    }

    void Update()
    {
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

        enemyCheck();

        /*if (!isFighting && !move.chillStats)
        {
            cam.GetComponent<CameraFollow>().changeScale(cam.GetComponent<CameraFollow>().defaultScale);
            move.assignStats(move.speedChill, move.jumpPowerChill, move.dashingVelocityChill, move.dashingTimeChill);
            move.chillStats = true;
        }
        else if (isFighting && !move.chillStats)
        {
            cam.GetComponent<CameraFollow>().changeScale(cam.GetComponent<CameraFollow>().battleScale);
            move.assignStats(move.speedBattle, move.jumpPowerBattle, move.dashingVelocityBattle,
                move.dashingTimeBattle);
            move.chillStats = false;
        }*/
    }

    private void enemyCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, new Vector2(move.faceDir, 0), rayDistanse, enemyMask);
        if (hit.collider == null)
        {
            if (isFighting)
            {
                isFighting = false;
            }
        }
        else if (hit.collider.CompareTag("Enemy") && !isFighting)
        {
            isFighting = true;
        }
    }

}