using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private int type; // 0 - flower; 1 - shard;
    private bool inside = false;
    private GameObject player;
    private GameObject questionMark;
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inside)
        {
            inside = true;
            player = other.gameObject;
            questionMark = player.transform.GetChild(3).gameObject;
            questionMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inside)
        {
            inside = false;
            questionMark.SetActive(false);
        }
    }

    
    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<PlayerChara>().TakeItem(type);
            Debug.Log("Picked Item");
            gameObject.SetActive(false);
        }
    }
}
