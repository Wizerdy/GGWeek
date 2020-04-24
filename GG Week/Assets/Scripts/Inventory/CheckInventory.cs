using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckInventory : MonoBehaviour
{
    public GameObject objectToHave;
    public UnityEvent callbacks;

    public void Check()
    {
        if(Inventory.instance.objectInHand == objectToHave)
        {
            Inventory.instance.DestroyHand();
            callbacks.Invoke();
        }
    }
}
