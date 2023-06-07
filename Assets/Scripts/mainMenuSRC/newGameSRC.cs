using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class newGameSRC : MonoBehaviour
{
    private Button bttn;
    private string path;
    void Start()
    {
        path = Application.persistentDataPath + "/player.json";
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(NewGame);
    }

    void NewGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        PlayerPrefs.SetInt("newGame", 0);
        GameObject.Find("LoadManager").GetComponent<Load>().LoadScene("lvl_0");
        
    }
    void Update()
    {
        
    }
}
