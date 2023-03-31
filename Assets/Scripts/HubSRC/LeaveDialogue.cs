using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveDialogue : MonoBehaviour
{
    private Button bttn;
    private CameraFollow cam;
    private DialogueSRC dia;
    private GameObject player;
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        dia = GameObject.FindWithTag("MainCamera").GetComponent<DialogueSRC>();

        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
        player = GameObject.FindWithTag("Player");
    }
    private void onClickFunc()
    {
        player.GetComponent<PlayerChara>().inDialogue = false;
        foreach (var i in dia.bttnList)
        {
            Destroy(i);
        }
        cam.changeCanvas(0);
    }
    
    void Update()
    {
        
    }
}
