using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    private SerializedObject myObject;
    private SerializedProperty duration;

    private Waypoint waypointScript;
    private bool isFixedPointFacing;

    void Awake()
    {
        myObject = new SerializedObject(target);
        duration = myObject.FindProperty("durationOfWaypoint");

    }

    private static string sArraySizePath = "waypoints.Array.size";
    private static string sArrayData = "waypoints.Array.data[{0}]";

    private Waypoint[] GetWaypointArray()
    {
        var arrayCount = myObject.FindProperty(sArraySizePath).intValue;
        var transformArray = new Waypoint[arrayCount];

        for (int i = 0; i < arrayCount; i++)
        {
            transformArray[i] = myObject.FindProperty(string.Format(sArrayData, i)).objectReferenceValue as Waypoint;    
        }

        return transformArray;
    }

    private void SetWaypoint(int index, Waypoint waypoint)
    {
        myObject.FindProperty(string.Format(sArrayData, index)).objectReferenceValue = waypoint;
    }
    public override void OnInspectorGUI()
    {
        myObject.Update();

        GUILayout.Label("Waypoint Controller", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(duration);
        if (duration.floatValue < 0)
            duration.floatValue = 0;

        GUILayout.Label("Waypoints", EditorStyles.boldLabel);
        var waypoints = GetWaypointArray();
        //for (var i = 0; i < waypoints.Length; i++)
        //{
        //    Waypoint result = (Waypoint) EditorGUILayout.ObjectField(waypoints[i], typeof(Waypoint));
        //    if (GUI.changed)
        //        SetWaypoint(i, result);
        //}
        myObject.ApplyModifiedProperties();
    }
}
