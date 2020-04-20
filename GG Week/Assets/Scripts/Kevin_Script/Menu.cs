using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPauseUI;

    void BackMenu()
    {
        SceneManager.LoadScene("MenueP", LoadSceneMode.Single);
    }

    void Quitter()
    {
        Application.Quit();
    }

    void Reprendre()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1;
    }


}
