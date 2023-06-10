using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitTravel : MonoBehaviour
{
    public Button bttn;
    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(OnClickFunc);
    }

    void OnClickFunc()
    {
        GameObject[] bttns = GameObject.FindGameObjectsWithTag("travelBttn");
        foreach (var i in bttns)
        {
            Destroy(i);
        }
        GameObject.Find("Panel Travel").GetComponent<TravelPanel>().lvl_bttns_pos.Clear();
        GameObject.Find("Panel Travel").GetComponent<TravelPanel>().lvl_bttns.Clear();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().changeCanvas(1);
    }
    void Update()
    {
        
    }
}
