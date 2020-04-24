using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    public bool lockMouse;

    [Header("Vision Prefs")]
    [SerializeField] private float rayDistance;
    [HideInInspector] public bool visionLock = false;

    [Header("Camera Movements")]
    [SerializeField] private float cameraMovementSpeed = 10f;
    [SerializeField] private float cameraMagnetRadius = 0.2f;

    [Header("HUD")]
    [SerializeField] private GameObject HUD;

    [HideInInspector] public GameObject objectLookingAt;
    private RaycastHit hit;

    private Vector3 basePos;
    private Quaternion baseRot;
    private float baseFov;
    private bool observing = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectLookingAt = null;

        //--- Pour lock le curseur dans l'écran ---//
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!visionLock)
        {
            Look();
            LineVision();
        } else
        {
            if(observing && Input.GetKeyDown(KeyCode.Escape))
            {
                //StartCoroutine(MoveCamera(baseTransform.localPosition, baseTransform.rotation));
                CameraComeBack();
            }
        }
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void LineVision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance) && hit.collider.gameObject != null)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            objectLookingAt = hit.collider.gameObject;
            //Debug.Log(objectLookingAt);

            if (Input.GetKeyDown(KeyCode.E) && objectLookingAt != null && objectLookingAt.GetComponent<Action>() != null)
            {
                objectLookingAt.GetComponent<Action>().Act();
            }
            switch (hit.collider.gameObject.tag)
            {
                case "Interactable":
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (objectLookingAt.GetComponentInChildren<Camera>() != null && objectLookingAt.GetComponentInChildren<Camera>().enabled)
                        {
                            observing = true;

                            HUD.SetActive(false);
                            visionLock = true;
                            Inventory.instance.ShowHand(false);
                            BaseTransform(transform);
                            baseFov = GetComponent<Camera>().fieldOfView;

                            if(objectLookingAt.GetComponentInChildren<InputField>() != null)
                                objectLookingAt.GetComponentInChildren<InputField>().ActivateInputField();

                            StartCoroutine(MoveCamera(objectLookingAt.GetComponentInChildren<Camera>().transform, objectLookingAt.GetComponentInChildren<Camera>().fieldOfView));
                        }

                        
                    }
                    break;
                case "Prop":
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        Inventory.instance.PickUp(objectLookingAt);
                    }
                    break;
            }

            //Debug.Log((Input.GetKeyDown(KeyCode.E)) + " // " + (objectLookingAt));
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
            objectLookingAt = null;
        }
    }

    public void ToggleLockMouse()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            visionLock = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            visionLock = false;
        }
    }

    public void CameraComeBack()
    {
        HUD.SetActive(true);
        Inventory.instance.ShowHand(true);
        StopAllCoroutines();
        StartCoroutine("ComeBack");
        observing = false;
    }

    public void ShowHUD(bool state)
    {
        HUD.SetActive(state);
    }

    private void BaseTransform(Transform transform)
    {
        basePos = transform.localPosition;
        baseRot = transform.localRotation;
    }


    IEnumerator MoveCamera(Vector3 pos, Quaternion rot, float fov)
    {
        //Debug.Log("Processing to " + pos + " from " + transform.position);
        while (transform.position != pos)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.Lerp(transform.position, pos, cameraMovementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, cameraMovementSpeed * Time.deltaTime);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, fov, cameraMovementSpeed * Time.deltaTime);

            if ((transform.position - pos).magnitude < cameraMagnetRadius)
            {
                transform.position = pos;
                transform.rotation = rot;
                GetComponent<Camera>().fieldOfView = fov;
            }
        }
    }

    IEnumerator MoveCamera(Transform target, float fov)
    {
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(MoveCamera(target.position, target.rotation, fov));
    }

    IEnumerator ComeBack()
    {
        //Debug.Log("Processing to " + basePos + " from " + transform.localPosition);
        while (transform.localPosition != basePos)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, basePos, cameraMovementSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, baseRot, cameraMovementSpeed * Time.deltaTime);
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, baseFov, cameraMovementSpeed * Time.deltaTime);


            if ((transform.localPosition - basePos).magnitude < cameraMagnetRadius)
            {
                transform.localPosition = basePos;
                transform.localRotation = baseRot;
                GetComponent<Camera>().fieldOfView = baseFov;
            }
        }
        visionLock = false;
    }
}
