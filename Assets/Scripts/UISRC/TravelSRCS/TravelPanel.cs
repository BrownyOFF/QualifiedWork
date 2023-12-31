using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class TravelPanel : MonoBehaviour
{
    public TextAsset lvl_names;
    private string lvl_names_string;
    public TextAsset shrine_names;
    public List<GameObject> lvl_bttns_pos;
    public List<GameObject> lvl_bttns;
    public List<string> scenes_list;

    private SaveClass save;

    public GameObject bttn_granted;
    public GameObject bttn_denied;
    public GameObject conf;
    public GameObject lvl_image;

    public string lvl_chosen;

    public PlayerClass player;
    private bool created = false;

    private void OnEnable()
    {
        if (!player.lvl_1_open)
        {
            lvl_bttns[2].SetActive(false);
        }

        if (!player.lvl_2_open)
        {
            lvl_bttns[3].SetActive(false);
        }
    }

    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        bttn_granted = Resources.Load("lvl_bttn_granted") as GameObject;
        bttn_denied = Resources.Load("lvl_bttn_denied") as GameObject;
        conf = GameObject.Find("conf_bttn_travel");
        lvl_image = GameObject.Find("lvl_image");
        conf.SetActive(false);
        lvl_image.SetActive(false);
        scenes_list = Get_all_lvls();
        lvl_bttns_pos = new List<GameObject>();
        
        var tmp = transform.GetChild(0);
        foreach (Transform child in tmp)
        {
            lvl_bttns_pos.Add(child.gameObject);
        }

        lvl_names_string = lvl_names.ToString();

        save = GameObject.FindWithTag("lvlScr").GetComponent<LevelCreate>().data;
        CreateBttns();
    }
    public void CreateBttns()
    {
        var posVec = new Vector2(lvl_bttns_pos[0].transform.position.x, lvl_bttns_pos[0].transform.position.y);
        lvl_bttns.Add(Instantiate(bttn_granted, posVec, Quaternion.identity));
        var lvl_name = getBetween(lvl_names_string, 0 + ":", "\r");
        lvl_bttns[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lvl_name;
        lvl_bttns[0].GetComponent<Lvl_bttnSRC>().lvl_name = "lvl_0";
        
        lvl_bttns[0].transform.SetParent(this.transform);

        posVec = new Vector2(lvl_bttns_pos[1].transform.position.x, lvl_bttns_pos[1].transform.position.y);
        lvl_bttns.Add(Instantiate(bttn_granted, posVec, Quaternion.identity));
        lvl_name = getBetween(lvl_names_string,  "hub:", "\r");
        lvl_bttns[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lvl_name;
        lvl_bttns[1].GetComponent<Lvl_bttnSRC>().lvl_name = "lvl_hub";
        
        lvl_bttns[1].transform.SetParent(this.transform);
        
        posVec = new Vector2(lvl_bttns_pos[2].transform.position.x, lvl_bttns_pos[2].transform.position.y);
        lvl_bttns.Add(Instantiate(bttn_granted, posVec, Quaternion.identity));
        lvl_name = getBetween(lvl_names_string, 1 + ":", "\r");
        lvl_bttns[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lvl_name;
        lvl_bttns[2].GetComponent<Lvl_bttnSRC>().lvl_name = "lvl_1";
        
        lvl_bttns[2].transform.SetParent(this.transform);

        posVec = new Vector2(lvl_bttns_pos[3].transform.position.x, lvl_bttns_pos[3].transform.position.y);
        lvl_bttns.Add(Instantiate(bttn_granted, posVec, Quaternion.identity));
        lvl_name = getBetween(lvl_names_string, 2 + ":", "\r");
        lvl_bttns[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lvl_name;
        lvl_bttns[3].GetComponent<Lvl_bttnSRC>().lvl_name = "lvl_2";

        lvl_bttns[3].transform.SetParent(this.transform);
    }
    
    private List<string> Get_all_lvls()
    {
        List<string> tmpList = new List<string>();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName.Contains("lvl"))
            {
                tmpList.Add(sceneName);
            }
        }

        return tmpList;
    }
    public static string getBetween(string file, string strStart, string strEnd)
    {
        if (file.Contains(strStart) && file.Contains(strEnd))
        {
            int Start, End;
            Start = file.IndexOf(strStart, 0) + strStart.Length;
            End = file.IndexOf(strEnd, Start);
            return file.Substring(Start, End - Start);
        }

        return "";
    }

}
