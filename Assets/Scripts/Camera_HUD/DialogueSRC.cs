using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class DialogueSRC : MonoBehaviour
{
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
    
    void Start()
    {
        bttnList = new List<GameObject>();
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
    
    public IEnumerator DialogueSrc(TextAsset textFile)
    {
        camSCR.BlackScreenTransparency(1);
        yield return new WaitForSeconds(1.5f);
        camSCR.BlackScreenTransparency(2);
        camSCR.changeCanvas(3);
        var text = textFile.text;
        var name = getBetween(text, "name:", "\r");
        var message = getBetween(text, "msg_00:", "\r");
        var spr_path = getBetween(text, "sprite:", "\r");
        var bg_path = getBetween(text, "bg:", "\r");
        var que_01 = getBetween(text, "que_01_00:", "\r");
        var que_02 = getBetween(text, "que_02_00:", "\r");
        var que_exit = getBetween(text, "que_exit:", "\r");
        string[] que = new[] { que_01, que_02, que_exit };
        Sprite spr = LoadSpriteFromFile(spr_path);
        Sprite bg = LoadSpriteFromFile(bg_path);
        spriteObj.GetComponent<Image>().sprite = spr;
        bgObj.GetComponent<Image>().sprite = bg;
        messageObj.GetComponent<TextMeshProUGUI>().text = message;
        namObj.GetComponent<TextMeshProUGUI>().text = name;
        quePrint(que);
    }

    private void quePrint(string[] que)
    {
        var posDiff = 0;
        for (int i = 0; i < que.Length; i++)
        {
            var posVec = new Vector3(buttonPos.transform.position.x, buttonPos.transform.position.y - posDiff, 0);
            GameObject bttnSay = Instantiate(sayButtonPrefab, posVec, quaternion.identity, heroSayObj.transform);
            posDiff += 1;
            bttnList.Add(bttnSay);
            if (i == que.Length - 1)
            {
                bttnSay.AddComponent<LeaveDialogue>();
            }
            bttnSay.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = que[i];
        }
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
