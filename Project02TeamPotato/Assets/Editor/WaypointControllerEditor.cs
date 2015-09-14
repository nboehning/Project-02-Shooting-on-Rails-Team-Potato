using UnityEngine;
using System.Collections;
using UnityEditor;

// Custom inspector for the WaypointController
// @author: Nathan Boehning
[CustomEditor(typeof(WaypointController))]
public class WaypointControllerEditor : Editor
{

    // Method to see whether element is at first position of array or not
    bool CanMoveUp(int i)
    {
        // Return whether the index is at first position of array
        return i >= 1;

    }
    
    // Method to see whether element is at last position of array or not
    bool CanMoveDown(int i)
    {
        // Return whether the index is at end of the array
        return i < serializedObject.FindProperty("waypointObjects").arraySize - 1;
  
    }

    // Move an element upwards in the array.
    private void SwapUp(int index1, int index2)
    {
        // Access the array as serialized object
        SerializedProperty controller = serializedObject.FindProperty(("waypointObjects"));

        // Insert another element at target position
        controller.InsertArrayElementAtIndex(index1);

        // Move array element into new position
        controller.MoveArrayElement(index2, index1);

        // Move other element upwards into old index
        controller.MoveArrayElement(index1 + 1, index2);

        // Reduce the array size to get rid of extra element
        int oldSize = controller.arraySize;
        controller.DeleteArrayElementAtIndex(index1);
        if (controller.arraySize == oldSize)
        {
            controller.DeleteArrayElementAtIndex(index1);
        }
    }

    // overrides the inspect GUI
    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Access the array
        SerializedProperty controller = serializedObject.FindProperty(("waypointObjects"));

        // Create a field for the array
        EditorGUILayout.PropertyField(controller);

        // If the array is expanded to show the individual elements
        if (controller.isExpanded)
        {
            // Create label showing the number of waypoints created in array
            EditorGUILayout.LabelField("Number of Waypoints: " + controller.arraySize);

            // Increase indent level for formatting
            EditorGUI.indentLevel++;

            // Loop through all elements of the array
            for (int i = 0; i < controller.arraySize; i++)
            {
                // Set indent level for formatting
                EditorGUI.indentLevel = 1;
                
                // Create a label to show the waypoint position within the array
                EditorGUILayout.LabelField("Waypoint " + (i + 1), EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();

                // Create the movement type property field from the array
                EditorGUILayout.PropertyField(controller.GetArrayElementAtIndex(i));

                // Check to see if element can move up in the array
                if (CanMoveUp(i))
                {
                    // If it can, create a button that allows designer to move the element upwards
                    if (GUILayout.Button("U", GUILayout.Width(25f)))
                    {
                        SwapUp(i, i - 1);
                    }
                }

                // Check to see if element can move down in the array
                if (CanMoveDown(i))
                {
                    // If it can, create a button that allows designer to move element downwards
                    if (GUILayout.Button("D", GUILayout.Width(25f)))
                    {
                        SwapUp(i + 1, i);
                    }
                }

                Color oldColor = GUI.color;
                GUI.color = Color.red;
                // Create a red button that removes the element from the array of waypoints
                if (GUILayout.Button("X", GUILayout.Width(25f)))
                {
                    // Code to remove the current element from the array
                    // @author: Tiffany Fischer
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
                // Revert the GUI color to previous color before red button
                GUI.color = oldColor;

                EditorGUILayout.EndHorizontal();
            }

            // Create a button that adds a waypoint to the end of the array
            if (GUILayout.Button("Add Waypoint"))
            {
                controller.arraySize += 1;
                EditorGUILayout.PropertyField(controller.GetArrayElementAtIndex(controller.arraySize - 1));
            }

        }

        // Apply the modified properties to the array of waypoints (serialized object)
        serializedObject.ApplyModifiedProperties();
    }
}
