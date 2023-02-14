using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement move;
    private GameObject eye;
    private float rayDistanse = 10f;
    public bool isFighting = false;
    void Start()
    {
        move = GetComponent<PlayerMovement>();
        eye = GameObject.Find("eyeCheck");
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(eye.transform.position, new Vector2(move.faceDir, 0), rayDistanse);
        if (hit.collider == null)
        {
            isFighting = false;
            move.assignStats(move.speedChill,move.jumpPowerChill,move.dashingVelocityChill,move.dashingTimeChill);
        }
        else if (hit.collider.CompareTag("Enemy"))
        {
            isFighting = true;
            Debug.Log("Enemy");
            move.assignStats(move.speedBattle,move.jumpPowerBattle,move.dashingVelocityBattle,move.dashingTimeBattle);
        }
    }
}
