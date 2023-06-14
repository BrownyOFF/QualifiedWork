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
        public bool lvl_0_open = true;
        public bool lvl_hub_open = true;
        public bool lvl_1_open = false;
        public bool lvl_2_open = false;

        public bool lvl_0_completed = false;
        public bool lvl_1_completed = false;
        public bool lvl_2_completed = false;

        //smith
        public bool que_01_00_smith_open= true;
        public bool que_01_00_smith_completed = false;
        public bool que_01_01_smith_open = false;
        public bool que_01_01_smith_completed = false;
    
        //reaper
        public bool que_01_00_reaper_open = true;
        public bool que_01_00_reaper_completed = false;
        public bool que_01_01_reaper_open = false;
        public bool que_01_01_reaper_completed = false;
    
        //alchemist
        public bool que_01_00_alchemist_open = true;
        public bool que_01_00_alchemist_completed = false;
        public bool que_01_01_alchemist_open = false;
        public bool que_01_01_alchemist_completed = false;
    
        //lvl_0_items
        public bool lvl_0_shard_picked = false;
        public bool lvl_0_flower_picked = false;
        public bool lvl_0_scroll_picked = false;
    
        //lvl_1_items
        public bool lvl_1_shard_picked = false;
        public bool lvl_1_flower_picked = false;
        public bool lvl_1_scroll_picked = false;
        #endregion
        public string currScene;

        public Vector3 respPos;

        public PlayerClass(PlayerClass data)
        { 
        hpMax = data.hpMax;
        spMax = data.spMax;
        level = data.level;
        damage = data.damage;
        piecesToGrade = data.piecesToGrade;
        pieces = data.pieces;
        currScene = data.updateScene();
        flaskMax = data.flaskMax;
        inv = data.inv;
        amount = data.amount;
        lvl_0_open = data.lvl_0_open;
        lvl_hub_open = data.lvl_hub_open;
        lvl_1_open = data.lvl_1_open;
        lvl_2_open = data.lvl_2_open;
        lvl_0_completed = data.lvl_0_completed;
        lvl_1_completed = data.lvl_1_completed;
        lvl_2_completed = data.lvl_2_completed;

        que_01_00_smith_open = data.que_01_00_smith_open;
        que_01_00_smith_completed = data.que_01_00_smith_completed;
        que_01_01_smith_open = data.que_01_01_smith_open;
        que_01_01_smith_completed = data.que_01_01_smith_completed;
        
        que_01_00_reaper_open = data.que_01_00_reaper_open;
        que_01_00_reaper_completed = data.que_01_00_reaper_completed;
        que_01_01_reaper_open = data.que_01_01_reaper_open;
        que_01_01_reaper_completed = data.que_01_01_reaper_completed;
        
        que_01_00_alchemist_open = data.que_01_00_alchemist_open;
        que_01_00_alchemist_completed = data.que_01_00_alchemist_completed;
        que_01_01_alchemist_open = data.que_01_01_alchemist_open;
        que_01_01_alchemist_completed = data.que_01_01_alchemist_completed;
        
        lvl_0_shard_picked = data.lvl_0_shard_picked;
        lvl_0_flower_picked = data.lvl_0_flower_picked;
        lvl_0_scroll_picked = data.lvl_0_scroll_picked;
        
        lvl_1_shard_picked = data.lvl_1_shard_picked;
        lvl_1_flower_picked = data.lvl_1_flower_picked;
        lvl_1_scroll_picked = data.lvl_1_scroll_picked;
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
