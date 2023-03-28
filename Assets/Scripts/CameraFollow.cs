using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class CameraFollow : MonoBehaviour
{
    public Animator anim;

    public int defaultScale = 8;
    public int battleScale = 6;
    private Transform target;
    public Vector3 offset;
    public float damping;
    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private GameObject Default;
    private GameObject Shrine;
    private GameObject Grade;
    private GameObject Dialogue;
    public GameObject YouDied;
    public GameObject blackScreen;

    public bool noDamping = false;

    private void Start()
    {
        Cursor.visible = false;
        
        Default = GameObject.Find("DefaultUI");
        Shrine = GameObject.Find("ShrineUI");
        Grade = GameObject.Find("GradeUI");
        Dialogue = GameObject.Find("DialogueUI");
        YouDied = GameObject.Find("Death");
        blackScreen = GameObject.Find("BlackScreen");
        anim = blackScreen.GetComponent<Animator>();
        YouDied.SetActive(false);
        changeCanvas(0);
        cam = GetComponent<Camera>();
        FindPlayer();
        changeScale(defaultScale);
    }

    public void FindPlayer()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    public void BlackScreenTransparency(int a)
    {
        if (a == 1)
        {
            anim.SetTrigger("Fade");
        }
        else
        {
            anim.SetTrigger("UnFade");

        }
    }
    public void changeCanvas(int i)
    {
        if (i == 0)
        {
            Shrine.SetActive(false);
            Default.SetActive(true);
            Grade.SetActive(false);
            Dialogue.SetActive(false);
            Cursor.visible = false;
        }
        else if (i == 1)
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(true);
            Dialogue.SetActive(false);
            Cursor.visible = true;
        }
        else if (i == 2)
        {
            Grade.SetActive(true);
            Default.SetActive(false);
            Shrine.SetActive(false);
            Dialogue.SetActive(false);
            Cursor.visible = true;
        }
        else if (i == 3)
        {
            Grade.SetActive(false);
            Default.SetActive(false);
            Shrine.SetActive(false);
            Dialogue.SetActive(true);
            Cursor.visible = true;
        }
    }
    private void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<PlayerChara>().isDead)
        {
            noDamping = true;
            return;
        }
        else if(noDamping && cam.transform.position != target.position)
        {
            noDamping = false;
            transform.position = target.position;
        }
        Vector3 movePos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
    }

    public IEnumerator DialogueSrc(TextAsset textFile)
    {
        BlackScreenTransparency(1);
        yield return new WaitForSeconds(1.5f);
        BlackScreenTransparency(2);
        changeCanvas(3);
        string text = textFile.text;
        string name = getBetween(text, "name:", "\r");
        string message = getBetween(text, "msg_00:", "\r");
        string spr_path = getBetween(text, "sprite:", "\r");
        string bg_path = getBetween(text, "bg:", "\r");
        Sprite spr = LoadSpriteFromFile(spr_path);
        Sprite bg = LoadSpriteFromFile(bg_path);
        Dialogue.transform.GetChild(1).GetComponent<Image>().sprite = spr;
        Dialogue.transform.GetChild(0).GetComponent<Image>().sprite = bg;
        Dialogue.transform.GetChild(2).transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            message;
        Dialogue.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            name;
        
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

    public void changeScale(int scale)
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, scale, Time.deltaTime);
    }
}
