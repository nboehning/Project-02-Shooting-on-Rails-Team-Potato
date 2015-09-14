using UnityEngine;
using System.Collections;

public class Facings
{
    float xRotation;
    float yRotation;
    float lookSensitivity = 5f;
    float curXRotation;
    float curYRotation;
    float lookSmoothDamp = 0.1f;
    float xRotationV = 0;
    float yRotationV = 0;
    private float duration;
    GameObject mainCamera = GameObject.Find("Main Camera");


    //public IEnumerator setFacing(bool isForced)
    //{
    //    if (!isForced)
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;

    //        yRotation += Input.GetAxis("Mouse X")*lookSensitivity;
    //        xRotation -= Input.GetAxis("Mouse Y")*lookSensitivity;

    //        xRotation = Mathf.Clamp(xRotation, -90, 90);

    //        curXRotation = Mathf.SmoothDamp(curXRotation, xRotation, ref xRotationV, lookSmoothDamp);
    //        curYRotation = Mathf.SmoothDamp(curYRotation, yRotation, ref yRotationV, lookSmoothDamp);

    //        mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    //        yield return new WaitForSeconds(duration);
    //    }
    //    else
    //    {

    //    }
    //}

    //public void setDuration(float sec)
    //{
    //    duration = sec;
    //}
}
