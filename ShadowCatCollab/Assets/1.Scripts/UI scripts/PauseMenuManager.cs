using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public string menuSceneName;

    public GameObject pauseMenuPanel;
    public GameObject pauseMenuIcon;
    private bool gamePaused;
    // Start is called before the first frame update

    private void Awake()
    {
        PauseMenuDeactive();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenuControl();
    }

    void PauseMenuControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            PauseMenuDeactive();
        }    
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
          PauseMenuActive();
        }
    }

    public void PauseMenuActive()
    {
        pauseMenuPanel.SetActive(true);
        pauseMenuIcon.SetActive(false);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void PauseMenuDeactive()
    {
        pauseMenuPanel.SetActive(false);
        pauseMenuIcon.SetActive(true);
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    
}
