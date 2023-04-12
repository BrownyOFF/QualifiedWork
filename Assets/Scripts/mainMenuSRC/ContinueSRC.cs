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
        path = Application.persistentDataPath + "/save.json";
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(Continue);
    }

    void Continue()
    {
        string json = File.ReadAllText(path);
        SaveManager.DataSave data = JsonUtility.FromJson<SaveManager.DataSave>(json);
        PlayerPrefs.SetInt("IsLoaded", 0);
        var x = Mathf.RoundToInt(data.resPos.x);
        var y = Mathf.RoundToInt(data.resPos.y);
        PlayerPrefs.SetInt("PosX", x);
        PlayerPrefs.SetInt("PosY", y);
        SceneManager.LoadScene(data.currScene);
    }
    void Update()
    {
        
    }
}
