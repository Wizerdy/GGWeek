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
        if(Input.GetKeyDown(KeyCode.R) && Inventory.instance.objectInHand != null)
        {
            objectToInspect = Inventory.instance.objectInHand;
            ToggleInspectRender();
            MouseLook.instance.ToggleLockMouse();
        }

        if(IsInspectRenderActive())
        {
            ObjectRotation();
        }
    }

    public void ToggleInspectRender()
    {
        SetInspectRender(!IsInspectRenderActive());
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

            if(insta.GetComponent<Rigidbody>() != null)
            {
                insta.GetComponent<Rigidbody>().isKinematic = true;
            }
            MouseLook.instance.ShowHUD(false);

        } else
        {
            if(HasObjectInspected())
            {
                for (int i = 0; i < inspectParent.childCount; i++)
                {
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
}