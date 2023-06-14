using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreate : MonoBehaviour
{
    #region PreFabs

    public GameObject flower;
    public GameObject shard;
    public GameObject scroll;
    
    public GameObject Player;
    public GameObject Enemy;
    
    public GameObject MushRoom;
    public GameObject Goblin;
    public GameObject Skeleton;
    public GameObject FlyingEye;

    public GameObject[] EnemyPos;
    
    public GameObject[] MushRoomPos;
    public GameObject[] GoblinPos;
    public GameObject[] SkeletonPos;
    public GameObject[] FlyingEyePos;
    
    private GameObject PlayerPos;

    #endregion

    public SaveClass data; 
    private int playerTP;
    private GameObject[] shrines;
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Enemy = Resources.Load("Enemy") as GameObject;
        
        MushRoom = Resources.Load("MushRoomEnemy") as GameObject;
        Goblin = Resources.Load("GoblinEnemy") as GameObject;
        Skeleton = Resources.Load("SkeletonEnemy") as GameObject;
        FlyingEye = Resources.Load("EyeEnemy") as GameObject;
        

        PlayerPos = GameObject.FindWithTag("playerPos");

        shrines = GameObject.FindGameObjectsWithTag("shrine");

        if (SceneManager.GetActiveScene().name != "lvl_hub")
        {
            foreachCycle(MushRoom, MushRoomPos);
            foreachCycle(Goblin, GoblinPos);
            foreachCycle(Skeleton, SkeletonPos);
            foreachCycle(FlyingEye, FlyingEyePos);
        }

        if (PlayerPrefs.GetInt("newGame") != 0) //load game
        {
            data = SaveSystem.LoadPlayer();
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
            Player.GetComponent<PlayerClass>().respPos = new Vector3(data.position[0],data.position[1],data.position[2]);
            if (Player.GetComponent<PlayerClass>().currScene == SceneManager.GetActiveScene().name)
            {
                GameObject.FindWithTag("Player").transform.position = Player.GetComponent<PlayerClass>().respPos;
            }

            #region ProggressData
            Player.GetComponent<PlayerClass>().lvl_0_open = data.lvl_0_open;
            Player.GetComponent<PlayerClass>().lvl_hub_open = data.lvl_hub_open;
            Player.GetComponent<PlayerClass>().lvl_1_open = data.lvl_1_open;
            Player.GetComponent<PlayerClass>().lvl_2_open = data.lvl_2_open;

            Player.GetComponent<PlayerClass>().lvl_0_completed = data.lvl_0_completed;
            Player.GetComponent<PlayerClass>().lvl_1_completed = data.lvl_1_completed;
            Player.GetComponent<PlayerClass>().lvl_2_completed = data.lvl_2_completed;

            Player.GetComponent<PlayerClass>().que_01_00_smith_open = data.que_01_00_smith_open;
            Player.GetComponent<PlayerClass>().que_01_00_smith_completed = data.que_01_00_smith_completed;
            Player.GetComponent<PlayerClass>().que_01_01_smith_open = data.que_01_01_smith_open;
            Player.GetComponent<PlayerClass>().que_01_01_smith_completed = data.que_01_01_smith_completed;

            Player.GetComponent<PlayerClass>().que_01_00_reaper_open = data.que_01_00_reaper_open;
            Player.GetComponent<PlayerClass>().que_01_00_reaper_completed = data.que_01_00_reaper_completed;
            Player.GetComponent<PlayerClass>().que_01_01_reaper_open = data.que_01_01_reaper_open;
            Player.GetComponent<PlayerClass>().que_01_01_reaper_completed = data.que_01_01_reaper_completed;

            Player.GetComponent<PlayerClass>().que_01_00_alchemist_open = data.que_01_00_alchemist_open;
            Player.GetComponent<PlayerClass>().que_01_00_alchemist_completed = data.que_01_00_alchemist_completed;
            Player.GetComponent<PlayerClass>().que_01_01_alchemist_open = data.que_01_01_alchemist_open;
            Player.GetComponent<PlayerClass>().que_01_01_alchemist_completed = data.que_01_01_alchemist_completed;

            Player.GetComponent<PlayerClass>().lvl_0_shard_picked = data.lvl_0_shard_picked;
            Player.GetComponent<PlayerClass>().lvl_0_flower_picked = data.lvl_0_flower_picked;
            Player.GetComponent<PlayerClass>().lvl_0_scroll_picked = data.lvl_0_scroll_picked;
            
            Player.GetComponent<PlayerClass>().lvl_1_shard_picked = data.lvl_1_shard_picked;
            Player.GetComponent<PlayerClass>().lvl_1_flower_picked = data.lvl_1_flower_picked;
            Player.GetComponent<PlayerClass>().lvl_1_scroll_picked = data.lvl_1_scroll_picked;
            #endregion

            if (SceneManager.GetActiveScene().name == "lvl_0")
            {
                if (Player.GetComponent<PlayerClass>().lvl_0_flower_picked)
                {
                    flower.SetActive(false);
                }
                if (Player.GetComponent<PlayerClass>().lvl_0_shard_picked)
                {
                    shard.SetActive(false);
                }
                if (Player.GetComponent<PlayerClass>().lvl_0_scroll_picked)
                {
                    scroll.SetActive(false);
                }
            }
            else if (SceneManager.GetActiveScene().name == "lvl_1")
            {
                if (Player.GetComponent<PlayerClass>().lvl_1_flower_picked)
                {
                    flower.SetActive(false);
                }
                if (Player.GetComponent<PlayerClass>().lvl_1_shard_picked)
                {
                    shard.SetActive(false);
                }
                if (Player.GetComponent<PlayerClass>().lvl_1_scroll_picked)
                {
                    scroll.SetActive(false);
                }
            }
        }
        else
        {
            Player.GetComponent<PlayerClass>().inv.Add(0);
            Player.GetComponent<PlayerClass>().inv.Add(1);
            Player.GetComponent<PlayerClass>().inv.Add(5);
            Player.GetComponent<PlayerClass>().amount.Add(0);
            Player.GetComponent<PlayerClass>().amount.Add(0);
            Player.GetComponent<PlayerClass>().amount.Add(0);
        } //new game
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
                i.GetComponent<EnemyClass>().DestroyObj();
            }
        }
    }
    
    public void SpawnObject(GameObject obj, GameObject pos)
    {
        var posVec = new Vector2(pos.transform.position.x, pos.transform.position.y);
        Instantiate(obj, posVec, Quaternion.identity);
    }
}
