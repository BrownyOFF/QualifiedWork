using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerInventory : MonoBehaviour
{
    private PlayerChara chara;
    private PlayerMovement move;
    private FightBehaviour fight;
    private PlayerClass playerClass;
    private GameObject itemBar;
    private Transform posItemUse;
    public int quickCurrent = 0;
    private GameObject knife;
    private GameObject bomb;
    public TextAsset itemsList;
    public TextAsset itemsSprite;
    private string itemsListString;

    public AudioSource bombsfx;
    public AudioSource knifesfx;
    public AudioSource potionsfx;
    void Start()
    {
        playerClass = GetComponent<PlayerClass>();
        knife = Resources.Load("Knife") as GameObject;
        bomb = Resources.Load("Bomb") as GameObject;
        posItemUse = gameObject.transform.GetChild(1).transform;
        itemBar = GameObject.Find("itemBar");
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        fight = GetComponent<FightBehaviour>();
        itemsListString = itemsList.text;
        if (!playerClass.inv.Contains(2))
        {
            playerClass.inv.Add(2);
            playerClass.amount.Add(playerClass.flaskMax);
        }
        ChangeSprite();
    }
    public void TakeItem(int id)
    {
        if (playerClass.inv.Contains(id))
        {
            int i = playerClass.inv.IndexOf(id);
            playerClass.amount[i]++;
        }else
        {
            playerClass.inv.Add(id);
            playerClass.amount.Add(1);
        }
    }

    public void UseItem(int index)
    {
        var tmpID = playerClass.inv.IndexOf(index);
        if (playerClass.inv[index] == 2 && playerClass.amount[index] > 0)
        {
            chara.hpCurrent += 25;
            potionsfx.Play();
            playerClass.amount[index]--;
        }
        else if (playerClass.inv[index] == 3 && playerClass.amount[index] > 0)
        {
            var tmppos = new Vector2(posItemUse.position.x, posItemUse.position.y);
            GameObject knife_throw = Instantiate(knife, tmppos, Quaternion.identity);
            knifesfx.Play();
            playerClass.amount[index]--;
        }
        else if (playerClass.inv[index] == 4 && playerClass.amount[index] > 0)
        {
            var tmppos = new Vector2(posItemUse.position.x, posItemUse.position.y);
            GameObject bomb_throw = Instantiate(bomb, tmppos, Quaternion.identity);
            playerClass.amount[index]--;
            bombsfx.Play();
        }
        ChangeSprite();
    }

    public void ResetFlask()
    {
        playerClass.amount[playerClass.inv.IndexOf(2)] = playerClass.flaskMax;
    }

    private void ChangeItem(int n)
    {
        if (quickCurrent == playerClass.inv.Count-1 && n == 1)
        {
            quickCurrent = 0;
            return;
        }
        else if (quickCurrent == 0 && n == -1)
        {
            quickCurrent = playerClass.inv.Count-1;
            return;
        }

        quickCurrent += n;
    }
    void Update()
    {
        if (GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>().inPause)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(playerClass.inv);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !fight.isAttacking && !fight.isBlocking)
        {
            UseItem(quickCurrent);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
        {
            ChangeItem(1);
            ChangeSprite();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            ChangeItem(-1);
            ChangeSprite();
        }
    }

    private void ChangeSprite()
    {
        var tmp = playerClass.inv[quickCurrent];
        var tmp2 = playerClass.inv.IndexOf(tmp);
        var text = itemsSprite.text;
        var spr_path = getBetween(text,playerClass.inv[quickCurrent].ToString() + ":" , "\r");
        Sprite itmSpr = LoadSpriteFromFile(spr_path);
        itemBar.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = playerClass.amount[tmp2].ToString();
        itemBar.transform.GetChild(1).GetComponent<Image>().sprite = itmSpr;
    }
    private static string getBetween(string file, string strStart, string strEnd)
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
    private Sprite LoadSpriteFromFile(string path)
    {
        // Load the texture from the file
        Texture2D texture = LoadTextureFromFile(path);

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        return sprite;
    }
    private Texture2D LoadTextureFromFile(string path)
    {
        // Load the image data from the file
        byte[] imageData = System.IO.File.ReadAllBytes(path);

        // Create a new texture and load the image data into it
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        return texture;
    }
}