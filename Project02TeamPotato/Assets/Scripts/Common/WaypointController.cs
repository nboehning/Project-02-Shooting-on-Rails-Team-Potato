using UnityEngine;
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

}
