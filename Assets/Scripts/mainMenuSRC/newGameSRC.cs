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
        path = Application.persistentDataPath + "/save.json";
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(NewGame);
    }

    void NewGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        PlayerPrefs.SetInt("PlayerPos", 0);
        SceneManager.LoadScene("lvl_0");
    }
    void Update()
    {
        
    }
}
