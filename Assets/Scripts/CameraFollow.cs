using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public int defaultScale = 8;
    public int battleScale = 4;
    private Transform target;
    public Vector3 offset;
    public float damping;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    
    private void Start()
    {
        cam = GetComponent<Camera>();
        target = GameObject.FindWithTag("Player").transform;
        changeScale(defaultScale);
    }

    public void changeScale(int scale)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, scale, Time.deltaTime);
    }
    void FixedUpdate()
    {
        Vector3 movePos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
    }
}
