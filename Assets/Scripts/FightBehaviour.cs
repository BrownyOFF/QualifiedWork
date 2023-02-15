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
    }

    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            // can attack
            if (Input.GetKey(KeyCode.Mouse0))
            {
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, enemyMask);
                for (int i = 0; i < enemyToDamage.Length; i++)
                {
                    enemyToDamage[i].GetComponent<EnemyStat>().takingDamage(damage);
                }
            }

        } else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, new Vector2(move.faceDir, 0), rayDistanse, enemyMask);
        if (hit.collider == null)
        {
            isFighting = false;
            move.assignStats(move.speedChill,move.jumpPowerChill,move.dashingVelocityChill,move.dashingTimeChill);
        }
        else if (hit.collider.CompareTag("Enemy"))
        {
            isFighting = true;
            Debug.Log("Enemy");
            move.assignStats(move.speedBattle,move.jumpPowerBattle,move.dashingVelocityBattle,move.dashingTimeBattle);
        }
    }
}
