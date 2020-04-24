using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeur : MonoBehaviour
{

    public float currentTime = 0;
    public float startTime = 10;
    public GameObject blackScreeenDead;
    public GameObject blackScreen;
    public GameObject pause;
    public GameObject textmanager;
    private bool dialogue = false;

    [SerializeField] Text compteur;

    // Start is called before the first frame update
    void Start()
    {
        textmanager.SetActive(false);
        blackScreen.SetActive(true);
        blackScreeenDead.SetActive(false);
        currentTime = startTime;

    }


    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        compteur.text = currentTime.ToString("0");
        int second = (int)(currentTime % 60);
        int minute = (int)(currentTime / 60);


        string timerString = string.Format("{0:0}:{1:00}",minute,second);

        compteur.text = timerString;

        if (dialogue)
        {
            textmanager.SetActive(true);
        }

        if (currentTime <= 65 )
        {
            dialogue = true;
        }

        if(currentTime <= 0)
        {

            currentTime = 0;
            blackScreeenDead.SetActive(true);
            pause.SetActive(false);

        }

    }
}
