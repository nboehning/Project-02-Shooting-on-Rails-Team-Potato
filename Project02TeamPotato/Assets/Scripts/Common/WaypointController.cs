using UnityEngine;
using System.Collections;
using System.Text;

public class WaypointController : MonoBehaviour
{
    public MovementTypes[] waypointObjects;

    public Vector3 startPos, endPos;

    public static  float waitTime = 5;

    /// <summary>
    /// @author Jake Skov
    /// </summary>
    public void OnDrawGizmos()
    {
        foreach (MovementTypes wp in waypointObjects)
        {
            switch (wp.moveType)
            {
                case MovementType.STRAIGHTLINE:
                    Gizmos.DrawLine(wp.point1.position, wp.point2.position);
                    break;

                case MovementType.BEZIERCURVE:
                    Gizmos.color = Color.green;
                    Vector3 lineStarting = wp.point1.transform.position;
                    for (int i = 1; i <= 1; i++)
                    {
                        Vector3 lineEnd = GetPoint(wp.point1.transform.position, wp.point2.transform.position, wp.point3.transform.position, i / 10f);
                        Gizmos.DrawLine(lineStarting, lineEnd);
                        lineStarting = lineEnd;
                    }
                    break;

                case MovementType.WAIT:
                    break;

                case MovementType.LOOKCHAIN:
                    break;
            }
        }
    }

    /// <summary>
    /// @Author Jake Skov
    /// </summary>
    /// <param name="start">Start Point</param>
    /// <param name="end">End Point</param>
    /// <param name="curve">Sets angle</param>
    /// <param name="t">sets up bezier</param>
    /// <returns>Curve</returns>
    public static Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end;
    }

    /// <summary>
    /// @Author Jake Skov
    /// </summary>
    /// <returns>Makes the program wait for waitTime Seconds</returns>
    public static IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
    }
    

	private GameObject mainCamera;

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
