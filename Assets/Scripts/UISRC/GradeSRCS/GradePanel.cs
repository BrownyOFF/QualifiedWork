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

    public TextMeshProUGUI lvl;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI sp;
    public TextMeshProUGUI pieces;
    public TextMeshProUGUI piecesCost;

    private PlayerChara stats;
    public PlayerClass playerClass;

    private CameraFollow cam;

    private bool isGet = false;

    void OnEnable()
    {
        PrintText();
    }

    private void Start()
    {
        GetObj();
        PrintText();
        isGet = true;
        playerClass = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
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
        playerClass = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        
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
        lvl.text = "Level: " + playerClass.player.level;
        pieces.text = "Pieces: " + playerClass.player.pieces;
        piecesCost.text = "Cost: " + playerClass.player.piecesToGrade;
    }

    void HpGrade()
    {
        if (playerClass.player.pieces < playerClass.player.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            playerClass.player.hpPerc += 0.2f;
            stats.CalcStats();
            stats.assignStats();
            playerClass.player.pieces -= playerClass.player.piecesToGrade;
            playerClass.player.piecesToGrade += playerClass.player.piecesToGrade / 2;
            playerClass.player.level += 1;
            PrintText();
        }
    }

    void SpGrade()
    {
        if (playerClass.player.pieces < playerClass.player.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            playerClass.player.spPerc += 0.2f;
            stats.CalcStats();
            stats.assignStats();
            playerClass.player.pieces -= playerClass.player.piecesToGrade;
            playerClass.player.piecesToGrade += playerClass.player.piecesToGrade / 2;
            playerClass.player.level += 1;
            PrintText();
        }
    }

    void ConfAction()
    {
        cam.changeCanvas(1);
    }
}
