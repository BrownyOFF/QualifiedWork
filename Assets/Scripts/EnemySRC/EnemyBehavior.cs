using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    public EnemyClass enemyClass;
    public bool inAction = false;
    void Start()
    {
        enemyClass = GetComponent<EnemyClass>();
        enemyClass.playerObj = GameObject.FindWithTag("Player");
        enemyClass.animController.SetTrigger("start");
    }

    void Update()
    {
        if (enemyClass.isDead || enemyClass.isStunned || !enemyClass.canAttack)
        {
            enemyClass.rb.velocity = Vector2.zero;
            return;
        }

        if (enemyClass.hpCurrent <= 0 && !enemyClass.isDead)
        {
            enemyClass.rb.velocity = Vector2.zero;
            enemyClass.Death();
        }
        
        enemyClass.distanceToPlayer = Vector2.Distance(transform.position, enemyClass.playerObj.transform.position);
        if (enemyClass.SeePlayer() && !enemyClass.CanAttack())
        {
            enemyClass.Move();
        }
        else if (enemyClass.CanAttack() && !enemyClass.isAttacking && !inAction)
        {
            enemyClass.rb.velocity = Vector2.zero;
            StartCoroutine(enemyClass.Attack());
        }
    }

    private IEnumerator Pause(float time)
    {
        inAction = true;
        yield return new WaitForSeconds(time);
        inAction = false;
    }
}
