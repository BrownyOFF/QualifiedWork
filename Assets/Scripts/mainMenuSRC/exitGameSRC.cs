using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class exitGameSRC : MonoBehaviour
{
    private Button bttn;
    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(ExitGame);
    }

    void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    void Update()
    {
        
    }
}
