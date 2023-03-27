using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrineScript : MonoBehaviour
{
    private GameObject camera;
    private GameObject btnShow;
    private GameObject respPoint;
    private bool canEnter = false;
    private bool inShrine = false;
    private GameObject player;
    private GameObject questionMark;
    private Collider2D playerColl;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !inShrine)
        {
            inShrine = true;
            playerColl = col;
            canEnter = true;
            btnShow.SetActive(true);
            questionMark.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && inShrine)
        {
            inShrine = false;
            canEnter = false;
            btnShow.SetActive(false);
            camera.GetComponent<CameraFollow>().changeCanvas(0);
            questionMark.SetActive(false);
        }
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        questionMark = player.transform.GetChild(3).gameObject;
        camera = GameObject.FindWithTag("MainCamera");
        btnShow = gameObject.transform.GetChild(0).gameObject;
        respPoint = gameObject.transform.GetChild(1).gameObject;
        btnShow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canEnter && inShrine)
        {
            playerColl.GetComponent<PlayerMovement>().rb.velocity = Vector2.zero;
            playerColl.GetComponent<PlayerMovement>().animator.SetFloat("Speed", 0);
            playerColl.GetComponent<PlayerChara>().RespawnPointAssign(respPoint);
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
