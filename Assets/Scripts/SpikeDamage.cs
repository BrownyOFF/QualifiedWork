using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public bool IsTakingDamage = false;
    private Collider2D check;
    public PlayerChara chara;
    private bool inTrigger = false;
    void Start()
    {
        
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        IsTakingDamage = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        check = col;
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inTrigger = false;
        IsTakingDamage = false;
    }

    void Update()
    {
        if (inTrigger)
        {
            if (check.CompareTag("Player") && !IsTakingDamage)
            {
                chara = check.GetComponent<PlayerChara>();
                IsTakingDamage = true;
                chara.hp -= 10f;
                StartCoroutine(Delay());

            }
        }
    }
}
