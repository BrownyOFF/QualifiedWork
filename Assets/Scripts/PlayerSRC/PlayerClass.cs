using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    
    [Serializable]
    public class Player
    {
        #region PlayerChara
        public float hpPerc = 1f;
        public float spPerc = 1f;
        public float piecesToGrade = 10f;
        public float pieces = 0;
        public int level = 1;
        public Vector3 resPos = Vector3.zero;
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

        public void setVars(float hp, float sp, int lvl, float dmg, float piecesGrade, float piece, List<int>invent, List<int> am, Vector3 pos,int flask)
        {
            hpPerc = hp;
            spPerc = sp;
            level = lvl;
            damage = dmg;
            piecesToGrade = piecesGrade;
            pieces = piece;
            inv = invent;
            amount = am;
            resPos = pos;
            flaskMax = flask;
        }
    }

    public Player player;
    void Start()
    {
        player = new Player();
        player.inv = new List<int>();
        player.amount = new List<int>();
        GameObject.FindWithTag("saveManager").GetComponent<SaveManager>().Load();
        PlayerPrefs.SetInt("IsLoaded", 1);
    }

    
    void Update()
    {
        
    }
}
