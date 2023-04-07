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
    public GameObject YouDied;
    public GameObject blackScreen;

    public bool noDamping = false;

    private void Start()
    {
        Cursor.visible = false;
        
        Default = GameObject.Find("DefaultUI");
        Shrine = GameObject.Find("ShrineUI");
        Grade = GameObject.Find("GradeUI");
        Travel = GameObject.Find("TravelUI");
        Dialogue = GameObject.Find("DialogueUI");
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
        if (i == 0) // default
        {
            Shrine.SetActive(false);
            Default.SetActive(true);
            Grade.SetActive(false);
            Travel.SetActive(false);
            Dialogue.SetActive(false);
            Cursor.visible = false;
        }
        else if (i == 1) // shrine
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(true);
            Travel.SetActive(false);
            Dialogue.SetActive(false);
            Cursor.visible = true;
        }
        else if (i == 2) // grade
        {
            Grade.SetActive(true);
            Default.SetActive(false);
            Shrine.SetActive(false);
            Travel.SetActive(false);
            Dialogue.SetActive(false);
            Cursor.visible = true;
        }
        else if (i == 4) // travel
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(false);
            Dialogue.SetActive(false);
            Travel.SetActive(true);
            Cursor.visible = true;
        }
        else if (i == 3) // dialogue
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(false);
            Travel.SetActive(false);
            Dialogue.SetActive(true);
            Cursor.visible = true;
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
    }

    public void changeScale(int scale)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, scale, Time.deltaTime);
    }
}
