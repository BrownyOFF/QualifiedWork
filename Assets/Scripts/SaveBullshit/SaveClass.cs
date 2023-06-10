using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveClass
{
    #region PlayerStats
    public float hpMax;
    public float spMax;
    public int level;
    public float damage;
    public int piecesToGrade;
    public int pieces;
    public string currScene;
    public float[] position;
    public int flaskMax;
    public List<int> inv;
    public List<int> amount;
    #endregion

    #region ProgresVars
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

    public SaveClass(PlayerClass player)
    {
        hpMax = player.hpMax;
        spMax = player.spMax;
        level = player.level;
        damage = player.damage;
        piecesToGrade = player.piecesToGrade;
        pieces = player.pieces;
        currScene = player.updateScene();
        position = new float[3];
        position[0] = GameObject.FindWithTag("Player").transform.position.x;
        position[1] = GameObject.FindWithTag("Player").transform.position.y;
        position[2] = GameObject.FindWithTag("Player").transform.position.z;
        flaskMax = player.flaskMax;
        inv = player.inv;
        amount = player.amount;
        lvl_0_open = player.lvl_hub_open;
        lvl_hub_open = player.lvl_hub_open;
        lvl_1_open = player.lvl_1_open;
        lvl_2_open = player.lvl_2_open;
        lvl_0_completed = player.lvl_0_completed;
        lvl_1_completed = player.lvl_1_completed;
        lvl_2_completed = player.lvl_2_completed;

        que_01_00_smith_open = player.que_01_00_smith_open;
        que_01_00_smith_completed = player.que_01_00_smith_completed;
        que_01_01_smith_open = player.que_01_01_smith_open;
        que_01_01_smith_completed = player.que_01_01_smith_completed;
        
        que_01_00_reaper_open = player.que_01_00_reaper_open;
        que_01_00_reaper_completed = player.que_01_00_reaper_completed;
        que_01_01_reaper_open = player.que_01_01_reaper_open;
        que_01_01_reaper_completed = player.que_01_01_reaper_completed;
        
        que_01_00_alchemist_open = player.que_01_00_alchemist_open;
        que_01_00_alchemist_completed = player.que_01_00_alchemist_completed;
        que_01_01_alchemist_open = player.que_01_01_alchemist_open;
        que_01_01_alchemist_completed = player.que_01_01_alchemist_completed;
        
        lvl_0_shard_picked = player.lvl_0_shard_picked;
        lvl_0_flower_picked = player.lvl_0_flower_picked;
        lvl_0_scroll_picked = player.lvl_0_scroll_picked;
        
        lvl_1_shard_picked = player.lvl_1_shard_picked;
        lvl_1_flower_picked = player.lvl_1_flower_picked;
        lvl_1_scroll_picked = player.lvl_1_scroll_picked;
    }
}