using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using File = System.IO.File;

public class ContinueSRC : MonoBehaviour
{
    private Button bttn;
    private string path;
    void Start()
    {
        path = Application.persistentDataPath + "/player.json";
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(Continue);
    }

    void Continue()
    {
        SaveClass tmpdata = SaveSystem.LoadPlayer();
        PlayerPrefs.SetInt("newGame", 1);
        SceneManager.LoadScene(tmpdata.currScene);

    }
    void Update()
    {
        
    }
}
