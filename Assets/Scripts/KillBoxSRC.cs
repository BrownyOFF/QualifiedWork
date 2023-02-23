using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KillBoxSRC : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerChara>().hpCurrent = 0f;
        }
        else if (col.CompareTag("Enemy"))
        {
            col.GetComponent<EnemyStat>().death();
        }
    }

    void Update()
    {
        
    }
}
