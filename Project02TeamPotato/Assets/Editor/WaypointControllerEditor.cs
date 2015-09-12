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

    bool CanMoveUp(int i)
    {

        return i >= 1;

    }

    bool CanMoveDown(int i)
    {

        return i < serializedObject.FindProperty("waypointObjects").arraySize - 1;
  
    }

    private void SwapUp(int index1, int index2)
    {
        SerializedProperty controller = serializedObject.FindProperty(("waypointObjects"));

        controller.InsertArrayElementAtIndex(index1);
        controller.MoveArrayElement(index2, index1);

        controller.MoveArrayElement(index1 + 1, index2);

        int oldSize = controller.arraySize;
        controller.DeleteArrayElementAtIndex(index1);
        if (controller.arraySize == oldSize)
        {
            controller.DeleteArrayElementAtIndex(index1);
        }
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

                if (CanMoveUp(i))
                {
                    if (GUILayout.Button("U", GUILayout.Width(25f)))
                    {
                        SwapUp(i, i - 1);
                    }
                }
                if (CanMoveDown(i))
                {
                    if (GUILayout.Button("D", GUILayout.Width(25f)))
                    {
                        SwapUp(i + 1, i);
                    }
                }

                GUI.color = Color.red;
                if (GUILayout.Button("X", GUILayout.Width(25f)))
                {
					int oldSize = controller.arraySize;
					controller.DeleteArrayElementAtIndex(i);
					if(controller.arraySize == oldSize)
					{
						controller.DeleteArrayElementAtIndex(i);
					}
                    /*if(controller.GetArrayElementAtIndex(i).objectReferenceValue != null)
                        controller.DeleteArrayElementAtIndex(i);
                    controller.DeleteArrayElementAtIndex(i);*/
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
