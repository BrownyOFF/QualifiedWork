using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    [SerializeField]public GameObject loadingScreen;
    [SerializeField]public Image progressBar;
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
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(1f); // Затримка на 1 секунду

        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.fillAmount = progress;
            Debug.Log(progress);
            if (progress >= 1.0f)
            {
                isLoadingComplete = true;
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
}
