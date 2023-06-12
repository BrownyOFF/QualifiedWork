using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMenu : MonoBehaviour
{
    private Button bttn;
    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(OnClickFunc);
    }

    private void OnClickFunc()
    {
        GameObject.Find("LoadManager").GetComponent<Load>().LoadScene("MainMenu");
    }
    void Update()
    {
        
    }
}
