using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = Microsoft.Unity.VisualStudio.Editor.Image;

public class UI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject hp;
    [SerializeField] private GameObject sp;
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
        hp.GetComponent<UnityEngine.UI.Image>().fillAmount = playerStats.hpCurrent / playerClass.hpMax;
        sp.GetComponent<UnityEngine.UI.Image>().fillAmount = playerStats.spCurrent / playerClass.spMax;

        var pie = playerClass.pieces.ToString(); 
        pieces.text = pie;
    }
}
