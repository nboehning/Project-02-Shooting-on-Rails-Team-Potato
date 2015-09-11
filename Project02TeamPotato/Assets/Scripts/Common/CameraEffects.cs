using UnityEngine;
using System.Collections;
using System.Timers;

public enum cameraTypes
{
	SHAKE,
	SPLATTER,
	FADE,
	WAIT
};

public class CameraEffects : Waypoint {

	public cameraTypes camType;
	public float shakeSeconds;
	public int shakeIntensity;
	public float waitSeconds;

	//public bool turnOnShaking = false;

	//private Vector3 originalCameraPosition;
	//private bool shakeTurnedOn = false;
	//private float randomValueX;
	//private float randomValueY;
	//private int shakeMilliSec;
	//private Timer shakeTimer = new Timer();

	//void Awake()
	//{
	//	shakeMilliSec = shakeSeconds > 0 ? shakeSeconds * 1000 : 1000;
	//	shakeTimer.Enabled = false;
	//	shakeTimer.AutoReset = false;
	//	shakeTimer.Interval = shakeMilliSec;
	//	shakeTimer.Elapsed += new ElapsedEventHandler(ShakeTimeOutEvent);
	//}
	
	//// Update is called once per frame
	//void Update ()
	//{
	//	if (turnOnShaking)
	//	{
	//		if (shakeTurnedOn)
	//			ShakeCamera();
	//		else
	//			StartShakingCamera();
	//	}
	//	else
	//	{
	//		if (shakeTurnedOn)
	//			StopShakingCamera();
	//	}
	//}
	
	//private void StartShakingCamera()
	//{
	//	originalCameraPosition = transform.localPosition;
	//	shakeMilliSec = shakeSeconds > 0 ? shakeSeconds * 1000 : 1000;
	//	shakeTimer.Interval = shakeMilliSec;
	//	shakeTimer.Start();
	//	shakeTurnedOn = true;
	//}
	
	//private void StopShakingCamera()
	//{
	//	shakeTurnedOn = false;
	//	transform.localPosition = originalCameraPosition;
	//}
	
	//private void ShakeCamera()
	//{
	//	randomValueX = 0.3f * shakeIntensity * (Random.value - 0.5f);
	//	randomValueY = 0.3f * shakeIntensity * (Random.value - 0.5f);
	//	transform.localPosition = new Vector3(originalCameraPosition.x + randomValueX,
	//	                                      originalCameraPosition.y + randomValueY, originalCameraPosition.z);
	//}

	//private void ShakeTimeOutEvent(object source, ElapsedEventArgs e)
	//{
	//	shakeTimer.Enabled = false;
	//	turnOnShaking = false;
	//}
} // end class CameraEffects
