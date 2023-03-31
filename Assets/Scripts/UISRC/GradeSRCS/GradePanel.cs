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

    private TextMeshProUGUI lvl;
    private TextMeshProUGUI hp;
    private TextMeshProUGUI sp;
    private TextMeshProUGUI pieces;
    private TextMeshProUGUI piecesCost;

    private PlayerChara stats;

    private CameraFollow cam;

    private bool isGet = false;

    void OnEnable()
    {
        if (isGet)
        {
            PrintText();
        }
    }

    private void Start()
    {
        GetObj();
        PrintText();
        isGet = true;
    }

    void GetObj()
    {
        //Btn find
        hpGrade = GameObject.Find("HealthBtn").GetComponent<Button>();
        spGrade = GameObject.Find("StaminaBtn").GetComponent<Button>();
        conf = GameObject.Find("ConfBtn").GetComponent<Button>();

        //text Find
        lvl = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        hp = GameObject.Find("HPMax").GetComponent<TextMeshProUGUI>();
        sp = GameObject.Find("SPMax").GetComponent<TextMeshProUGUI>();
        pieces = GameObject.Find("Pieces").GetComponent<TextMeshProUGUI>();
        piecesCost = GameObject.Find("Cost").GetComponent<TextMeshProUGUI>();

        // Player Stats Find
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerChara>();

        //Camera find
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();

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
