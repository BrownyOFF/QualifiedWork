using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BttnDialogueSRC : MonoBehaviour
{
    public string messageID;
    public string textString;
    public GameObject messageObj;
    public int bttnCount;
    private Button bttn;
    public List<GameObject> bttnList;
    public bool isGrade;
    public bool isText;
    public string name;
    public string idOpen;
    private bool isPrinting = false;
    private int strCount = 1;
    private bool isClicked = false;
    void Start()
    {
        Debug.Log(textString);
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
    }
    
    private IEnumerator printText()
    {
        var text = GetLine(textString, strCount);
        if (text == "")
        {
            EndButton();
        }
        isPrinting = true;
        for (int i = 0; i < text.Length; i++)
        {
            messageObj.GetComponent<TextMeshProUGUI>().text += text[i];
            yield return new WaitForSeconds(0.01f);
        }
        isPrinting = false;
        strCount++;
    }
    private void onClickFunc()
    {
        if (isClicked)
        {
            return;
        }

        if (isText)
        {
            switch (idOpen)
            {
                case "0":
                    if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_smith_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_smith_completed = true;
                    }
                    else if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_smith_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_smith_completed = true;
                    }
                    break;
                case "5":
                    if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_reaper_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_reaper_completed = true;
                    }
                    else if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_reaper_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_reaper_completed = true;
                    }
                    break;
                case "1":
                    if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_alchemist_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_00_alchemist_completed = true;
                    }
                    else if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_alchemist_completed)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().que_01_01_alchemist_completed = true;
                    }
                    break;
            }
            isText = false;
        }
        
        if (isGrade)
        {
            switch (name)
            {
                case "Кузнець":
                    GameObject.FindWithTag("Player").GetComponent<PlayerClass>().damage += 5;
                    GameObject.FindWithTag("Player").GetComponent<PlayerClass>()
                        .amount[GameObject.FindWithTag("Player").GetComponent<PlayerClass>().inv.IndexOf(0)]--;
                    break;
                case "Жнець полювання":
                    if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_1_open)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_1_open = true;
                    }
                    else if (!GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_2_open)
                    {
                        GameObject.FindWithTag("Player").GetComponent<PlayerClass>().lvl_2_open = true;
                    }
                    GameObject.FindWithTag("Player").GetComponent<PlayerClass>()
                        .amount[GameObject.FindWithTag("Player").GetComponent<PlayerClass>().inv.IndexOf(5)]--;
                    break;
                case "Алхімік":
                    GameObject.FindWithTag("Player").GetComponent<PlayerClass>().flaskMax++;
                    GameObject.FindWithTag("Player").GetComponent<PlayerClass>()
                        .amount[GameObject.FindWithTag("Player").GetComponent<PlayerClass>().inv.IndexOf(1)]--;
                    break;
            }
            isGrade = false;
        }
        isClicked = true;
        messageObj.GetComponent<TextMeshProUGUI>().text = "";
        for (int i = 0; i < bttnList.Count; i++)
        {
            if (i != bttnCount)
            {
                bttnList[i].SetActive(false);
            }
        }
        StartCoroutine(printText());
    }
    private string GetLine(string text, int lineNo)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo-1] : null;
    }

    private void EndButton()
    {
        messageObj.GetComponent<TextMeshProUGUI>().text = "Щось ше??";
        for (int i = 0; i < bttnList.Count; i++)
        {
            if (i != bttnCount)
            {
                bttnList[i].SetActive(true);
            }
            else
            {
                bttnList[i].SetActive(false);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPrinting && isClicked)
        {
            messageObj.GetComponent<TextMeshProUGUI>().text = "";
            StartCoroutine(printText());
        }
    }
}
