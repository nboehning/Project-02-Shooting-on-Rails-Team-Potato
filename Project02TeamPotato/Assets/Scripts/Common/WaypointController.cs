using UnityEngine;
using System.Collections;
using System.Text;

public class WaypointController : MonoBehaviour
{

    public MovementTypes[] waypointObjects;
//	public MovementTypes[] cameraWaypoints;
	
	private GameObject mainCamera;

//	public MovementType moveType;
//	public float waypointDuration;
//	
//	public Transform point1;
//	public Transform point2;
//	public Transform point3;
//	public Transform lookPoint;
//	
//	public float lookTime1;
//	public float lookTime2;
//	public float lookTime3;
//	
//	public FacingTypes facingType;
//	public CameraEffectTypes effectType;
//	public float effectDuration;
//	
//	public float fadeTime;
//	public bool splatterFade;
//	public float shakeIntensity;
//	public bool isFadedOut = false;	// If true, screen will be black, if false, screen will be visible

	void Start ()
	{
		mainCamera = GameObject.FindWithTag("MainCamera"); // find the main camera by its tag

		waypointObjects = new MovementTypes[10]; // start off with 10 waypoints for testing
		for (int i = 0; i < 10; i++) // init all 10 testing waypoints as camera effects
		{
			waypointObjects[i] = new MovementTypes();
			// alternate between a Shake camera effect and None
			waypointObjects[i].effectType = i % 2 == 1 ? CameraEffectTypes.SHAKE : CameraEffectTypes.NONE;
			// alternate effect durations between 2 seconds and 4 seconds
			waypointObjects[i].effectDuration = i % 4 > 1 ? 2.0f : 4.0f;
			// alternate shake intensity between 2 and 4 (relative magnitude)
			waypointObjects[i].shakeIntensity = i % 4 > 1 ? 2.0f : 4.0f;
		}
		
		StartCoroutine(WaypointEngine()); // process the array of waypoints
	} // end method Start
	
	IEnumerator WaypointEngine()
	{
		MovementTypes wp; // create a waypoint object pointer to use in the for loop
//		foreach(MovementTypes wp in waypointObjects)
		// iterate through all the waypoints in the waypoints array
		for (int i = 0; i < waypointObjects.Length; i++)
		{
			wp = waypointObjects[i]; // point wp to the current waypoint
			switch(wp.effectType) // check for camera effects
			{
			case CameraEffectTypes.SHAKE:
				StartCoroutine(ShakeCamera(wp.effectDuration, wp.shakeIntensity));
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			case CameraEffectTypes.FADE:
				break;
			case CameraEffectTypes.SPLATTER:
				break;
			case CameraEffectTypes.NONE:
//				StartCoroutine(WaitCamera(wp.effectDuration));
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			default:
				Debug.Log ("Invalid camera effect!");
				break;
			} // end switch
		} // end for
		yield return null;
	} // end method WaypointEngine
	
	IEnumerator ShakeCamera(float shakeSeconds, float shakeIntensity)
	{
		float shakeMultiplier = 0.3f; // arbitrary number to adjust shaking intensity
		float elapsedTime = 0f; // keeps track of elapsed time to continue camera shaking
		float randomValueX; // x-axis random additive value
		float randomValueY; // y-axis random additive value
		Vector3 originalCameraPosition = mainCamera.transform.position;

		shakeIntensity = shakeMultiplier * shakeIntensity; // fine-tume the shaking intensity
		while (elapsedTime < shakeSeconds) // iterate through the loop for shakeSeconds seconds
		{
			randomValueX = shakeIntensity * (Random.value - 0.5f);
			randomValueY = shakeIntensity * (Random.value - 0.5f);
			// move the main camera to a slightly different position based on randomValueX and randomValueY
			mainCamera.transform.position = new Vector3(originalCameraPosition.x + randomValueX,
			                                            originalCameraPosition.y + randomValueY,
			                                            originalCameraPosition.z);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		// return the main camera to its original position
		mainCamera.transform.position = originalCameraPosition;
		yield return null;
	} // end method ShakeCamera
} // end class WaypointController
