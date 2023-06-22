using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    public PlayerClass playerClass;
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
    public GameObject cam;
    private float timeBtwAttack;
    private float startTimeBtwAttack = 0.5f;
    private int attackCount = 0;
    private int attackCountMax = 3;
    private float attackDelayTime = 1f;
    private float parryTime = 0.5f;
    private GameObject attackPos;
    private bool parryCan = true;
    public float attackRange = 2f;
    //public float damage = 10f;
    private bool isBlockClicked = false;
    private bool canAttack = true;
    private Animator animCont;

    public AudioSource attacksfx;
    void Start()
    {
        playerClass = GetComponent<PlayerClass>();
        animCont = GetComponent<Animator>();
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        enemyMask = LayerMask.GetMask("Enemy");
        attackPos = GameObject.Find("attackPos");
        cam = GameObject.Find("Main Camera");
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

    public IEnumerator attack()
    {
        attacksfx.Play();
        animCont.SetTrigger("Attack");
        isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds(0.25f);
        timeBtwAttack = startTimeBtwAttack;
        Collider2D[] enemyToDamage = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, enemyMask);
        for (int i = 0; i < enemyToDamage.Length; i++)
        {
            if (!enemyToDamage[i].GetComponent<EnemyClass>().isDead)
            {
                enemyToDamage[i].GetComponent<EnemyClass>().TakeDamage(playerClass.damage);
            }
        }
        isAttacking = false;
        StartCoroutine(AttackCD());
    }

    public IEnumerator CanParry()
    {
        if (!isAttacking && !isBlocking)
        {
            animCont.SetTrigger("Parry");
            isParry = true;
            parryCan = false;
            yield return new WaitForSeconds(0.2f);
            isParry = false;
            StartCoroutine(ParryCD());
        }
    }

    private IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(startTimeBtwAttack);
        canAttack = true;
    }
    public IEnumerator ParryCD()
    {
        yield return new WaitForSeconds(parryTime);
        parryCan = true;
    }

    void Update()
    {
        if (chara.isDead || chara.isStunned || chara.inDialogue || cam.GetComponent<CameraFollow>().inPause)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack && move.rb.velocity == Vector2.zero)
        {
            StartCoroutine(attack());
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
        else if (Input.GetMouseButtonDown(2) && parryCan && move.rb.velocity == Vector2.zero)
        {
            StartCoroutine(CanParry());
        }
        else
        {
            animCont.SetBool("Block", false);
            isBlocking = false;
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