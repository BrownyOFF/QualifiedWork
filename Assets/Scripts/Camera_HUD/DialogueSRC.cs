using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class DialogueSRC : MonoBehaviour
{
    private PlayerClass player;
    private Camera cam;
    private GameObject Dialogue;
    private CameraFollow camSCR;
    private GameObject messageObj;
    private GameObject namObj;
    private GameObject spriteObj;
    private GameObject bgObj;
    private GameObject heroSayObj;
    private GameObject sayButtonPrefab;
    private GameObject buttonPos;
    public List<GameObject> bttnList;
    private List<string> que;
    private List<string> queType;
    private string messageText;
    private string nameStr;
    private string itemID;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerClass>();
        sayButtonPrefab = Resources.Load("sayBttn") as GameObject;
        cam = GetComponent<Camera>();
        Dialogue = GameObject.Find("DialogueUI");
        camSCR = GetComponent<CameraFollow>();
        spriteObj = Dialogue.transform.GetChild(1).gameObject;
        bgObj = Dialogue.transform.GetChild(0).gameObject;
        namObj = Dialogue.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).gameObject;
        messageObj = Dialogue.transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).gameObject;
        heroSayObj = Dialogue.transform.GetChild(3).gameObject;
        buttonPos = heroSayObj.transform.GetChild(0).gameObject;
    }
    
    void Update()
    {
        
    }
    
    public IEnumerator DialogueSrc(TextAsset textFile, string queID)
    {
        que = new List<string>();
        queType = new List<string>();
        bttnList = new List<GameObject>();
        camSCR.BlackScreenTransparency(1);
        yield return new WaitForSeconds(1.5f);
        camSCR.BlackScreenTransparency(2);
        camSCR.changeCanvas(3);
        var text = textFile.text;
        nameStr = getBetween(text, "name:", "\r");
        itemID = getBetween(text, "itemID:", "\r");
        var message = getBetween(text, "msg_00:", "\r");
        var spr_path = getBetween(text, "sprite:", "\r");
        var bg_path = getBetween(text, "bg:", "\r");

        switch (nameStr)
        {
            case "Кузнець":
                if (player.que_01_00_smith_open && !player.que_01_00_smith_completed)
                {
                    var que_01 = getBetween(text, "que_01_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_01_00");
                }
                else if (player.que_01_01_smith_open && !player.que_01_01_smith_completed)
                {
                    var que_01 = getBetween(text, "que_02_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_02_00");
                }
                if (player.amount[player.inv.IndexOf(0)] > 0)
                {
                    var que_grade = getBetween(text, "que_grade_00:", "\r");
                    que.Add(que_grade);
                    queType.Add("que_grade_00");
                }
                break;
            case "Алхімік":
                if (player.que_01_00_alchemist_open && !player.que_01_00_alchemist_completed)
                {
                    var que_01 = getBetween(text, "que_01_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_01_00");
                }
                else if (player.que_01_01_alchemist_open && !player.que_01_01_alchemist_completed)
                {
                    var que_01 = getBetween(text, "que_02_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_02_00");
                }
                if (player.amount[player.inv.IndexOf(1)] > 0)
                {
                    var que_grade = getBetween(text, "que_grade_00:", "\r");
                    que.Add(que_grade);
                    queType.Add("que_grade_00");
                }
                break;
            case "Жнець полювання":
                if (player.que_01_00_reaper_open && !player.que_01_00_reaper_completed)
                {
                    var que_01 = getBetween(text, "que_01_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_01_00");
                }
                else if (player.que_01_01_reaper_open && !player.que_01_01_reaper_completed)
                {
                    var que_01 = getBetween(text, "que_02_00:", "\r");
                    que.Add(que_01);
                    queType.Add("que_02_00");
                }
                if (player.amount[player.inv.IndexOf(5)] > 0)
                {
                    var que_grade = getBetween(text, "que_grade_00:", "\r");
                    que.Add(que_grade);
                    queType.Add("que_grade_00");
                }
                break;
        }
        
        var que_ex = getBetween(text, "que_exit:", "\r");
        que.Add(que_ex);
        queType.Add("que_exit");
        
        Sprite spr = LoadSpriteFromFile(spr_path);
        Sprite bg = LoadSpriteFromFile(bg_path);
        spriteObj.GetComponent<Image>().sprite = spr;
        bgObj.GetComponent<Image>().sprite = bg;
        messageObj.GetComponent<TextMeshProUGUI>().text = message;
        namObj.GetComponent<TextMeshProUGUI>().text = name;
        quePrint(que, queType, text);
    }

    private void quePrint(List<string> que, List<string> queID, string textFile)
    {
        var posDiff = 0;
        for (int i = 0; i < que.Count; i++)
        {
            var posVec = new Vector3(buttonPos.transform.position.x, buttonPos.transform.position.y - posDiff, 0);
            GameObject bttnSay = Instantiate(sayButtonPrefab, posVec, quaternion.identity, heroSayObj.transform);
            posDiff += 1;
            bttnList.Add(bttnSay);
            if (queID[i].Contains("exit"))
            {
                bttnSay.AddComponent<LeaveDialogue>();
            }
            else if (queID[i].Contains("grade"))
            {
                bttnSay.AddComponent<BttnDialogueSRC>();
                bttnSay.GetComponent<BttnDialogueSRC>().bttnCount = i;
                bttnSay.GetComponent<BttnDialogueSRC>().messageID = queID[i];
                bttnSay.GetComponent<BttnDialogueSRC>().textString = getAllDialogue(queID[i], textFile);
                bttnSay.GetComponent<BttnDialogueSRC>().messageObj = messageObj;
                bttnSay.GetComponent<BttnDialogueSRC>().bttnList = bttnList;
                bttnSay.GetComponent<BttnDialogueSRC>().isGrade = true;
                bttnSay.GetComponent<BttnDialogueSRC>().name = nameStr;
            }
            else
            {
                bttnSay.AddComponent<BttnDialogueSRC>();
                bttnSay.GetComponent<BttnDialogueSRC>().bttnCount = i;
                bttnSay.GetComponent<BttnDialogueSRC>().messageID = queID[i];
                bttnSay.GetComponent<BttnDialogueSRC>().textString = getAllDialogue(queID[i], textFile);
                bttnSay.GetComponent<BttnDialogueSRC>().messageObj = messageObj;
                bttnSay.GetComponent<BttnDialogueSRC>().bttnList = bttnList;
                bttnSay.GetComponent<BttnDialogueSRC>().isGrade = false;
                bttnSay.GetComponent<BttnDialogueSRC>().idOpen = itemID;
                bttnSay.GetComponent<BttnDialogueSRC>().isText = true;
            }
            bttnSay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = que[i];
        }
    }

    public string getAllDialogue(string messID, string text)
    {
        int lstDgt = 1;
        messageText = "";
        string id = getBetween(messID, "que_", "_0");
        while (true)
        {
            var tmpText = getBetween(text, "mess_" + id + "_0" + lstDgt + ":", "\r");
            if (tmpText == "")
            {
                break;
            }
            messageText += tmpText + "\r\n";
            lstDgt++;
        }
        return messageText;
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
    public Sprite LoadSpriteFromFile(string path)
    {
        // Load the texture from the file
        Texture2D texture = LoadTextureFromFile(path);

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        return sprite;
    }
    public Texture2D LoadTextureFromFile(string path)
    {
        // Load the image data from the file
        byte[] imageData = System.IO.File.ReadAllBytes(path);

        // Create a new texture and load the image data into it
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        return texture;
    }
}
