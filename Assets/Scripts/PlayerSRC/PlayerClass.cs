using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
        #region PlayerChara
        public float hpMax = 100f;
        public float spMax = 50f;
        public int piecesToGrade = 10;
        public int pieces = 0;
        public int level = 1;
        #endregion

        #region FightBehaviour
        public float damage = 10;
        #endregion

        #region Movement
        public float speed = 8f;
        public float jumpPower = 16f;
        public float spintMult = 1.5f;
        #endregion

        #region Inventory
        public int flaskMax = 2;
        public List<int> inv;
        public List<int> amount;
        #endregion

        #region Progress
        public bool lvl_0_open;
        public bool lvl_hub_open;
        public bool lvl_1_open;
        public bool lvl_2_open;

        public bool lvl_0_completed;
        public bool lvl_1_completed;
        public bool lvl_2_completed;

        //smith
        public bool que_01_00_smith_open;
        public bool que_01_00_smith_completed;
        public bool que_01_01_smith_open;
        public bool que_01_01_smith_completed;
    
        //reaper
        public bool que_01_00_reaper_open;
        public bool que_01_00_reaper_completed;
        public bool que_01_01_reaper_open;
        public bool que_01_01_reaper_completed;
    
        //alchemist
        public bool que_01_00_alchemist_open;
        public bool que_01_00_alchemist_completed;
        public bool que_01_01_alchemist_open;
        public bool que_01_01_alchemist_completed;
        
        //lvl_0_items
        public bool lvl_0_shard_picked;
        public bool lvl_0_flower_picked;
        public bool lvl_0_scroll_picked;
    
        //lvl_1_items
        public bool lvl_1_shard_picked;
        public bool lvl_1_flower_picked;
        public bool lvl_1_scroll_picked;
        #endregion
        public string currScene;

        public Vector3 respPos;

        public PlayerClass(float hp, float sp, int lvl, float dmg, int piecesGrade, int piece, List<int>invent, List<int> am, int flask, float x, float y, float z)
        {
            hpMax = hp;
            spMax = sp;
            level = lvl;
            damage = dmg;
            piecesToGrade = piecesGrade;
            pieces = piece;
            inv = invent;
            amount = am;
            flaskMax = flask;
            currScene = SceneManager.GetActiveScene().name;
            respPos = new Vector3(x, y, z);
        }

        public string updateScene()
        {
            currScene = SceneManager.GetActiveScene().name;
            return currScene;
        }

        public void GetPos()
        {
            var tmpPlayer = GameObject.FindWithTag("Player");
            respPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        }
        
}
