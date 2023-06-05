using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClass : MonoBehaviour
{
        #region PlayerChara
        public float hpMax = 100f;
        public float spMax = 50f;
        public float piecesToGrade = 10f;
        public float pieces = 0;
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

        public string currScene;

        public PlayerClass(float hp, float sp, int lvl, float dmg, float piecesGrade, float piece, List<int>invent, List<int> am, int flask)
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
        }
        
        
}
