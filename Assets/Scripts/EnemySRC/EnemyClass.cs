using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using Random = System.Random;

public class EnemyClass : MonoBehaviour
{
    #region stats
    public float hpMax;
    public float hpCurrent;
    public float speed;
    public bool isDead;
    public float pieces;
    #endregion

    #region Attacks
    public float dmg;
    public float range;
    public float attackCd;
    public float attackDelay;
    public bool isStunned;
    public bool canOnlyDodge;
    public bool canOnlyParry;
    #endregion

    #region Look
    public float distanceToSee;
    public float distanceToAttack;
    public float distanceToPlayer;
    #endregion

    #region Others
    Random rng = new Random();
    public bool isAttacking = false;
    public bool canAttack = true;
    #endregion
    
    #region References
    public GameObject playerObj;
    public GameObject itemDrop;
    public Rigidbody2D rb;
    public Animator animController;
    public EnemyBehavior enemyBehavior;
    public GameObject attackPoss;
    public GameObject indicatorDodge;
    public GameObject indicatorParry;
    public LayerMask playerLayer;
    #endregion

    #region Methods

    public IEnumerator AttackCd()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCd);
        canAttack = true;
    }
    
    public bool SeePlayer()
    {
        if (distanceToPlayer <= distanceToSee)
            return true;
        return false;
    }
    public bool CanAttack()
    {
        if (distanceToPlayer <= distanceToAttack && canAttack)
            return true;
        return false;
    }
    public void Death()
    {
        StopAllCoroutines();
        enemyBehavior.enabled = false;
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        rb.isKinematic = true;
        isDead = true;
        animController.SetTrigger("isDead");
        
        int dice = rng.Next(1,100);
        if (dice % 2 == 0)
        {
            var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GameObject pickUp = Instantiate(itemDrop, pos, Quaternion.identity);
            dice = rng.Next(1, 100);
            if (dice % 2 == 0)  // knifeID == 3
            {
                pickUp.GetComponent<ItemPickUp>().id = 3;
            }
            else   // bombID == 4
            {
                pickUp.GetComponent<ItemPickUp>().id = 4;
            }
        }
        playerObj.GetComponent<PlayerChara>().getPieces(pieces);
        this.enabled = false;
    }

    private IEnumerator StunCd()
    {
        isStunned = true;
        animController.SetTrigger("isStunned");
        yield return new WaitForSeconds(2f);
        isStunned = false;
    }

    public void TakeDamage(float damage)
    {
        if (gameObject != null)
        {
            if (!isAttacking)
            {
                animController.SetTrigger("getHit");
            }
            if (isStunned) //hit after stun
            {
                hpCurrent -= damage * 2f;
                StopCoroutine(StunCd());
                isStunned = false;
                return;
            }
            
            hpCurrent -= damage; //normal hit
        }
    }

    public bool InRangeAttack()
    {
        if (distanceToPlayer <= range)
        {
            return true;
        }
        return false;
    }

    public IEnumerator Attack()
    {
        // 0 - default attack
        // 1 - only dodge
        // 2 - only parry
        isAttacking = true;
        var dice = rng.Next(0, 100);
        if (canOnlyDodge && 0 <= dice && dice <= 10) // onlyDodge
        {
            animController.SetTrigger("attackSpecial");
            // Indicator HERE
            indicatorDodge.SetActive(true);
            var tmpColor = indicatorDodge.GetComponent<SpriteRenderer>().color;
            tmpColor.a = 100f;
            indicatorDodge.GetComponent<SpriteRenderer>().color = tmpColor;
            yield return new WaitForSeconds(attackDelay - 0.2f);
            // Indicator FLASH
            tmpColor.a = 255f;
            indicatorDodge.GetComponent<SpriteRenderer>().color = tmpColor;
            yield return new WaitForSeconds(0.2f);
            Collider2D[] colls = Physics2D.OverlapCircleAll(attackPoss.transform.position, range, playerLayer);
            foreach (var i in colls)
            {
                if (i.GetComponent<PlayerChara>().takeDmg(dmg, 1))
                {
                    StartCoroutine(StunCd());
                }
            }
            indicatorDodge.SetActive(false);
        }
        else if (canOnlyParry && 90 <= dice && dice < 100) // onlyParry
        {
            animController.SetTrigger("attackSpecial");
            // Indicator HERE
            indicatorParry.SetActive(true);
            var tmpColor = indicatorDodge.GetComponent<SpriteRenderer>().color;
            tmpColor.a = 100f;
            indicatorParry.GetComponent<SpriteRenderer>().color = tmpColor;
            yield return new WaitForSeconds(attackDelay - 0.2f);
            // Indicator FLASH
            tmpColor.a = 255f;
            indicatorParry.GetComponent<SpriteRenderer>().color = tmpColor;
            yield return new WaitForSeconds(0.2f);
            Collider2D[] colls = Physics2D.OverlapCircleAll(attackPoss.transform.position, range, playerLayer);
            foreach (var i in colls)
            {
                if (i.GetComponent<PlayerChara>().takeDmg(dmg, 2))
                {
                    StartCoroutine(StunCd());
                }
            }
            indicatorParry.SetActive(false);
        }
        else  // default
        {
            animController.SetTrigger("attack");
            yield return new WaitForSeconds(attackDelay);
            Collider2D[] colls = Physics2D.OverlapCircleAll(attackPoss.transform.position, range, playerLayer);
            foreach (var i in colls)
            {
                if (i.GetComponent<PlayerChara>().takeDmg(dmg, 0))
                {
                    StartCoroutine(StunCd());
                }
            }
        }
        isAttacking = false;
        StartCoroutine(AttackCd());
    }
    
    public void Move()
    {
        Vector2 direction = (playerObj.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        animController.SetTrigger("run");
        if (rb.velocity.x > 0)              // mirror gameobject
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    #endregion
    
}
