using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WaypointController))]
public class WaypointControllerEditor : Editor
{

    private WaypointController controllerScript;

    void Awake()
    {
        controllerScript = (WaypointController) target;
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty controller = serializedObject.FindProperty(("waypointObjects"));

        EditorGUILayout.PropertyField(controller);
        if (controller.isExpanded)
        {
            EditorGUILayout.LabelField("Size of array: " + controller.arraySize);
            EditorGUI.indentLevel++;
            for (int i = 0; i < controller.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(controller.GetArrayElementAtIndex(i));

                GUI.color = Color.red;
                if (GUILayout.Button("X", GUILayout.Width(25f)))
                {
                    
                    if(controller.GetArrayElementAtIndex(i).objectReferenceValue != null)
                        controller.DeleteArrayElementAtIndex(i);
                    controller.DeleteArrayElementAtIndex(i);
                }
                GUI.color = Color.white;

                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Waypoint"))
            {
                controller.arraySize += 1;
                EditorGUILayout.PropertyField(controller.GetArrayElementAtIndex(controller.arraySize - 1));
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
