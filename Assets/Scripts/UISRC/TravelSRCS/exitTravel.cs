using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitTravel : MonoBehaviour
{
    public Button bttn;
    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(OnClickFunc);
    }

    void OnClickFunc()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().changeCanvas(1);
    }
    void Update()
    {
        
    }
}
