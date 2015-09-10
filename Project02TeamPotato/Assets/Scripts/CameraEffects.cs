using UnityEngine;
//using System;
using System.Collections;

public class CameraEffects : MonoBehaviour {

	private Vector3 originalCameraPosition;
	private Camera myCamera;
	private Rect cameraRect;
	bool switchFrames = true;
	private float randomValue;

	// Use this for initialization
	void Start ()
	{
		originalCameraPosition = transform.localPosition;
		Debug.Log("Camera Position: " + originalCameraPosition);
		myCamera = GetComponentInParent<Camera>();
		cameraRect = myCamera.rect;
		Debug.Log("Camera Rect: " + cameraRect.ToString());
//		Debug.Log("Camera Rect: " + myCamera.rect.ToString());

	}
	
	// Update is called once per frame
	void Update ()
	{
		switchFrames = !switchFrames;

		if (switchFrames)
		{
			randomValue = 0.3f * Random.value;
			transform.localPosition = new Vector3(originalCameraPosition.x + randomValue,
			                                      originalCameraPosition.y + randomValue, originalCameraPosition.z);
//			randomValue = 0.1f;
//			transform.localPosition = new Vector3(originalCameraPosition.x + randomValue,
//			                                      originalCameraPosition.y + randomValue, originalCameraPosition.z);
//			randomValue = 0.3f * Random.value;
//			transform.localPosition = new Vector3(originalCameraPosition.x + randomValue,
//			                                      originalCameraPosition.y + randomValue, 0);
//			myCamera.rect = new Rect(randomValue, 0f, 1f - randomValue * 2f, 1f);
		}
		else
			transform.localPosition = originalCameraPosition;
// myCamera.rect = cameraRect;


//		if (Input.GetButtonDown ("Jump"))
//		{
//			// choose the margin randomly
//			float margin = Random.value;
//			var margin = Random.Range (0.0, 0.3);
			// setup the rectangle
//			camera.rect = Rect (margin, 0, 1 - margin * 2, 1);
//		}
	}
}
