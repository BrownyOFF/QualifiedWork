using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraFollow : MonoBehaviour
{
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
    public GameObject YouDied;

    public bool noDamping = false;

    private void Start()
    {
        Cursor.visible = false;
        Default = GameObject.Find("DefaultUI");
        Shrine = GameObject.Find("ShrineUI");
        Grade = GameObject.Find("GradeUI");
        YouDied = GameObject.Find("Death");
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
    
    public void changeCanvas(int i)
    {
        if (i == 0)
        {
            Shrine.SetActive(false);
            Default.SetActive(true);
            Grade.SetActive(false);
            Cursor.visible = false;

        }
        else if (i == 1)
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(true);
            Cursor.visible = true;
        }
        else if (i == 2)
        {
            Grade.SetActive(true);
            Default.SetActive(false);
            Shrine.SetActive(false);
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
