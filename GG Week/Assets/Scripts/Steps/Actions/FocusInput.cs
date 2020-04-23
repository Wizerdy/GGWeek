using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusInput : MonoBehaviour
{
    public void Focus()
    {
        gameObject.GetComponent<InputField>().ActivateInputField();
    }
}
