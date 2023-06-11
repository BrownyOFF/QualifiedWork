using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockNextLVL : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_0_completed = true;
    }
}
