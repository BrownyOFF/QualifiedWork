using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class LevelCreate : MonoBehaviour
{
    #region PreFabs


    public GameObject Player;
    public GameObject Enemy;

    public GameObject[] EnemyPos;
    private GameObject PlayerPos;

    #endregion
    void Awake()
    {
        Player = Resources.Load("Player") as GameObject;
        Enemy = Resources.Load("Enemy") as GameObject;

        EnemyPos = GameObject.FindGameObjectsWithTag("enemyPos");
        PlayerPos = GameObject.FindWithTag("playerPos");

        foreachCycle(Enemy, EnemyPos);
        SpawnObject(Player, PlayerPos);
    }

    public void foreachCycle(GameObject obj, GameObject[] pos)
    {
        foreach (var tPos in pos)
        {
            SpawnObject(obj, tPos);
        }
    }

    public void forEachEnemyFindDestroy()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemy != null)
        {
            foreach (var i in enemy)
            {
                i.SetActive(true);
                i.GetComponent<EnemySRC>().destroy();
            }
        }
    }
    
    public void SpawnObject(GameObject obj, GameObject pos)
    {
        var posVec = new Vector2(pos.transform.position.x, pos.transform.position.y);
        Instantiate(obj, posVec, Quaternion.identity);
    }
    
    
    void Update()
    {
        
    }
}
