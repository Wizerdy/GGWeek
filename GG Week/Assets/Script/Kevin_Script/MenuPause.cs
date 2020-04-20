using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuPause : MonoBehaviour
{

    public static bool GamePause = false;
    public GameObject menuPauseUI;

    void continuer()
    {
        menuPauseUI.SetActive(false);
        Time.timeScale = 1;
        GamePause = false;
        Cursor.lockState = CursorLockMode.Locked;
        

    }

    void Pause()
    {
        menuPauseUI.SetActive(true);
        Time.timeScale = 0;
        GamePause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            //  Debug.Log("touche P presser");
            if (GamePause)
            {
                continuer();
            }
            else
            {
                Pause();
            }
        }

    }
}
