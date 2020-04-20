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

    void LoadCredit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Credit", LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCountInBuildSettings != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            if (SceneManager.GetActiveScene().buildIndex != 0)
                menuPauseUI.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("You've reach the last level");
            LoadCredit();
        }
    }

    public int getLevelNum()
    {
        return (SceneManager.GetActiveScene().buildIndex);
    }

    void Reprendre()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1;
    }


}
