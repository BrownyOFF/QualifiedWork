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
    private string shrine_names_string;
    public List<GameObject> lvl_bttns;
    private List<GameObject> shrine_bttns;
    public List<string> scenes_list;

    void Start()
    {
        scenes_list = Get_all_lvls();
        Debug.Log(scenes_list);
        shrine_bttns = new List<GameObject>();
        lvl_bttns = new List<GameObject>();
        
        var tmp = transform.GetChild(0);
        foreach (Transform child in tmp)
        {
            lvl_bttns.Add(child.gameObject);
        }

        tmp = transform.GetChild(1);
        foreach (Transform child in tmp)
        {
            shrine_bttns.Add(child.gameObject);
        }

        SetShrineFalse();
            
        lvl_names_string = lvl_names.ToString();
        shrine_names_string = shrine_names.ToString();
        
        Print_lvl_names();
    }

    public void SetShrineFalse()
    {
        foreach (var i in shrine_bttns)
        {
            i.SetActive(false);
        }
    }
    public void Print_shrine_names(string id)
    {
        int countTMP = 0;
        foreach (var i in shrine_bttns)
        {
            var tmpName = getBetween(shrine_names_string, id + "_" + countTMP + ":", "\r");
            if (tmpName == "")
            {
                break;
            }
            i.SetActive(true);
            i.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tmpName;
            i.GetComponent<Shrine_bttnSRC>().scene_name = id;
            i.GetComponent<Shrine_bttnSRC>().shrine_name = id + "_" + countTMP;
            countTMP++;
        }
    }
    private void Print_lvl_names()
    {
        int count = 0;
        foreach (var i in scenes_list)
        {
            var lvl_name = getBetween(lvl_names_string, i + ":", "\r");
            lvl_bttns[count].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = lvl_name;
            lvl_bttns[count].GetComponent<Lvl_bttnSRC>().lvl_name = i;
            count++;
        }
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
    void Update()
    {
        
    } public static string getBetween(string file, string strStart, string strEnd)
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
