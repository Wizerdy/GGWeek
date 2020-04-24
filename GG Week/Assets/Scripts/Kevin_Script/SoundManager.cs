using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] son;
    public int number;

     /*public static void playSound()
     {
         GameObject soundGameObject = new GameObject("son");
         AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

     }*/  

    void Update()
    {

        int j = number;
        for (int i = 0; i < son.Length; i++)
        {
            if (i == j)
            {
                //son[i];
            }
        }
    }

}
