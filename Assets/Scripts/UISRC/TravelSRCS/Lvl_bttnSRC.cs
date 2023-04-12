using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl_bttnSRC : MonoBehaviour
{
    public string lvl_name;
    private Button bttn;
    [SerializeField] private GameObject panel_src; 
    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
    }

    private void onClickFunc()
    {
        panel_src.GetComponent<TravelPanel>().SetShrineFalse();
        foreach (var i in panel_src.GetComponent<TravelPanel>().lvl_bttns)
        {
            i.GetComponent<Image>().color = Color.gray;
        }
        bttn.GetComponent<Image>().color = Color.green; 
        panel_src.GetComponent<TravelPanel>().Print_shrine_names(lvl_name);
    }
    void Update()
    {
        
    }
}
