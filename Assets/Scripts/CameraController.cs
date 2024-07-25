using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    float mouseX;
    float mouseY;

    private float rotationX = 0f;
    private float sensX = 2f;

    public float mouseSen;

    public GameObject cam;

    [HideInInspector]


    bool mCanLook = true;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

        mCanLook = !ConversationManager.sInstance.mConversationActive;

        if (mCanLook)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            mouseLook(mouseX, mouseY);

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void mouseLook(float mouseX, float mouseY)
    {

        rotationX += mouseY * sensX;
        rotationX = Mathf.Clamp(rotationX, -90, 90);

        if (mouseX > 0)
        {
            transform.Rotate(Vector3.up * mouseSen * mouseX);
        }
        if (mouseX < 0)
        {
            transform.Rotate(-Vector3.up * mouseSen * -mouseX);
        }
        cam.transform.localEulerAngles = new Vector3(-rotationX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
    }
}