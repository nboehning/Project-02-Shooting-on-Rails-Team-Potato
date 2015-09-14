using UnityEngine;
using UnityEngine.UI;
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
                    if(wp.point1 != null && wp.point2 != null)                  // @author: Nathan, created check to remove null reference excepttion error
                        Gizmos.DrawLine(wp.point1.position, wp.point2.position);
                    break;

                case MovementType.BEZIERCURVE:
                    Gizmos.color = Color.green;
                    if (wp.point1 != null && wp.point2 != null && wp.point3 != null)    // @author: Nathan, created check to remove null reference excepttion error
                    {
                        Vector3 lineStarting = wp.point1.transform.position;
                        for (int i = 1; i <= 10; i++)
                        {
                            Vector3 lineEnd = GetPoint(wp.point1.transform.position, wp.point2.transform.position,
                                wp.point3.transform.position, i/10f);
                            Gizmos.DrawLine(lineStarting, lineEnd);
                            lineStarting = lineEnd;
                        }
                    }
                    break;

                case MovementType.WAIT:
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
    

    // @author: Craig Broskow
	private GameObject mainCamera;
	private GameObject canvasObject;
	private GameObject canvasBackground;
	private GameObject splatterForeground;

    // @author: Craig Broskow
	void Start ()
	{
		InitCameraEffects(); // initialize the camera effects objects

		StartCoroutine(WaypointEngine()); // process the array of waypoints
	} // end method Start


    // @author: Craig Broskow
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

    // @author: Craig Broskow
    // Modified from testing to designer input by: Nathan Boehning
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
	
    // @author: Craig Broskow, created skeleton

	IEnumerator RunMovement(MovementTypes wp)
	{
		switch(wp.moveType)
		{
			case MovementType.BEZIERCURVE:
                StartCoroutine(MoveBezierCurve(wp.waypointDuration, wp.point1.transform.position, wp.point2.transform.position,
                    wp.point3.transform.position));
                yield return new WaitForSeconds(wp.waypointDuration);
                break;
			case MovementType.LOOKANDRETURN:
				break;
			case MovementType.STRAIGHTLINE:
				StartCoroutine(MoveStraightLine(wp.waypointDuration, wp.point1.position, wp.point2.position));
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
	
    // Author: Craig Broskow, created skeleton to test concurrent running
    // Modified by: Nathan, added jakes code from ScriptMovementMasterClass
	IEnumerator MoveStraightLine(float moveSeconds, Vector3 startPos, Vector3 endPos)
	{

        Vector3.Lerp(startPos, endPos, moveSeconds);

		yield return null;
	} // end method MoveStraightLine

    // Author: Nathan Boehning, created method, implemented Jakes code from ScriptMovementMasterClass and formatted
    //         to work with this class.
    IEnumerator MoveBezierCurve(float moveSeconds, Vector3 startPos, Vector3 endPos, Vector3 controlPos)
    {
        for (int i = 1; i <= 1; i++)
        {
            endPos = GetPoint(startPos, endPos, controlPos, i / 10f);
            Vector3.Lerp(startPos, endPos, moveSeconds);
            startPos = endPos;
        }
        yield return null;
    }

    // @author: Craig Broskow, created skeleton to test concurrent running
    // Modified for actual facing code by: Nathan Boehning
	IEnumerator RunFacing(MovementTypes wp)
	{
        // If the movement type is look and return, no facing type will occur
	    if (wp.moveType == MovementType.LOOKANDRETURN)
	    {
	        yield return new WaitForSeconds(wp.waypointDuration);
	    }
	    else
	    {
	        switch (wp.facingType)
	        {
	            case FacingTypes.FIXEDPOINT:
	                StartCoroutine(FacingFixedPoint(wp.waypointDuration, wp.lookPoint));
	                yield return new WaitForSeconds(wp.waypointDuration);
	                break;
	            case FacingTypes.FREELOOK:
	                StartCoroutine(FacingFreeLook(wp.waypointDuration));
	                yield return new WaitForSeconds(wp.waypointDuration);

                    // Make cursor visible and able to move again
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
	            default:
	                Debug.Log("Invalid facing type!");
	                break;
	        } // end switch
	        yield return null;
	    }
	} // end method RunFacing

    // @author: Nathan Boehning

    IEnumerator FacingFixedPoint(float faceSeconds, Transform lookPoint)
    {
        float elapsedTime = 0f;     // variable to hold elapsed time

        while (elapsedTime < faceSeconds)
        {
            // Set the rotation of the mainCamera to look at the position
            mainCamera.transform.LookAt(lookPoint.position);

            // Increment the elapsed time by the change in time
            elapsedTime += Time.deltaTime;
        }

        yield return null;
    }
    // @author: Craig Broskow, testing for concurrent running
    // Modified for actual facing code by: Nathan Boehning
	IEnumerator FacingFreeLook(float faceSeconds)
	{
		float elapsedTime = 0f; // keeps track of elapsed time to continue facing
        float xRotation = 0f;
        float yRotation = 0f;
        float lookSensitivity = 0.5f;
        float curXRotation = mainCamera.transform.rotation.x;
        float curYRotation = mainCamera.transform.rotation.y;
        float lookSmoothDamp = 0.1f;
        float xRotationV = 0;
        float yRotationV = 0;

        // Make cursor invisible and locked in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        while (elapsedTime < faceSeconds) // iterate through the loop for faceSeconds seconds
	    {

            // Increment the yRotation using mouse input mulitplied by the mouse sensitivity
	        yRotation += Input.GetAxis("Mouse X")*lookSensitivity;

            // Decrement the xRotation using mouse input. (incrementing creates an inverted effect)
	        xRotation -= Input.GetAxis("Mouse Y")*lookSensitivity;

            // Lock the rotation so camera can only look straight up or straight down without going circular
	        xRotation = Mathf.Clamp(xRotation, -90, 90);

            // Smooth the rotation to prevent screen tearing
	        curXRotation = Mathf.SmoothDamp(curXRotation, xRotation, ref xRotationV, lookSmoothDamp);
	        curYRotation = Mathf.SmoothDamp(curYRotation, yRotation, ref yRotationV, lookSmoothDamp);

            // Set the rotation of the camera to new rotation
	        mainCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            // Increment the elapsed time by the change in time
	        elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    } // end method FaceFreeLook

    // @author: Craig Broskow
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

    // @author: Craig Broskow
	IEnumerator ShakeCamera(float shakeSeconds, float shakeIntensity)
	{
		float shakeMultiplier = 0.3f; // arbitrary number to adjust shaking intensity
		float elapsedTime = 0f; // keeps track of elapsed time to continue camera shaking
		float randomValueX; // x-axis random additive value
		float randomValueY; // y-axis random additive value
		Vector3 originalCameraPosition = mainCamera.transform.localPosition;
		
		shakeIntensity = shakeMultiplier * shakeIntensity; // fine-tune the shaking intensity
		while (elapsedTime < shakeSeconds) // iterate through the loop for shakeSeconds seconds
		{
			randomValueX = shakeIntensity * (Random.value - 0.5f);
			randomValueY = shakeIntensity * (Random.value - 0.5f);
			// move the main camera to a slightly different position based on randomValueX and randomValueY
			mainCamera.transform.localPosition = new Vector3(originalCameraPosition.x + randomValueX,
			                                            originalCameraPosition.y + randomValueY,
			                                            originalCameraPosition.z);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		// return the main camera to its original position
		mainCamera.transform.localPosition = originalCameraPosition;
		yield return null;
	} // end method ShakeCamera
	
    // @author: Craig Broskow
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

    // @author: Craig Broskow
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
