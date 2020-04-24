using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    [Header("temp")]
    public GameObject objectToInspect;

    [Header("Inspect")]
    public GameObject inspectRender;
    public Transform inspectParent;

    private Vector3 mouseLastPos;

    void Start()
    {
        mouseLastPos = Vector3.zero;
    }

    void Update()
    {
        if(!IsInspectRenderActive() && Input.GetKeyDown(KeyCode.R) && Inventory.instance.objectInHand != null)
        {
            objectToInspect = Inventory.instance.objectInHand;
            ToggleInspectRender();
        }

        if(IsInspectRenderActive())
        {
            ObjectRotation();

            if(Input.GetButtonDown("Cancel"))
            {
                ToggleInspectRender();
            }
        }
    }

    public void ToggleInspectRender()
    {
        SetInspectRender(!IsInspectRenderActive());
        MouseLook.instance.ToggleLockMouse();
    }

    public void SetInspectRender(bool state)
    {
        inspectRender.SetActive(state);

        if(state == true)
        {
            inspectParent.rotation = Quaternion.identity;
            GameObject insta = Instantiate(objectToInspect, inspectParent);

            if(insta.GetComponent<RotationInHand>() != null)
            {
                insta.transform.eulerAngles = insta.GetComponent<RotationInHand>().rotation;
            }

            insta.transform.localPosition = new Vector3(0, 0, 0);
            insta.layer = inspectParent.gameObject.layer;
            SetLayerRecursively(insta, insta.layer);

            if(insta.GetComponent<Rigidbody>() != null)
            {
                insta.GetComponent<Rigidbody>().isKinematic = true;
            }
            MouseLook.instance.ShowHUD(false);

            if(objectToInspect.GetComponent<Action>() != null)
            {
                objectToInspect.GetComponent<Action>().Act();
            }
        } else
        {
            if(HasObjectInspected())
            {
                for (int i = 0; i < inspectParent.childCount; i++)
                {
                    if (inspectParent.GetChild(i).childCount > 0 && inspectParent.GetChild(i).GetChild(0).GetComponent<Action>() != null) {
                        inspectParent.GetChild(i).GetChild(0).GetComponent<Action>().Act();
                    }
                    Destroy(inspectParent.GetChild(i).gameObject);
                }
            }
            MouseLook.instance.ShowHUD(true);
        }

    }

    public bool IsInspectRenderActive()
    {
        return inspectRender.activeSelf;
    }

    public bool HasObjectInspected()
    {
        if (inspectParent.childCount > 0)
            return true;

        return false;
    }

    private void ObjectRotation()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseLastPos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - mouseLastPos;
            mouseLastPos = Input.mousePosition;

            Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;

            inspectParent.rotation = Quaternion.AngleAxis(delta.magnitude * 0.1f, axis) * inspectParent.rotation;
        }
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}