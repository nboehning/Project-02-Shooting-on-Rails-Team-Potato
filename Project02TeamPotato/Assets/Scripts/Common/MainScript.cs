using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	public Waypoint[] moveWaypoints;
	public Waypoint[] faceWaypoints;
	public CameraEffects[] cameraWaypoints;

	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");

		cameraWaypoints = new CameraEffects[10];
		for (int i = 0; i < 10; i++)
		{
			cameraWaypoints[i] = new CameraEffects();
			cameraWaypoints[i].wpType = waypointTypes.CAMERA;
			cameraWaypoints[i].camType = i % 2 == 0 ? cameraTypes.SHAKE : cameraTypes.WAIT;
			cameraWaypoints[i].shakeIntensity = i % 4 == 0 ? 2 : 4;
			cameraWaypoints[i].shakeSeconds = i % 4 == 0 ? 2.0f : 4.0f;
			cameraWaypoints[i].waitSeconds = i % 4 == 0 ? 2.0f : 4.0f;
		}

		StartCoroutine(CameraEngine());
	}

	IEnumerator CameraEngine()
	{
		foreach(CameraEffects cwp in cameraWaypoints)
		{
			switch(cwp.camType)
			{
				case cameraTypes.SHAKE:
					StartCoroutine(ShakeCamera(cwp.shakeSeconds, cwp.shakeIntensity));
					yield return new WaitForSeconds(cwp.shakeSeconds);
					break;
				case cameraTypes.WAIT:
					StartCoroutine(WaitCamera(cwp.waitSeconds));
					yield return new WaitForSeconds(cwp.waitSeconds);
					break;
				case cameraTypes.FADE:
					break;
				case cameraTypes.SPLATTER:
					break;
				default:
					Debug.Log ("Invalid camera effect!");
					break;
			}
		}
		yield return null;
	}

	// Update is called once per frame
	//	void Update () {
	//	
	//	}
	
	IEnumerator ShakeCamera(float shakeSeconds, int shakeIntensity)
	{
		float elapsedTime = 0f;
		float randomValueX;
		float randomValueY;
		Vector3 originalCameraPosition = mainCamera.transform.position;

		while (elapsedTime < shakeSeconds)
		{
			randomValueX = 0.3f * shakeIntensity * (Random.value - 0.5f);
			randomValueY = 0.3f * shakeIntensity * (Random.value - 0.5f);
			mainCamera.transform.position = new Vector3(originalCameraPosition.x + randomValueX,
			                                            originalCameraPosition.y + randomValueY,
			                                            originalCameraPosition.z);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		mainCamera.transform.position = originalCameraPosition;
	}

	IEnumerator WaitCamera(float waitSeconds)
	{
		yield return new WaitForSeconds(waitSeconds);
	}
} // end class MainScript
