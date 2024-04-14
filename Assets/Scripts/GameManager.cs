 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject LoadingScene;
    private Animator animator;
    private Sprite skin;
    private Dictionary<string, float> properties;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setProperties(Dictionary<string, float> properties)
    {
        this.properties = properties;
    }

    public Dictionary<string, float> getProperties()
    {
        return properties;

    }




    public void backToMenu()
    {
        LoadingScreen("MainMenu");
    }

    public void goToCharacterSelection()
    {
        LoadingScreen("Character Selection");
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
}
