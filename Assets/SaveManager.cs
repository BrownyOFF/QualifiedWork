using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using UnityEngine.SceneManagement;
using File = System.IO.File;

public class SaveManager : MonoBehaviour
{
    private string path;
    private PlayerChara chara;
    private PlayerInventory inv;
    private FightBehaviour fight;
    
    [System.Serializable]
    public class DataSave
    {
        public float hpPerc;
        public float spPerc;
        public int level;
        public float damage;
        public float piecesToGrade;
        public float pieces;
        public List<int> inv;
        public List<int> amount;
        public Vector3 resPos;
        public string currScene;
        public int flaskMax = 2;
        public void Assign(float hp, float sp, int lvl, float dmg, float piecesGrade, float piece, List<int>invent, List<int> am, Vector3 pos,int flask)
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
            currScene = SceneManager.GetActiveScene().name;
        }

        public void ReturnStats(float hp1, float sp1, int lvl1, float dmg1, float piecesGrade1, float piece1, List<int>invent1, List<int> am1, Vector3 pos1, int flask1)
        {
            hp1 = hpPerc;
            sp1 = spPerc;
            lvl1 = level;
            dmg1 = damage;
            piecesGrade1 = piecesToGrade;
            piece1 = pieces;
            invent1 = inv;
            am1 = amount;
            pos1 = resPos;
            flask1 = flaskMax;
        }
    }

    private DataSave saveData;
    public PlayerClass playerClass;
    void Start()
    {
        saveData = new DataSave();
        chara = GameObject.FindWithTag("Player").GetComponent<PlayerChara>();
        path = Application.persistentDataPath + "/save.json";
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/save.json");
        saveData = JsonUtility.FromJson<DataSave>(json);
        playerClass = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        playerClass.player.setVars(saveData.hpPerc, saveData.spPerc, saveData.level, saveData.damage, saveData.piecesToGrade,saveData.pieces, saveData.inv, saveData.amount, saveData.resPos,saveData.flaskMax);
        chara.CalcStats();
        chara.assignStats();
    }

    public void Save()
    {
        saveData.Assign(playerClass.player.hpPerc, playerClass.player.spPerc, playerClass.player.level, playerClass.player.damage, playerClass.player.piecesToGrade,playerClass.player.pieces, playerClass.player.inv, playerClass.player.amount, playerClass.player.resPos, playerClass.player.flaskMax);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }
    void Update()
    {
        
    }
}
