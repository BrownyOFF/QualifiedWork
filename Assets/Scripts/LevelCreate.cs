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

    private int playerTP;
    private GameObject[] shrines;
    void Awake()
    {
        Player = Resources.Load("Player") as GameObject;
        Enemy = Resources.Load("Enemy") as GameObject;

        EnemyPos = GameObject.FindGameObjectsWithTag("enemyPos");
        PlayerPos = GameObject.FindWithTag("playerPos");

        shrines = GameObject.FindGameObjectsWithTag("shrine");

        playerTP = PlayerPrefs.GetInt("PlayerPos");
        if (playerTP != null)
        {
            SpawnObject(Player, shrines[playerTP]);
        }
        else
        {
            SpawnObject(Player, PlayerPos);
        }
        foreachCycle(Enemy, EnemyPos);
    }

    public void foreachCycle(GameObject obj, GameObject[] pos)
    {
        if (pos != null)
        {
            foreach (var tPos in pos)
            {
                SpawnObject(obj, tPos);
            }
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
