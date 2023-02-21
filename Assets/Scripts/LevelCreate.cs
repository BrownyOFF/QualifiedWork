using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LevelCreate : MonoBehaviour
{
    #region PreFabs


    private GameObject Player;
    private GameObject Enemy;

    private GameObject[] EnemyPos;
    private GameObject PlayerPos;

    #endregion
    void Awake()
    {
        Player = Resources.Load("Player") as GameObject;
        Enemy = Resources.Load("Enemy") as GameObject;

        EnemyPos = GameObject.FindGameObjectsWithTag("enemyPos");
        PlayerPos = GameObject.FindWithTag("playerPos");

        foreach (var tPos in EnemyPos)
        {
            SpawnObject(Enemy, tPos);
        }
        SpawnObject(Player, PlayerPos);
    }

    private void SpawnObject(GameObject obj, GameObject pos)
    {
        var posVec = new Vector2(pos.transform.position.x, pos.transform.position.y);
        Instantiate(obj, posVec, Quaternion.identity);
    }
    
    
    void Update()
    {
        
    }
}
