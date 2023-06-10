using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public bool IsTakingDamage = false;
    private Collider2D check;
    public PlayerChara chara;
    public EnemyClass enChara;
    private bool inTrigger = false;
    private float damage = 10f;
    void Start()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        check = col;
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inTrigger = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (check.CompareTag("Player"))
            {
                chara = check.GetComponent<PlayerChara>();
                IsTakingDamage = true;
                chara.hpCurrent -= chara.hpCurrent;
            }
            else if (check.CompareTag("Enemy") && !check.GetComponent<EnemyClass>().isDead)
            {
                enChara = check.GetComponent<EnemyClass>();
                enChara.Death();
            }
        }
    }
}
