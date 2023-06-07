using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfScript : MonoBehaviour
{
    private Button bttn;
    [SerializeField] private GameObject panel_src; 

    void Start()
    {
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
    }

    void onClickFunc()
    {
        StartCoroutine(ChangeLevel());
        
    }

    IEnumerator ChangeLevel()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().BlackScreenTransparency(1);
        GameObject.FindWithTag("Player").GetComponent<PlayerChara>().inDialogue = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("Player").GetComponent<FightBehaviour>().enabled = true;
        SaveSystem.SavePlayer(GameObject.FindWithTag("Player").GetComponent<PlayerClass>());
        GameObject.Find("LoadManager").GetComponent<Load>().LoadScene(panel_src.GetComponent<TravelPanel>().lvl_chosen);
        yield return null;
    }
}
