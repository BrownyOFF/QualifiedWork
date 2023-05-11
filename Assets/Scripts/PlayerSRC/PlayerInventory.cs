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
    //public int flowerAmount;
    //public int shardAmount;
    public int quickCurrent = 0;
    // public int quickCurrentID = 3;
    //public List<int> inv;
    //public List<int> amount;
    private GameObject knife;
    private GameObject bomb;
    public TextAsset itemsList;
    public TextAsset itemsSprite;
    private string itemsListString;
    //private int flaskMax = 2;
    
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
        ChangeSprite();
    }
    public void TakeItem(int id)
    {
        if (playerClass.player.inv.Contains(id))
        {
            int i = playerClass.player.inv.IndexOf(id);
            playerClass.player.amount[i]++;
        }else
        {
            playerClass.player.inv.Add(id);
            playerClass.player.amount.Add(1);
        }
    }

    public void UseItem(int index)
    {
        var tmpID = playerClass.player.inv.IndexOf(index);
        if (playerClass.player.inv[index] == 2 && playerClass.player.amount[index] > 0)
        {
            chara.hpCurrent += 25;
            playerClass.player.amount[index]--;
        }
        else if (playerClass.player.inv[index] == 3 && playerClass.player.amount[index] > 0)
        {
            var tmppos = new Vector2(posItemUse.position.x, posItemUse.position.y);
            GameObject knife_throw = Instantiate(knife, tmppos, Quaternion.identity);
            playerClass.player.amount[index]--;
        }
        else if (playerClass.player.inv[index] == 4 && playerClass.player.amount[index] > 0)
        {
            var tmppos = new Vector2(posItemUse.position.x, posItemUse.position.y);
            GameObject bomb_throw = Instantiate(bomb, tmppos, Quaternion.identity);
            playerClass.player.amount[index]--;
        }
    }

    public void ResetFlask()
    {
        playerClass.player.amount[0] = playerClass.player.flaskMax;
    }

    private void ChangeItem(int n)
    {
        if (quickCurrent == playerClass.player.inv.Count-1 && n == 1)
        {
            quickCurrent = 0;
            return;
        }
        else if (quickCurrent == 0 && n == -1)
        {
            quickCurrent = playerClass.player.inv.Count-1;
            return;
        }

        quickCurrent += n;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(playerClass.player.inv);
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
        var tmp = playerClass.player.inv[quickCurrent];
        var tmp2 = playerClass.player.inv.IndexOf(tmp);
        var text = itemsSprite.text;
        var spr_path = getBetween(text,playerClass.player.inv[quickCurrent].ToString() + ":" , "\r");
        Sprite itmSpr = LoadSpriteFromFile(spr_path);
        itemBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerClass.player.amount[tmp2].ToString();
        itemBar.transform.GetChild(0).GetComponent<Image>().sprite = itmSpr;
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