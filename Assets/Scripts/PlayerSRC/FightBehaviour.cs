using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private PlayerChara chara;
    private LayerMask enemyMask;
    private float rayDistanse = 5f;
    public bool isFighting = false;
    public bool isAttacking = false;
    public bool isBlocking = false;
    public bool isParry = false;
    public bool isRecentlyHit = false;
    private float unHitTime = 3f;
    private float startUnHitTime = 0f;
    public Camera cam;
    private float timeBtwAttack;
    private float startTimeBtwAttack = 0.4f;
    private int attackCount = 0;
    private int attackCountMax = 3;
    private float attackDelayTime = 1f;
    private float parryTime = 0.3f;
    private GameObject attackPos;
    public float attackRange = 3f;
    private float damage = 10f;
    private bool isBlockClicked = false;
    private Animator animCont;

    void Start()
    {
        animCont = GetComponent<Animator>();
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        enemyMask = LayerMask.GetMask("Enemy");
        attackPos = GameObject.Find("attackPos");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public bool canAttackCheck()
    {
        if (!isBlocking && timeBtwAttack <= 0 && move.rb.velocity == Vector2.zero)
        {
            return true;
        }
        else
            return false;
    }

    public bool canBlockCheck()
    {
        if (!isAttacking && !isBlocking)
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
        animCont.SetTrigger("Attack");
        Debug.Log("Attack!");
        isAttacking = true;
        timeBtwAttack = startTimeBtwAttack;
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, enemyMask);
        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            enemyToDamage[i].GetComponent<EnemySRC>().TakeDamage(damage);
        }
    }
    

    private IEnumerator CanParry()
    {
        isParry = true;
        yield return new WaitForSeconds(parryTime);
        isParry = false;
    }
    void Update()
    {
        if(chara.isDead || chara.isStunned)
            return;
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
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
                animCont.SetBool("Block", true);
                isBlocking = true;
                StartCoroutine(CanParry());
            }
        }
        else
        {
            animCont.SetBool("Block", false);
            isParry = false;
            isBlocking = false;
            timeBtwAttack -= Time.deltaTime;
            if (timeBtwAttack <= 0)
            {
                isAttacking = false;
            }
        }
        
        if (isRecentlyHit)
        {
            startUnHitTime += Time.deltaTime;
            if (startUnHitTime >= unHitTime)
            {
                startUnHitTime = 0;
                isRecentlyHit = false;
            }
        }
    }
}