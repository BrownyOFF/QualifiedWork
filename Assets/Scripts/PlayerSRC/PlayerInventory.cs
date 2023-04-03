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
    private GameObject itemBar;
    public int flowerAmount;
    public int shardAmount;
    public int quickCurrent = 0;
    public int quickCurrentID = 3;
    public List<int> inv;
    public List<int> amount;
    public List<int> quickSlots;
    public TextAsset itemsList;
    public TextAsset itemsSprite;
    private string itemsListString;
    private int flaskMax = 2;
    
    void Start()
    {
        itemBar = GameObject.Find("itemBar");
        chara = GetComponent<PlayerChara>();
        move = GetComponent<PlayerMovement>();
        fight = GetComponent<FightBehaviour>();
        itemsListString = itemsList.text;
        inv.Add(3);
        amount.Add(flaskMax);
        quickSlots.Add(inv[0]);
        ChangeSprite();
    }
    public void TakeItem(int id)
    {
        if (inv.Contains(id))
        {
            int i = inv.IndexOf(id);
            amount[i]++;
        }else
        {
            inv.Add(id);
            amount.Add(1);
        }
    }

    public void UseItem(int id)
    {
        var tmpID = inv.IndexOf(id);
        if (id == 3 && amount[tmpID] > 0)
        {
            chara.hpCurrent += 25;
            amount[tmpID]--;
        }
    }

    public void ResetFlask()
    {
        amount[inv.IndexOf(3)] = flaskMax;
    }

    private void ChangeItem(int n)
    {
        if (quickCurrent == quickSlots.Count-1 && n == 1)
        {
            quickCurrent = 0;
            return;
        }
        else if (quickCurrent == 0 && n == -1)
        {
            quickCurrent = quickSlots.Count-1;
            return;
        }

        quickCurrent += n;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(inv);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !fight.isAttacking && !fight.isBlocking)
        {
            UseItem(quickSlots[quickCurrent]);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
        {
            ChangeItem(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
        {
            ChangeItem(-1);
        }
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        var tmp = quickSlots[quickCurrent];
        var tmp2 = inv.IndexOf(tmp);
        var text = itemsSprite.text;
        var spr_path = getBetween(text,quickSlots[quickCurrent].ToString() + ":" , "\r");
        Sprite itmSpr = LoadSpriteFromFile(spr_path);
        itemBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = amount[tmp2].ToString();
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