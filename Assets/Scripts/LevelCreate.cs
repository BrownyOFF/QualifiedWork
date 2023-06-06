using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Player = GameObject.FindWithTag("Player");
        Enemy = Resources.Load("Enemy") as GameObject;
        
        EnemyPos = GameObject.FindGameObjectsWithTag("enemyPos");
        PlayerPos = GameObject.FindWithTag("playerPos");

        shrines = GameObject.FindGameObjectsWithTag("shrine");

        if (SceneManager.GetActiveScene().name != "lvl_hub")
        {
            foreachCycle(Enemy, EnemyPos);
        }

        if (PlayerPrefs.GetInt("newGame") != 0)
        {
            SaveClass data = SaveSystem.LoadPlayer();
            Player.GetComponent<PlayerClass>().hpMax = data.hpMax;
            Player.GetComponent<PlayerClass>().spMax = data.spMax;
            Player.GetComponent<PlayerClass>().level = data.level;
            Player.GetComponent<PlayerClass>().damage = data.damage;
            Player.GetComponent<PlayerClass>().piecesToGrade = data.piecesToGrade;
            Player.GetComponent<PlayerClass>().pieces = data.pieces;
            Player.GetComponent<PlayerClass>().flaskMax = data.flaskMax;
            Player.GetComponent<PlayerClass>().currScene = data.currScene;
            Player.GetComponent<PlayerClass>().inv = data.inv;
            Player.GetComponent<PlayerClass>().amount = data.amount;
        }
        else
        {
            Player.GetComponent<PlayerClass>().inv.Add(0);
            Player.GetComponent<PlayerClass>().inv.Add(1);
            Player.GetComponent<PlayerClass>().inv.Add(5);
            Player.GetComponent<PlayerClass>().amount.Add(0);
            Player.GetComponent<PlayerClass>().amount.Add(0);
            Player.GetComponent<PlayerClass>().amount.Add(0);
        }
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
}
