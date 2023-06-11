using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AdviceTriggerSRC : MonoBehaviour
{
    public int id;
    public bool isItem = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("AdvisePanel").GetComponent<AdviceSRC>().CheckInt(id, isItem);
            Destroy(this);
        }
    }
}
