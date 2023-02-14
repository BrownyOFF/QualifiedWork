using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private GameObject eye;
    private float rayDistanse = 10f;
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        eye = GameObject.Find("eyeCheck");
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, Vector2.right * move.dashingDir, rayDistanse);
        Debug.DrawRay(eye.transform.position, Vector2.right * move.dashingDir * rayDistanse, Color.red);
        if (hit.collider.tag == "enemy")
        {
            Debug.Log("Enemy detected");
        }
        
    }
}
