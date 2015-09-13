using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class WaypointController : MonoBehaviour
{
    public MovementTypes[] waypointObjects;
/*
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
                    Vector3 lineStarting = GetPoint();
                    for (int i = 1; i <= 1; i++)
                    {
                        Vector3 lineEnd = GetPoint(startPos, endPos, wp.bezierCurve.position, i / 10f);
                        Gizmos.DrawLine(lineStarting, lineEnd);
                        lineStarting = lineEnd;
                    }
                    break;

                case MovementTypes.LOOKRETURN:
                    break;

                case MovementTypes.LOOKCHAIN:
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
    public Vector3 GetPoint(Vector3 start, Vector3 end, Vector3 curve, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end;
    }

    /// <summary>
    /// @Author Jake Skov
    /// </summary>
    /// <returns>Makes the program wait for waitTime Seconds</returns>
    public IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
    }
    */

	private GameObject mainCamera;
	private GameObject canvasObject;
	private GameObject canvasBackground;
	private GameObject splatterForeground;

	void Start ()
	{
		InitCameraEffects(); // initialize the camera effects objects

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
			// alternate fade effect between fading-in and fading-out
			waypointObjects[i].isFadedOut = i % 4 > 1 ? true : false;
			waypointObjects[i].waypointDuration = waypointObjects[i].effectDuration;
			waypointObjects[i].moveType = MovementType.STRAIGHTLINE;
			waypointObjects[i].facingType = FacingTypes.FREELOOK;
		}

		// setup special waypoints for testing fading and splatter camera effects
		waypointObjects[0].effectType = CameraEffectTypes.FADE;
		waypointObjects[0].isFadedOut = false;
		waypointObjects[1].effectType = CameraEffectTypes.FADE;
		waypointObjects[1].isFadedOut = true;
		waypointObjects[2].effectType = CameraEffectTypes.SPLATTER;
		waypointObjects[2].effectDuration = 10.0f;
		waypointObjects[2].fadeTime = 2.0f;
		waypointObjects[2].splatterFade = true;
		waypointObjects[2].waypointDuration = 10.0f;
		waypointObjects[5].effectType = CameraEffectTypes.SPLATTER;
		waypointObjects[5].effectDuration = 4.0f;
		waypointObjects[5].fadeTime = 2.0f;
		waypointObjects[5].splatterFade = false;
		waypointObjects[5].waypointDuration = 4.0f;
		waypointObjects[8].effectType = CameraEffectTypes.FADE;
		waypointObjects[8].isFadedOut = false;
		waypointObjects[9].effectType = CameraEffectTypes.FADE;
		waypointObjects[9].isFadedOut = true;

		StartCoroutine(WaypointEngine()); // process the array of waypoints
	} // end method Start

	private void InitCameraEffects()
	{
		mainCamera = GameObject.FindWithTag("MainCamera"); // find the main camera by its tag
		canvasObject = GameObject.Find("Canvas");
		canvasBackground = GameObject.Find("FadeBackgroundPanel");
		splatterForeground = GameObject.Find("SplatterBackgroundPanel");
		canvasObject.SetActive(false);
		canvasBackground.GetComponent<Image>().enabled = true;
		splatterForeground.GetComponent<Image>().enabled = true;
	} // end method InitCameraEffects

	IEnumerator WaypointEngine()
	{
		MovementTypes wp; // create a waypoint object pointer to use in the for loop
		// iterate through all the waypoints in the waypoints array
		for (int i = 0; i < waypointObjects.Length; i++)
		{
			wp = waypointObjects[i]; // point wp to the current waypoint
			StartCoroutine(RunMovement(wp));
			StartCoroutine(RunFacing(wp));
			StartCoroutine(RunCameraEffect(wp));
			yield return new WaitForSeconds(wp.waypointDuration);
		} // end for
		yield return null;
	} // end method WaypointEngine
	
	IEnumerator RunMovement(MovementTypes wp)
	{
		switch(wp.moveType)
		{
			case MovementType.BEZIERCURVE:
				break;
			case MovementType.LOOKANDRETURN:
				break;
			case MovementType.STRAIGHTLINE:
				StartCoroutine(MoveStraightLine(wp.waypointDuration));
				yield return new WaitForSeconds(wp.waypointDuration);
				break;
			case MovementType.WAIT:
				yield return new WaitForSeconds(wp.waypointDuration);
				break;
			default:
				Debug.Log ("Invalid movement type!");
				break;
		} // end switch
		yield return null;
	} // end method RunMovement
	
	IEnumerator MoveStraightLine(float moveSeconds)
	{
		float elapsedTime = 0f; // keeps track of elapsed time to continue movement
		int i = 0; // counter for displaying loop count
		string outputString;

		while (elapsedTime < moveSeconds) // iterate through the loop for moveSeconds seconds
		{
			i++;
			outputString = "Straight line movement #" + i.ToString();
			Debug.Log (outputString);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return null;
	} // end method MoveStraightLine
	
	IEnumerator RunFacing(MovementTypes wp)
	{
		switch(wp.facingType)
		{
			case FacingTypes.FIXEDPOINT:
				break;
			case FacingTypes.FREELOOK:
				StartCoroutine(FaceFreeLook(wp.waypointDuration));
				yield return new WaitForSeconds(wp.waypointDuration);
				break;
			default:
				Debug.Log ("Invalid facing type!");
				break;
		} // end switch
		yield return null;
	} // end method RunFacing
	
	IEnumerator FaceFreeLook(float faceSeconds)
	{
		float elapsedTime = 0f; // keeps track of elapsed time to continue facing
		int i = 0; // counter for displaying loop count
		string outputString;
		
		while (elapsedTime < faceSeconds) // iterate through the loop for faceSeconds seconds
		{
			i++;
			outputString = "Facing free look #" + i.ToString();
			Debug.Log (outputString);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return null;
	} // end method FaceFreeLook

	IEnumerator RunCameraEffect(MovementTypes wp)
	{
		switch(wp.effectType) // check for camera effects
		{
			case CameraEffectTypes.SHAKE:
				StartCoroutine(ShakeCamera(wp.effectDuration, wp.shakeIntensity));
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			case CameraEffectTypes.FADE:
				StartCoroutine(FadeCamera(wp.effectDuration, wp.isFadedOut));
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			case CameraEffectTypes.SPLATTER:
				StartCoroutine(SplatterCamera(wp.effectDuration, wp.splatterFade, wp.fadeTime));
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			case CameraEffectTypes.NONE:
				yield return new WaitForSeconds(wp.effectDuration);
				break;
			default:
				Debug.Log ("Invalid camera effect!");
				break;
		} // end switch
		yield return null;
	} // end method RunCameraEffect

	IEnumerator ShakeCamera(float shakeSeconds, float shakeIntensity)
	{
		float shakeMultiplier = 0.3f; // arbitrary number to adjust shaking intensity
		float elapsedTime = 0f; // keeps track of elapsed time to continue camera shaking
		float randomValueX; // x-axis random additive value
		float randomValueY; // y-axis random additive value
		Vector3 originalCameraPosition = mainCamera.transform.position;
		
		shakeIntensity = shakeMultiplier * shakeIntensity; // fine-tune the shaking intensity
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
	
	IEnumerator FadeCamera(float fadeSeconds, bool fadeIn)
	{
		float elapsedTime = 0f; // keeps track of elapsed time to fade the camera
		float elapsedPercent = 0f; // elapsedTime / fadeSeconds
		Color canvasColor = new Color(0f, 0f, 0f, 0f); // assume we're fading out (transparent screen)
		
		splatterForeground.SetActive(false);
		canvasBackground.SetActive(true);
		if (fadeIn) // we're fading in
			canvasColor.a = 1.0f; // set the screen to opaque
		canvasBackground.GetComponent<Image>().color = canvasColor;
		canvasObject.SetActive(true); // turn on the canvas
		while (elapsedTime < fadeSeconds) // iterate through the loop for fadeSeconds seconds
		{
			elapsedPercent = elapsedTime / fadeSeconds;
			if (fadeIn) // we're fading in
				canvasColor.a = Mathf.Lerp(1.0f, 0f, elapsedPercent);
			else // we're fading out
				canvasColor.a = Mathf.Lerp(0f, 1.0f, elapsedPercent);
			canvasBackground.GetComponent<Image>().color = canvasColor;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		if (fadeIn) // we're fading in
			canvasObject.SetActive(false); // turn off the canvas
		else // we're fading out
		{
			canvasColor.a = 1.0f; // set the screen to opaque
			canvasBackground.GetComponent<Image>().color = canvasColor;
		}
		yield return null;
	} // end method FadeCamera

	IEnumerator SplatterCamera(float splatterSeconds, bool fadeIn, float fadeSeconds)
	{
		float elapsedTime = 0f; // keeps track of elapsed time for the splatter effect
		float elapsedPercent = 0f; // fadeTime / fadeSeconds
		Color canvasColor = new Color(0f, 0f, 0f, 1.0f); // assume we're not fading (opaque screen)
		
		canvasBackground.SetActive(false);
		splatterForeground.SetActive(true);
		splatterForeground.GetComponent<Image>().enabled = true;
		if (!fadeIn) // we're not fading the splatter effect in
		{
			splatterForeground.GetComponent<Image>().color = canvasColor;
			canvasObject.SetActive(true); // turn on the canvas
			while (elapsedTime < splatterSeconds) // iterate through the loop for splatterSeconds seconds
			{
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
		else // we're fading the splatter effect in
		{
			canvasColor.a = 0f; // set the screen to transparent
			splatterForeground.GetComponent<Image>().color = canvasColor;
			canvasObject.SetActive(true); // turn on the canvas
			while (elapsedTime < splatterSeconds) // iterate through the loop for splatterSeconds seconds
			{
				if (elapsedTime < fadeSeconds)
				{
					elapsedPercent = elapsedTime / fadeSeconds;
					canvasColor.a = Mathf.Lerp(0f, 1.0f, elapsedPercent);
				}
				else if (elapsedTime > splatterSeconds - fadeSeconds)
				{
					elapsedPercent = (elapsedTime + fadeSeconds - splatterSeconds) / fadeSeconds;
					canvasColor.a = Mathf.Lerp(1.0f, 0f, elapsedPercent);
				}
				else
					canvasColor.a = 1.0f; // set the screen to opaque
				splatterForeground.GetComponent<Image>().color = canvasColor;
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}
		canvasObject.SetActive(false); // turn off the canvas
		yield return null;
	} // end method SplatterCamera
} // end class WaypointController
