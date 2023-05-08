using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamemanagerScript : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject pauseButton;

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene Jacob");
        Time.timeScale = 1f;
    }

    public void MenuOption()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    //PauseSection

    public void PauseButton()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        PausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

}
