using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Image mainPanel;
    public Image creditsPanel;

    public void BeginGame()
    {
        SceneManager.LoadScene("Loading Scene");
    }

    public void CreditScreen()
    {
        mainPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(true);
    }

    public void ReturnToMain()
    {
        mainPanel.gameObject.SetActive(true);
        creditsPanel.gameObject.SetActive(false);
    }

}
