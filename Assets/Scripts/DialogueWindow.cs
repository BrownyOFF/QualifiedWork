using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueWindow : MonoBehaviour
{
    private GameObject text;
    private GameObject dialogeBox;
    private Transform[] gameobjectChilds;
    [SerializeField] private string message;
    private float timeToPrint = 0.3f;
    void Start()
    {
        //get child components
        gameobjectChilds = gameObject.GetComponentsInChildren<Transform>();
        dialogeBox = Array.Find(gameobjectChilds, p =>p.gameObject.name == "DialogBox").gameObject;
        text = Array.Find(gameobjectChilds, p =>p.gameObject.name == "TextSign").gameObject;
        
        dialogeBox.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            dialogeBox.SetActive(true);
            text.SetActive(true);
            StartCoroutine(PrintText());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ClearText();
            dialogeBox.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }

    private IEnumerator PrintText()
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.GetComponent<TextMeshPro>().text += message[i];
            yield return new WaitForSeconds(timeToPrint);
        }
    }

    private void ClearText()
    {
        text.GetComponent<TextMeshPro>().text = "";
    }
    
    void Update()
    {
        
    }
}
