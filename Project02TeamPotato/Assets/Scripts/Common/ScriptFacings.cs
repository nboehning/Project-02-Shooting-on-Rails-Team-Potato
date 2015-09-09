using UnityEngine;
using System.Collections;

public class Facings : Waypoint
{
    float xRotation;
    float yRotation;
    float lookSensitivity = 5f;
    float curXRotation;
    float curYRotation;
    float lookSmoothDamp = 0.1f;
    float xRotationV = 0;
    float yRotationV = 0;

    GameObject mainCamera = GameObject.Find("Main Camera");

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        curXRotation = Mathf.SmoothDamp(curXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        curYRotation = Mathf.SmoothDamp(curYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}
}
