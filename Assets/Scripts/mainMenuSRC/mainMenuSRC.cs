using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class mainMenuSRC : MonoBehaviour
{
    private string path;
    private GameObject continueBttn;
    void Start()
    {
        continueBttn = GameObject.Find("ContinueBttn");
        path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            continueBttn.SetActive(true);
        }
        else
        {
            continueBttn.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
