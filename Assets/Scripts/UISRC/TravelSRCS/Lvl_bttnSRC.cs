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
        panel_src = GameObject.Find("Panel Travel");
        bttn = GetComponent<Button>();
        bttn.onClick.AddListener(onClickFunc);
    }

    private void onClickFunc()
    {
        panel_src.GetComponent<TravelPanel>().lvl_image.SetActive(true);
        Sprite itmSpr = LoadSpriteFromFile( "Assets/Images/" + lvl_name + ".png");
        panel_src.GetComponent<TravelPanel>().lvl_image.GetComponent<Image>().sprite = itmSpr;
        panel_src.GetComponent<TravelPanel>().conf.SetActive(true);
        panel_src.GetComponent<TravelPanel>().lvl_chosen = lvl_name;
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