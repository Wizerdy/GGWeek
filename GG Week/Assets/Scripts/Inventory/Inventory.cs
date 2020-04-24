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
        if (objectInHand != null)
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

        if (obj.GetComponent<RotationInHand>() != null)
        {
            obj.transform.eulerAngles = obj.GetComponent<RotationInHand>().rotation;
        }

        obj.GetComponentInChildren<Collider>().enabled = false;

        if(obj.GetComponentInChildren<Rigidbody>() != null)
        {
            obj.GetComponentInChildren<Rigidbody>().isKinematic = true;
        }
    }

    public void DestroyHand()
    {
        Destroy(objectInHand);
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
