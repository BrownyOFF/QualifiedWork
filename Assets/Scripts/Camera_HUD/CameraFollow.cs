using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class CameraFollow : MonoBehaviour
{
    public Animator anim;

    public int defaultScale = 8;
    public int battleScale = 6;
    private Transform target;
    public Vector3 offset;
    public float damping;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private GameObject Default;
    private GameObject Shrine;
    private GameObject Grade;
    private GameObject Travel;
    private GameObject Dialogue;
    private GameObject pauseMenu;
    public GameObject YouDied;
    public GameObject blackScreen;

    public bool noDamping = false;
    public bool inPause = false;

    private void Start()
    {
        Cursor.visible = false;
        
        Default = GameObject.Find("DefaultUI");
        Shrine = GameObject.Find("ShrineUI");
        Grade = GameObject.Find("GradeUI");
        Travel = GameObject.Find("TravelUI");
        Dialogue = GameObject.Find("DialogueUI");
        pauseMenu = GameObject.Find("PauseMenu");
        YouDied = GameObject.Find("Death");
        blackScreen = GameObject.Find("BlackScreen");
        anim = blackScreen.GetComponent<Animator>();
        YouDied.SetActive(false);
        changeCanvas(0);
        cam = GetComponent<Camera>();
        FindPlayer();
        changeScale(defaultScale);
    }

    public void FindPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void BlackScreenTransparency(int a)
    {
        if (a == 1)
        {
            anim.SetTrigger("Fade");
        }
        else
        {
            anim.SetTrigger("UnFade");

        }
    }
    public void changeCanvas(int i)
    {
        switch (i)
        {
            case 0: // default
                Shrine.SetActive(false);
                Default.SetActive(true);
                Grade.SetActive(false);
                Travel.SetActive(false);
                Dialogue.SetActive(false);
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                break;
            case 1: // shrine
                Grade.SetActive(false);
                Default.SetActive(false);
                Shrine.SetActive(true);
                if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_0_completed)
                {
                    GameObject tmp = Shrine.transform.GetChild(2).gameObject;
                    tmp.SetActive(false);
                }
                Travel.SetActive(false);
                Dialogue.SetActive(false);
                pauseMenu.SetActive(false);
                Cursor.visible = true;
                break;
            case 2: // grade
                Grade.SetActive(true);
                Default.SetActive(false);
                Shrine.SetActive(false);
                Travel.SetActive(false);
                Dialogue.SetActive(false);
                pauseMenu.SetActive(false);
                Cursor.visible = true;
                break;
            case 3: // dialogue
                Grade.SetActive(false);
                Default.SetActive(false);
                Shrine.SetActive(false);
                Travel.SetActive(false);
                Dialogue.SetActive(true);
                pauseMenu.SetActive(false);
                Cursor.visible = true;
                break;
            case 4: // travel
                Grade.SetActive(false);
                Default.SetActive(false);
                Shrine.SetActive(false);
                Dialogue.SetActive(false);
                Travel.SetActive(true);
                pauseMenu.SetActive(false);
                Cursor.visible = true;
                break;
            case 5: // pauseMenu
                Grade.SetActive(false);
                Default.SetActive(false);
                Shrine.SetActive(false);
                Dialogue.SetActive(false);
                Travel.SetActive(false);
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                break;
        }
    }
    private void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerChara>().isDead)
        {
            noDamping = true;
            return;
        }
        else if(noDamping && cam.transform.position != target.position)
        {
            noDamping = false;
            transform.position = target.position;
        }
        Vector3 movePos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
        if (Input.GetKey(KeyCode.P) && !inPause)
        {
            PauseStart();
        }
    }

    private void PauseStart()
    {
        inPause = true;
        Time.timeScale = 0f;
        changeCanvas(5);
    }

    public void PauseStop()
    {
        inPause = false;
        Time.timeScale = 1f;
        changeCanvas(0);
    }
    
    public void changeScale(int scale)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, scale, Time.deltaTime);
    }
}
