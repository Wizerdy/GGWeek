using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitter : MonoBehaviour
{
    public float temps;

    void Update()
    {
        temps -= Time.deltaTime;
        Time.timeScale = 1;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quiter");
            Application.Quit();
        }
        else if(temps <= 0)
        { 
               Debug.Log("quiter2");
               Application.Quit();

        }
    }
}
