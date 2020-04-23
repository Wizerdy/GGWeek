using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Action : MonoBehaviour
{
    public UnityEvent callbacks;

    public void Act()
    {
        callbacks.Invoke();
    }
}
