using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shrine_bttnSRC : MonoBehaviour
{
    private Button bttn;
    public string shrine_name;
    public string scene_name;
    private GameObject player;
    private GameObject cam;
    private GameObject lvl_src;
    void Start()
    {
        lvl_src = GameObject.FindWithTag("lvlScr");
        cam = GameObject.FindWithTag("MainCamera");
        bttn = GetComponent<Button>();
        player = GameObject.FindWithTag("Player");
        bttn.onClick.AddListener(OnCLickFunc);
    }

    void OnCLickFunc()
    {
        var currScene = SceneManager.GetActiveScene().name;
        if (currScene == scene_name)
        {
            StartCoroutine(TPPlayer());
        }
        else
        {
            StartCoroutine(ChangeLevel());
        }
    }

    IEnumerator ChangeLevel()
    {
        cam.GetComponent<CameraFollow>().BlackScreenTransparency(1);
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerChara>().inDialogue = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<FightBehaviour>().enabled = true;
        var tmpId = shrine_name[shrine_name.Length - 1];
        int tmp = tmpId - '0';
        PlayerPrefs.SetInt("PlayerPos", tmp);
        PlayerPrefs.Save();
        SceneManager.LoadScene(scene_name);
    }
    
    
    IEnumerator TPPlayer()
    {
        var shrine = GameObject.Find(shrine_name);
        if (shrine != null)
        {
            //destroy enemy's
            lvl_src.GetComponent<LevelCreate>().forEachEnemyFindDestroy();
            //spawn them again
            lvl_src.GetComponent<LevelCreate>().foreachCycle(lvl_src.GetComponent<LevelCreate>().Enemy, lvl_src.GetComponent<LevelCreate>().EnemyPos);
            
            cam.GetComponent<CameraFollow>().BlackScreenTransparency(1);
            yield return new WaitForSeconds(1f);
            player.GetComponent<PlayerChara>().inDialogue = false;
            player.transform.position = shrine.transform.GetChild(1).position;
            player.GetComponent<PlayerChara>().RespawnPointAssign(shrine.transform.GetChild(1).gameObject);
            cam.transform.position = shrine.transform.position;
            cam.GetComponent<CameraFollow>().BlackScreenTransparency(0);
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            GameObject.FindWithTag("Player").GetComponent<FightBehaviour>().enabled = true;
        }
    }
    void Update()
    {
        
    }
}
