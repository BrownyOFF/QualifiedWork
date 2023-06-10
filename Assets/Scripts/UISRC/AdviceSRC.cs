using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdviceSRC : MonoBehaviour
{
    public TextAsset adviceFile;
    public TextAsset itemFile;
    public GameObject adviceTextObj;
    public GameObject textPlusItem;
    public GameObject itemIMG;
    public GameObject panel;
    
    void Start()
    {
        panel.SetActive(false);
    }

    public void CheckInt(int adviceInt, bool isItem)
    {
        panel.SetActive(true);
        if (isItem)
        {
            string itemString = itemFile.ToString();
            adviceTextObj.SetActive(false);
            textPlusItem.SetActive(true);
            itemIMG.SetActive(true);
            var spr_path = getBetween(itemString, adviceInt + ":", "\r");
            Sprite spr = LoadSpriteFromFile(spr_path);
            itemIMG.GetComponent<Image>().sprite = spr;
        }
        else
        {
            
            string adviceString = adviceFile.ToString();
            adviceTextObj.SetActive(true);
            textPlusItem.SetActive(false);
            itemIMG.SetActive(false);
            var adviceText = getBetween(adviceString, "advice" + adviceInt + ":", "\r");
            adviceTextObj.GetComponent<TextMeshProUGUI>().text = adviceText;
        }

        StartCoroutine(HideTime());
    }

    IEnumerator HideTime()
    {
        yield return new WaitForSeconds(5f);
        panel.SetActive(false);
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
