using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class Load : MonoBehaviour
{
    [SerializeField]public GameObject loadingScreen;
    [SerializeField]public Image progressBar;
    [SerializeField]public GameObject hintObj;
    [SerializeField]public GameObject input;
    [SerializeField]public TextAsset hints_file;
    private AsyncOperation asyncOperation;
    private bool isLoadingComplete = false;

    private void Update()
    {
        if (isLoadingComplete && Input.GetKeyDown(KeyCode.Return))
        {
            ActivateScene();
        }
    }

    public void LoadScene(string sceneName)
    {
        
        var hints_str = hints_file.text;
        var hints_amount = hints_str.Split('\r').Length;
        Random rng = new Random();
        var hint_id = rng.Next(0, hints_amount);
        var hint_text = getBetween(hints_str, hint_id + ":", "\r");
        hintObj.GetComponent<TextMeshProUGUI>().text = "Запис у щоденнику " + (hint_id + 1) + "\n" + hint_text;
        
        var bg = LoadSpriteFromFile("Assets/Images/"+ sceneName + "_load.jpg");
        loadingScreen.GetComponent<Image>().sprite = bg;
        
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        
        loadingScreen.SetActive(true);
        input.SetActive(false);
        
        yield return new WaitForSeconds(1f); // Затримка на 1 секунду

        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.fillAmount = progress;
            if (progress >= 1.0f)
            {
                isLoadingComplete = true;
                input.SetActive(true);
            }

            yield return null;
        }

        ActivateScene();
    }

    private void ActivateScene()
    {
        asyncOperation.allowSceneActivation = true;
        loadingScreen.SetActive(false);
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
