using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GradePanel : MonoBehaviour
{
    private Button hpGrade;
    private Button spGrade;
    private Button conf;

    private Text lvl;
    private Text hp;
    private Text sp;
    private Text pieces;
    private Text piecesCost;

    private PlayerChara stats;

    private CameraFollow cam;
    
    void Start()
    {
        //btn Find
        hpGrade = GameObject.Find("HealthBtn").GetComponent<Button>();
        spGrade = GameObject.Find("StaminaBtn").GetComponent<Button>();
        conf = GameObject.Find("ConfBtn").GetComponent<Button>();

        //text Find
        lvl = GameObject.Find("Level").GetComponent<Text>();
        hp = GameObject.Find("HPMax").GetComponent<Text>();
        sp = GameObject.Find("SPMax").GetComponent<Text>();
        pieces = GameObject.Find("Pieces").GetComponent<Text>();
        piecesCost = GameObject.Find("Cost").GetComponent<Text>();
        
        // Player Stats Find
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerChara>();
        
        //Camera find
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        
        PrintText();
        
        //Btn Listeners
        hpGrade.onClick.AddListener(HpGrade);
        spGrade.onClick.AddListener(SpGrade);
        conf.onClick.AddListener(ConfAction);
    }

    void PrintText()
    {
        hp.text = "Health: " + stats.hpMax;
        sp.text = "Stamina: " + stats.spMax;
        lvl.text = "Level: " + stats.level;
        pieces.text = "Pieces: " + stats.pieces;
        piecesCost.text = "Cost: " + stats.piecesToGrade;
    }

    void HpGrade()
    {
        if (stats.pieces < stats.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            stats.hpPerc += 0.2f;
            stats.CalcStats();
            stats.assignStats();
            stats.pieces -= stats.piecesToGrade;
            stats.piecesToGrade += stats.piecesToGrade / 2;
            stats.level += 1;
            PrintText();
        }
    }

    void SpGrade()
    {
        if (stats.pieces < stats.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            stats.spPerc += 0.2f;
            stats.CalcStats();
            stats.assignStats();
            stats.pieces -= stats.piecesToGrade;
            stats.piecesToGrade += stats.piecesToGrade / 2;
            stats.level += 1;
            PrintText();
        }
    }

    void ConfAction()
    {
        cam.changeCanvas(1);
    }
}
