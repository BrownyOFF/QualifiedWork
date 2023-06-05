using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI hp;
    [SerializeField] private TextMeshProUGUI sp;
    [SerializeField] private TextMeshProUGUI pieces;

    [SerializeField] private PlayerChara playerStats;

    public PlayerClass playerClass;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerChara>();
        playerClass = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();

    }
    
    void Update()
    {
        hp.text = playerStats.hpCurrent.ToString();
        sp.text = playerStats.spCurrent.ToString();
        var pie = playerClass.pieces.ToString(); 
        pieces.text = "Pieces: " + pie;
    }
}
