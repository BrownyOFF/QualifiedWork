using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newGameSRC : MonoBehaviour
{
    private Button bttn;

    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(NewGame);
    }

    void NewGame()
    {
        SceneManager.LoadScene("lvl_0");
    }
    void Update()
    {
        
    }
}
