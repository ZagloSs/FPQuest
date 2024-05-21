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
            SceneManager.LoadScene("RoomGeneration");
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
        // Espera un frame para asegurar que la escena se haya cargado.
        yield return null;

        // Encuentra los componentes necesarios.
        RoomManager roomManager = FindObjectOfType<RoomManager>();
        PlayerPosition player = FindObjectOfType<PlayerPosition>();
        Properties playerProperties = player.GetComponent<Properties>();

        // Carga el juego.
        SaveLoadManager.Instance.LoadGame(roomManager, player, playerProperties);
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
