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
    private Button exit;

    
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
        exit = GameObject.Find("Exit").GetComponent<Button>();

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
        exit.onClick.AddListener(ConfAction);
        
    }
    void PrintText()
    {
        hp.text = playerClass.hpMax.ToString();
        sp.text = playerClass.spMax.ToString();
        lvl.text = "Рівень: " + playerClass.level.ToString();
        pieces.text = playerClass.pieces.ToString();
        piecesCost.text = "Вартість: " + playerClass.piecesToGrade.ToString();
    }

    void HpGrade()
    {
        if (playerClass.pieces < playerClass.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            playerClass.hpMax += 10f;
            stats.assignStats();
            playerClass.pieces -= playerClass.piecesToGrade;
            playerClass.piecesToGrade += 10;
            playerClass.level += 1;
            PrintText();
        }
    }

    void SpGrade()
    {
        if (playerClass.pieces < playerClass.piecesToGrade)
        {
            Debug.Log("Not Enough Pieces");
            return;
        }
        else
        {
            playerClass.spMax += 10f;
            stats.assignStats();
            playerClass.pieces -= playerClass.piecesToGrade;
            playerClass.piecesToGrade += playerClass.piecesToGrade / 2;
            playerClass.level += 1;
            PrintText();
        }
    }

    void ConfAction()
    {
        cam.changeCanvas(1);
    }
}
