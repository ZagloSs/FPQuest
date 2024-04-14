 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
