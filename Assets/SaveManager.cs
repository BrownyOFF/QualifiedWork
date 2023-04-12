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
        public void Assign(float hp, float sp, int lvl, float dmg, float piecesGrade, float piece, List<int>invent, List<int> am, GameObject pos)
        {
            hpPerc = hp;
            spPerc = sp;
            level = lvl;
            damage = dmg;
            piecesToGrade = piecesGrade;
            pieces = piece;
            inv = invent;
            amount = am;
            resPos = pos.transform.position;
            currScene = SceneManager.GetActiveScene().name;
        }

        public void ReturnStats(float hp1, float sp1, int lvl1, float dmg1, float piecesGrade1, float piece1, List<int>invent1, List<int> am1, GameObject pos1)
        {
            hp1 = hpPerc;
            sp1 = spPerc;
            lvl1 = level;
            dmg1 = damage;
            piecesGrade1 = piecesToGrade;
            piece1 = pieces;
            invent1 = inv;
            am1 = amount;
            pos1.transform.position = resPos;
        }
    }

    private DataSave saveData;
    void Start()
    {
        path = Application.persistentDataPath + "/save.json";
        saveData = new DataSave();
        chara = GameObject.FindWithTag("Player").GetComponent<PlayerChara>();
        inv = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        fight = GameObject.FindWithTag("Player").GetComponent<FightBehaviour>();
        if (PlayerPrefs.GetInt("IsLoaded") == 0)
        {
            Load();
            PlayerPrefs.SetInt("IsLoaded", 1);
        }
    }

    public void Load()
    {
        try
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<DataSave>(json);
            //saveData.ReturnStats(chara.hpPerc, chara.spPerc, chara.level, fight.damage, chara.piecesToGrade,chara.pieces, inv.inv, inv.amount, chara.RespawnPoint);
            chara.hpPerc = saveData.hpPerc;
            chara.spPerc = saveData.spPerc;
            chara.level = saveData.level;
            fight.damage = saveData.damage;
            chara.piecesToGrade = saveData.piecesToGrade;
            chara.pieces = saveData.pieces;
            inv.inv = saveData.inv;
            inv.amount = saveData.amount;
            chara.RespawnPoint.transform.position = saveData.resPos;
            
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }

    public void Save()
    {
        saveData.Assign(chara.hpPerc, chara.spPerc, chara.level, fight.damage, chara.piecesToGrade,chara.pieces, inv.inv, inv.amount, chara.RespawnPoint);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }
    void Update()
    {
        
    }
}
