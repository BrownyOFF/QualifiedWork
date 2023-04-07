using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelButton : MonoBehaviour
{
    private Button bttn;
    private CameraFollow cam;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
    }

    private void onClickFunc()
    {
        cam.changeCanvas(4);
    }
    void Update()
    {
        
    }
}
