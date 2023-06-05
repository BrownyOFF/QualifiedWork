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
    public float piecesToGrade;
    public float pieces;
    public string currScene;
    public float[] position;
    public int flaskMax;
    #endregion

    public SaveClass(PlayerClass player)
    {
        hpMax = player.hpMax;
        spMax = player.spMax;
        level = player.level;
        damage = player.damage;
        piecesToGrade = player.piecesToGrade;
        pieces = player.pieces;
        currScene = player.currScene;
        position = new float[3];
        position[0] = GameObject.FindWithTag("Player").transform.position.x;
        position[1] = GameObject.FindWithTag("Player").transform.position.y;
        position[2] = GameObject.FindWithTag("Player").transform.position.z;
        flaskMax = player.flaskMax;
    }
}