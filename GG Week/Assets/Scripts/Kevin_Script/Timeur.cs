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


    [SerializeField] Text compteur;

    // Start is called before the first frame update
    void Start()
    {

        blackScreen.SetActive(true);
        blackScreeenDead.SetActive(false);
        currentTime = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        compteur.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {

            currentTime = 0;
            blackScreeenDead.SetActive(true);
            pause.SetActive(false);

        }

    }
}
