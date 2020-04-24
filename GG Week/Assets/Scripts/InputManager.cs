using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public string password;
    public UnityEvent callback;
    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void Check()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if(inputField.text.ToLower() == password.ToLower())
            {
                callback.Invoke();
            }

            inputField.text = "";
        }

    }
}
