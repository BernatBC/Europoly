using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pausePanel;

    /// <summary>
    /// Method <c>ShowPauseMenu</c> pauses the game, and shows the pause menu.
    /// </summary>
    public void ShowPauseMenu() {
        Time.timeScale = 0;
        GetComponent<CellInfo>().SetRaycast(false);
        pausePanel.SetActive(true);
    }

    /// <summary>
    /// Method <c>ResumeGame</c> resumes the game.
    /// </summary>
    public void ResumeGame() {
        pausePanel.SetActive(false);
        GetComponent<CellInfo>().SetRaycast(true);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Method <c>BackToMenu</c> returns to menu screen
    /// </summary>
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Method <c>QuitGame</c> quits the game.
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }
}
