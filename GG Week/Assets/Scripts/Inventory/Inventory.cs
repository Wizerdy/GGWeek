using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject objectInHand;

    [Header("Parents")]
    public Transform hand;
    public Transform drop;
    public Transform propParent;

    private void Awake()
    {
        instance = this;
    }

    public void Drop()
    {
        objectInHand.transform.parent = propParent;
        objectInHand.transform.position = drop.transform.position;

        objectInHand.GetComponentInChildren<Collider>().enabled = true;

        if (objectInHand.GetComponentInChildren<Rigidbody>() != null)
        {
            objectInHand.GetComponentInChildren<Rigidbody>().isKinematic = false;
        }

        objectInHand = null;
    }

    public void PickUp(GameObject obj)
    {
        if(objectInHand != null)
        {
            Drop();
        }

        objectInHand = obj;
        obj.transform.position = hand.position;
        obj.transform.parent = hand.transform;
        obj.transform.localRotation = Quaternion.identity;

        obj.GetComponentInChildren<Collider>().enabled = false;

        if(obj.GetComponentInChildren<Rigidbody>() != null)
        {
            obj.GetComponentInChildren<Rigidbody>().isKinematic = true;
        }
    }

    public void ToggleHand()
    {
        hand.gameObject.SetActive(!hand.gameObject.activeSelf);
    }

    public void ShowHand(bool state)
    {
        hand.gameObject.SetActive(state);
    }
}
