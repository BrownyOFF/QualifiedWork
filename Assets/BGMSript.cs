using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class BGMSript : MonoBehaviour
{
    public AudioSource lvl_0;
    public AudioSource lvl_1;
    public AudioSource lvl_hub;

    public string scene;
    
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case "lvl_0":
                lvl_0.Play();
                break;
            case "lvl_1":
                lvl_1.Play();
                break;
            case "lvl_hub":
                lvl_hub.Play();
                break;
        }

    }

    void Update()
    {
        
    }
}
