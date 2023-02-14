using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Text hp;
    [SerializeField] private Text sp;

    [SerializeField] private PlayerChara playerStats;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        hp = GameObject.Find("HP").GetComponent<Text>();
        sp = GameObject.Find("SP").GetComponent<Text>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerChara>();
    }
    
    void Update()
    {
        hp.text = playerStats.hp.ToString();
        sp.text = playerStats.sp.ToString();
    }
}
