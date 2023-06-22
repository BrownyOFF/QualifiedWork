using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unlockNextLVL : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_0_completed = true;
        if (SceneManager.GetActiveScene().name == "lvl_1")
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_1_completed = true;
        }
    }
}
