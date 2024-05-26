using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool IsPaused = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
        {
            IsPaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);

        }else if(Input.GetKeyDown(KeyCode.Escape) && IsPaused)
        {
            IsPaused = false;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }

    public void setNonPaused()
    {
        IsPaused = false;
    }
}
