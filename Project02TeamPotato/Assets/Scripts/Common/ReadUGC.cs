using UnityEngine;
using System;
using System.Collections;
using System.IO;

// @author: Craig Broskow
public class ReadUGC : MonoBehaviour {
	
	FileInfo sourceFile = null;
	StreamReader reader = null;
	string filePath;
	string[] inputLines;
	
	// @author: Craig Broskow
	public bool UGCFileExists()
	{
		try
		{
			filePath = Application.dataPath + "/Dynamic Assets/Resources/UGC/potato.txt";
			sourceFile = new FileInfo (filePath);
			return (sourceFile.Exists);
		}
		catch (Exception e)
		{
			Debug.Log("ReadUGC.UGCFileExists() threw an exception!");
			Debug.Log("Exception Message: " + e.Message);
			return false;
		}
	} // end method UGCFileExists
	
	// @author: Craig Broskow
	public bool LoadUGCFile(ref MovementTypes[] waypointObjects)
	{
		char[] delimiterChars = { ' ', '=' };
		
		try
		{
			if (!sourceFile.Exists)
			{
				return false;
			}
			if (sourceFile != null && sourceFile.Exists)
				reader = sourceFile.OpenText();
			if (reader == null)
			{
				Debug.Log ("UGC stream reader is null!");
				return false;
			}
			else
			{
				inputLines = new string[100];
				int lineNumber = 0;
				int lineCount = 0;
				while (((lineNumber < 100) && (inputLines[lineNumber] = reader.ReadLine()) != null))
				{
					lineNumber++;
					lineCount++;
				}
				reader.Close();
				
				waypointObjects = new MovementTypes[lineCount];
				MovementTypes wp; // create a waypoint object pointer to use in the for loop
				for (int i = 0; i < lineCount; i++)
				{
					waypointObjects[i] = new MovementTypes();
					wp = waypointObjects[i]; // point wp to the current waypoint
					string[] paramArray = inputLines[i].Split(delimiterChars);
					for (int j = 0; j < paramArray.Length - 1; j = j + 2)
					{
						switch (paramArray[j])
						{
						case "DT":
							wp.fadeTime = Convert.ToSingle(paramArray[j+1]);
							break;
						case "ED":
							wp.effectDuration = Convert.ToSingle(paramArray[j+1]);
							break;
						case "EL":
							wp.effectDelay = Convert.ToSingle(paramArray[j+1]);
							break;
						case "ET":
							switch (Convert.ToInt32(paramArray[j+1]))
							{
							case 0:
								wp.effectType = CameraEffectTypes.SPLATTER;
								break;
							case 1:
								wp.effectType = CameraEffectTypes.SHAKE;
								break;
							case 2:
								wp.effectType = CameraEffectTypes.FADE;
								break;
							default:
								wp.effectType = CameraEffectTypes.NONE;
								break;
							}
							break;
						case "FT":
							wp.facingType = Convert.ToInt32(paramArray[j+1]) == 0 ? FacingTypes.FREELOOK : FacingTypes.FIXEDPOINT;
							break;
						case "IF":
							wp.isFadedOut = Convert.ToInt32(paramArray[j+1]) == 0 ? false : true;
							break;
						case "L1":
							wp.lookTime1 = Convert.ToSingle(paramArray[j+1]);
							break;
						case "L2":
							wp.lookTime2 = Convert.ToSingle(paramArray[j+1]);
							break;
						case "L3":
							wp.lookTime3 = Convert.ToSingle(paramArray[j+1]);
							break;
						case "LP":
							string[] LPArray = paramArray[j+1].Split(',');
							GameObject goLP = new GameObject();
							goLP.transform.position = new Vector3(Convert.ToSingle(LPArray[0]),
							                                      Convert.ToSingle(LPArray[1]),
							                                      Convert.ToSingle(LPArray[2]));
							wp.lookPoint = goLP.transform;
							break;
						case "MT":
							switch (Convert.ToInt32(paramArray[j+1]))
							{
							case 0:
								wp.moveType = MovementType.STRAIGHTLINE;
								break;
							case 1:
								wp.moveType = MovementType.BEZIERCURVE;
								break;
							case 2:
								wp.moveType = MovementType.LOOKANDRETURN;
								break;
							default:
								wp.moveType = MovementType.WAIT;
								break;
							}
							break;
						case "P1":
							string[] P1Array = paramArray[j+1].Split(',');
							GameObject goP1 = new GameObject();
							goP1.transform.position = new Vector3(Convert.ToSingle(P1Array[0]),
							                                      Convert.ToSingle(P1Array[1]),
							                                      Convert.ToSingle(P1Array[2]));
							wp.point1 = goP1.transform;
							break;
						case "P2":
							string[] P2Array = paramArray[j+1].Split(',');
							GameObject goP2 = new GameObject();
							goP2.transform.position = new Vector3(Convert.ToSingle(P2Array[0]),
							                                      Convert.ToSingle(P2Array[1]),
							                                      Convert.ToSingle(P2Array[2]));
							wp.point2 = goP2.transform;
							break;
						case "P3":
							string[] P3Array = paramArray[j+1].Split(',');
							GameObject goP3 = new GameObject();
							goP3.transform.position = new Vector3(Convert.ToSingle(P3Array[0]),
							                                      Convert.ToSingle(P3Array[1]),
							                                      Convert.ToSingle(P3Array[2]));
							wp.point3 = goP3.transform;
							break;
						case "SF":
							wp.splatterFade = Convert.ToInt32(paramArray[j+1]) == 0 ? false : true;
							break;
						case "SI":
							wp.shakeIntensity = Convert.ToSingle(paramArray[j+1]);
							break;
						case "WD":
							wp.waypointDuration = Convert.ToSingle(paramArray[j+1]);
							break;
						default:
							Debug.Log ("UGC file contains an invalid parameter: " + paramArray[j]);
							return false;
							break;
						}
					}
				}
			}
			return true;
		}
		catch (Exception e)
		{
			Debug.Log("ReadUGC.loadUGCFile() threw an exception!");
			Debug.Log("Exception Message: " + e.Message);
			return false;
		}
	} // end method loadUGCFile
} // end class ReadUGC
