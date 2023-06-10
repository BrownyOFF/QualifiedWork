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
    private PlayerInventory inventory;
    private GameObject plClass;
    
    private Button bttn;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        levelCreateSRC = GameObject.FindGameObjectWithTag("lvlScr");
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerChara>();
        plClass = GameObject.FindGameObjectWithTag("Player");
    }

    private void onClickFunc()
    {
        Debug.Log("Button Clicked");
        //destroy all current enemy's
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        levelCreateSRC.GetComponent<LevelCreate>().forEachEnemyFindDestroy();
        //spawn them again
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().MushRoom, levelCreateSRC.GetComponent<LevelCreate>().MushRoomPos);
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Goblin, levelCreateSRC.GetComponent<LevelCreate>().GoblinPos);
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().Skeleton, levelCreateSRC.GetComponent<LevelCreate>().SkeletonPos);
        levelCreateSRC.GetComponent<LevelCreate>().foreachCycle(levelCreateSRC.GetComponent<LevelCreate>().FlyingEye, levelCreateSRC.GetComponent<LevelCreate>().FlyingEyePos);

        //Player reset stats
        player.assignStats();
        
        // Reset Flask
        inventory.ResetFlask();

        //save player
        SaveSystem.SavePlayer(plClass.GetComponent<PlayerClass>());
    }
    void Update()
    {
        
    }
}
