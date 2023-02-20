using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueWindow : MonoBehaviour
{
    private TextMesh text;
    private string message;
    void Start()
    {
        text = GetComponentInChildren<TextMesh>();
        text.gameObject.SetActive(false);
        message = "Ты умрешь kekw";
        text.text = message;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        text.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
