using UnityEngine;
using System.Collections;
using System.Text;

public enum MovementType
{
    StraightLine,
    BezierCurve,
    LookAndReturn,
    Pause
}

public enum EffectType
{
    Shake,
    Splatter,
    Fade
}

[System.Serializable]
public class Waypoint : MonoBehaviour
{
    public float durationOfWaypoint;
    public Waypoint[] waypoints;

    #region Facing

    //private bool isFixedPointFacing = false;
    public bool isFixedPointFacing
    {
        get { return isFixedPointFacing; }
        set { isFixedPointFacing = value; }
    }

    #endregion

    #region Movement

    public MovementType[] possibleMovements;
    private MovementType curMovementType;

    #endregion

    #region CameraEffect

    public EffectType[] possibleEffects;
    private EffectType curEffectType;

    #endregion

}
