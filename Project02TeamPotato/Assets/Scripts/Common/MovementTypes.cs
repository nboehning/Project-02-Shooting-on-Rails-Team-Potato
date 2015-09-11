using UnityEngine;
using System.Collections;

public enum MovementType
{
    STRAIGHTLINE,
    BEZIERCURVE,
    LOOKANDRETURN,
    WAIT
}

public enum CameraEffectTypes
{
    SPLATTER,
    SHAKE,
    FADE,
    NONE
}

public enum FacingTypes
{
    FREELOOK,
    FIXEDPOINT
}

[System.Serializable]
public class MovementTypes
{
    public MovementType moveType;
    public float waypointDuration;

    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform lookPoint;

    public float lookTime1;
    public float lookTime2;
    public float lookTime3;

    public FacingTypes facingType;
    public CameraEffectTypes effectType;
	public float effectDuration;

    public float splatterFadeSpeed;
    public bool splatterFade;
    public float shakeIntensity;
}
