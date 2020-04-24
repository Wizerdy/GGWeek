using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    public GameObject[] dialogueBox;
    public GameObject player;
    public int number = 2;

    void Start()
    {
        
    }

    void Update()
    {

        int j = playtext(number);
        for (int i = 0; i < dialogueBox.Length; i++)
        {
            if (i == j)
            {
                dialogueBox[i].SetActive(true);
            }
        }
    }

    int playtext(int number)
    {
        return number;
    }
}
