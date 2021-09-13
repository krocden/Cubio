using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC");
            if(isPaused)
            {
                Debug.Log("unpause");
                ResumeGame();
            }
            else
            {
                Debug.Log("pause");
                PauseGame();
            }
        }
    }

    //Pause the game
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }
    //Resume the game from pause
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
    //Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
