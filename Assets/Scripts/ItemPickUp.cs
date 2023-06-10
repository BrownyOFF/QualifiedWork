using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] public int id;
    public bool unique;
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
            player.GetComponent<PlayerInventory>().TakeItem(id);
            Debug.Log("Picked Item " + id);
            GameObject.Find("AdvisePanel").GetComponent<AdviceSRC>().CheckInt(id, true);
            questionMark.SetActive(false);
            if (unique)
            {
                var scene = SceneManager.GetActiveScene().name;
                if (id == 0)
                {
                    switch (scene)
                    {
                        case "lvl_0":
                            player.GetComponent<PlayerClass>().lvl_0_shard_picked = true;
                            break;
                        case "lvl_1":
                            player.GetComponent<PlayerClass>().lvl_1_shard_picked = true;
                            break;
                    }
                }
                else if (id == 1)
                {
                    switch (scene)
                    {
                        case "lvl_0":
                            player.GetComponent<PlayerClass>().lvl_0_flower_picked = true;
                            break;
                        case "lvl_1":
                            player.GetComponent<PlayerClass>().lvl_1_flower_picked = true;
                            break;
                    }
                }
                else if(id == 5)
                {
                    switch (scene)
                    {
                        case "lvl_0":
                            player.GetComponent<PlayerClass>().lvl_0_scroll_picked = true;
                            break;
                        case "lvl_1":
                            player.GetComponent<PlayerClass>().lvl_1_scroll_picked = true;
                            break;
                    }
                }
            }
            gameObject.SetActive(false);
        }
    }
}
