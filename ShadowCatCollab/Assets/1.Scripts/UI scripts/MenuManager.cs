using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainBttn, mainImg;
    public GameObject optiosBttn, optionsImg;
    public GameObject creditsBttn, creditsImg;
    public GameObject creditsPanel, optionsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene Jacob");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainBttn()
    {
        mainBttn.SetActive(false);
        mainImg.SetActive(true);
        optiosBttn.SetActive(true);
        optionsImg.SetActive(false);
        creditsBttn.SetActive(true);
        creditsImg.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OptionsBttn()
    {
        mainBttn.SetActive(true);
        mainImg.SetActive(false);
        optiosBttn.SetActive(false);
        optionsImg.SetActive(true);
        creditsBttn.SetActive(true);
        creditsImg.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CreditsBttn()
    {
        mainBttn.SetActive(true);
        mainImg.SetActive(false);
        optiosBttn.SetActive(true);
        optionsImg.SetActive(false);
        creditsBttn.SetActive(false);
        creditsImg.SetActive(true);
        creditsPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
}
