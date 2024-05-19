using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject LoadingScene;
    public static MenuManager instance;

    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void backToMenu()
    {
        LoadingScreen("MainMenu");
        AudioManager.instance.PlayClick();
    }

    public void goToCharacterSelection()
    {
        LoadingScreen("Character Selection");
    }

    public void goToGame(string sceneName)
    {
        LoadingScreen(sceneName);
    }

    public void quitGame()
    {
        Application.Quit();
    }



    public void LoadingScreen(string sceneName)
    {
        StartCoroutine(LoadSceneAynchronously(sceneName));
    }

    IEnumerator LoadSceneAynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScene.SetActive(true);
        while (!operation.isDone)
        {

            yield return null;
        }
    }

    //Peque�o parche para arreglar un bug.
    public void AuidoManagerPlayClick()
    {
        AudioManager.instance.PlayClick();
    }
}
