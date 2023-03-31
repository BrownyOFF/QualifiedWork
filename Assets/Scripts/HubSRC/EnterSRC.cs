using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSRC : MonoBehaviour
{
    private GameObject questionMark;
    [SerializeField] private TextAsset textFile;
    private bool inside = false;
    private bool inShop = false;
    void Start()
    {
        questionMark = GameObject.FindWithTag("Player").transform.GetChild(3).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questionMark.SetActive(true);
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            questionMark.SetActive(false);
            inside = false;
            inShop = false;
        }
    }

    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.E) && !inShop)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerChara>().inDialogue = true;
            StartCoroutine(GameObject.FindWithTag("MainCamera").GetComponent<DialogueSRC>().DialogueSrc(textFile));
            inShop = true;
        }
    }
}
