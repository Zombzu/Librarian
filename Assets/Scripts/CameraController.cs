using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;
    private float rotationX = 0f;
    private float sensX = 100f;

    public float mouseSen;

    public GameObject cam;

    [HideInInspector]


    bool mCanLook = true;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

//        mCanLook = !ConversationManager.sInstance.mConversationActive;

        if (mCanLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseLook();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void mouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSen * Time.deltaTime;

        rotationX-= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 

        cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); 
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}