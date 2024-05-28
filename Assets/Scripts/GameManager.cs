 using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject LoadingScene;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject pickUpCanvas;
    private RuntimeAnimatorController animator;
    private Dictionary<string, float> properties;
    private Sprite healtrait;

    public int spawnedEnemies;

    public GameObject movementJoystickPrefab;  // Referencia al prefab del joystick de movimiento
    private GameObject movementJoystickInstance;
    
    public GameObject attackJoystickPrefab;  // Referencia al prefab del joystick de disparo
    private GameObject attackJoystickInstance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //Prueba para acceder al diccionario de properties
            properties = new Dictionary<string, float>();
            spawnedEnemies = 0;
            InitializeProperties();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // Instanciar el joystick solo en plataformas móviles
        if (!Application.isMobilePlatform)
        {
            movementJoystickInstance.SetActive(false);
            attackJoystickInstance.SetActive(false);
        }
    }
    // Método para obtener el joystick de movimiento
    public Joystick GetMovementJoystick()
    {
        if (movementJoystickInstance != null)
        {
            return movementJoystickInstance.GetComponent<Joystick>();
        }
        return null;
    }    
    
    // Método para obtener el joystick de disparo
    public Joystick GetAttackJoystick()
    {
        if (attackJoystickInstance != null)
        {
            return attackJoystickInstance.GetComponent<Joystick>();
        }
        return null;
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

    public void setHealthPortrait(Sprite sprite)
    {
        healtrait = sprite;
    }
    public Sprite getHealthPortrait()
    {
        return healtrait;
    }

    public void setProperties(Dictionary<string, float> properties)
    {
        this.properties = properties;
    }

    public Dictionary<string, float> getProperties()
    {
        return properties;

    }

    public void setAnimator(RuntimeAnimatorController animator)
    {
        this.animator = animator;
    }
    public RuntimeAnimatorController GetAnimator()
    {
        return this.animator;
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void mostrarCanvasModificacionItem(string str)
    {
        pickUpCanvas.GetComponentInChildren<TextMeshProUGUI>().text = str;
        pickUpCanvas.SetActive(true);
    }
}
