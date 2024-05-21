using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private SaveLoadManager saveLoadManager;
    private RoomManager roomManager;
    private PlayerPosition player;
    private Properties playerProperties;

    private void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        roomManager = FindObjectOfType<RoomManager>();
        player = FindObjectOfType<PlayerPosition>();
        playerProperties = player.GetComponent<Properties>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SaveGame()
    {
        saveLoadManager.SaveGame(roomManager, player, playerProperties);
    }


    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
