/*using UnityEngine;
using System.Collections;

public class ScriptMovementMasterClass : MonoBehaviour
{

    Vector3 startPos, endPos;
    public float time;
    MovementTypes[] moveType;
    // Use this for initialization
    void Start()
    {
        foreach (MovementTypes wp in moveType)
        {
            switch (wp.moveType)
            {
                case MovementType.BEZIERCURVE:
                    startPos = wp.point1.transform.position;
                    for (int i = 1; i <= 1; i++)
                    {
                        endPos = WaypointController.GetPoint(wp.point1.transform.position, wp.point2.transform.position
                            , wp.point3.transform.position, i / 10f);
                        Gizmos.DrawLine(startPos, endPos);
                        startPos = endPos;
                    }
                    break;
                case MovementType.STRAIGHTLINE:
                    startPos = wp.point1.transform.position;
                    endPos = wp.point2.transform.position;
                    break;
                case MovementType.WAIT:

                    break;
                //case MovementType.LOOKCHAIN:

                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MovementTypes wp in moveType)
        {
            switch (wp.moveType)
            {
                case MovementType.BEZIERCURVE:
                    startPos = wp.point1.transform.position;
                    for (int i = 1; i <= 1; i++)
                    {
                        endPos = WaypointController.GetPoint(wp.point1.transform.position, wp.point2.transform.position, wp.point3.transform.position, i / 10f);
                        Vector3.Lerp(startPos, endPos, time);
                        startPos = endPos;
                    }
                    break;
                case MovementType.STRAIGHTLINE:
                    startPos = wp.point1.transform.position;
                    endPos = wp.point2.transform.position;
                    Vector3.Lerp(startPos, endPos, time);
                    break;
                case MovementType.WAIT:
                    StartCoroutine("WaypointController.WaitTime");
                    Vector3.Lerp(startPos, endPos, time);
                    break;
              //  case MovementType.LOOKCHAIN:

                    Vector3.Lerp(startPos, endPos, time);
                    break;
            }
        }
    }
}
*/