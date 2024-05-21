using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
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
            DontDestroyOnLoad(gameObject);
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
    public void LoadGame()
    {
        if (SaveLoadManager.Instance.SaveFileExists())
        {
            GameManager.instance.isLoaded = true;
            
            // Usa una corutina para asegurar que la escena se cargue antes de llamar a LoadGame.
            StartCoroutine(LoadGameAfterSceneLoad());
        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }
    private IEnumerator LoadGameAfterSceneLoad()
    {
        SceneManager.LoadScene("RoomGeneration");
        // Espera un frame para asegurar que la escena se haya cargado.
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Nigs");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Nigs2" + player.GetComponent<Properties>().Health);
        Debug.Log("Nigs3");
        SaveLoadManager.Instance.LoadGame(/*roomManager, playerPos,*/ player);
        Debug.Log("Nigs4");
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

    //Pequeño parche para arreglar un bug.
    public void AuidoManagerPlayClick()
    {
        AudioManager.instance.PlayClick();
    }

}
