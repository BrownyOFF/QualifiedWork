using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestButton : MonoBehaviour
{
    //enemyRelated
    private GameObject[] enemys;
    private GameObject levelCreateSRC;
    
    //PlayerRelated
    private PlayerChara player;
    
    private Button bttn;
    void Start()
    {
        levelCreateSRC = GameObject.FindGameObjectWithTag("lvlScr");
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerChara>();
    }

    private void onClickFunc()
    {
        Debug.Log("Button Clicked");
        //destroy all current enemy's
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys != null)
        {
            foreach (var i in enemys)
            {
                i.GetComponent<EnemyStat>().destroy();
            }
        }

        //spawn them again
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Enemy, levelCreateSRC.GetComponent<LevelCreate>().EnemyPos);
        
        //Player reset stats
        player.assignStats();
    }
    void Update()
    {
        
    }
}
