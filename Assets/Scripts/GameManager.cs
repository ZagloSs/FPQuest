 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject LoadingScene;
    private AnimatorController animator;
    private Dictionary<string, float> properties;
    [SerializeField] private GameObject healthBar;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //Prueba para acceder al diccionario de properties
            properties = new Dictionary<string, float>();

            InitializeProperties();

        }
    }

    private void InitializeProperties()
    {
        // Inicializamos el diccionario de propiedades con valores por defecto
        properties = new Dictionary<string, float>();
        properties.Add("Health", 5f); // Por ejemplo, asignamos un valor por defecto de 100 a la salud
        properties.Add("Damage", 5f);  // Asignamos un valor por defecto de 10 al daño
        properties.Add("Speed", 0.5f);    // Asignamos un valor por defecto de 5 a la velocidad
        properties.Add("AttSpeed", 5f); // Asignamos un valor por defecto de 1 a la velocidad de ataque
        
    }

    public void setProperties(Dictionary<string, float> properties)
    {
        this.properties = properties;
    }

    public Dictionary<string, float> getProperties()
    {
        return properties;

    }

    public void setAnimator(AnimatorController animator)
    {
        this.animator = animator;
    }
    public AnimatorController GetAnimator()
    {
        return this.animator;
    }
    


    public void backToMenu()
    {
        LoadingScreen("MainMenu");
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
}
