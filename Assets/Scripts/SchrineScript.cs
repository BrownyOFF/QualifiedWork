using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrineScript : MonoBehaviour
{
    private GameObject camera;
    private GameObject btnShow;
    private bool canEnter = false;
    private bool inShrine = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        canEnter = true;
        btnShow.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canEnter = false;
        btnShow.SetActive(false);
        camera.GetComponent<CameraFollow>().changeCanvas(0);
    }

    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
        btnShow = gameObject.transform.GetChild(0).gameObject;
        btnShow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canEnter && !inShrine)
        {
            camera.GetComponent<CameraFollow>().changeCanvas(1);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindWithTag("Player").GetComponent<FightBehaviour>().enabled = false;
            inShrine = true;
        }
        else if (Input.GetKey(KeyCode.Escape) && inShrine)
        {
            camera.GetComponent<CameraFollow>().changeCanvas(0);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<FightBehaviour>().enabled = true;
            inShrine = false;
        }
        
        
    }
}
