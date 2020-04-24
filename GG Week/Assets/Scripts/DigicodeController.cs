using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DigicodeController : MonoBehaviour
{
    public UnityEvent callbacks;

    public string password;
    public string input;

    public void Add(int num)
    {
        input += num;
    }

    public void Submit()
    {
        if(input == password)
        {
            callbacks.Invoke();
            SoundManager.instance.Play("Correct");
        } else
        {
            SoundManager.instance.Play("Wrong");
        }

        input = "";
    }
}
