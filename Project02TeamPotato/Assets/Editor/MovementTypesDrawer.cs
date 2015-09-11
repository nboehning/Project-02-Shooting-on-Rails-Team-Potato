using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(MovementTypes))]
public class MovementTypesDrawer : PropertyDrawer
{

    private MovementTypes thisObject;

    private float extraHeight = 150f;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect movementTypeDisplay = new Rect(position.x, position.y + 2, position.width, 15f);

        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty movementType = property.FindPropertyRelative("moveType");

        SerializedProperty point1 = property.FindPropertyRelative("point1");
        SerializedProperty point2 = property.FindPropertyRelative("point2");
        SerializedProperty point3 = property.FindPropertyRelative("point3");

        SerializedProperty lookTime1 = property.FindPropertyRelative("lookTime1");
        SerializedProperty lookTime2 = property.FindPropertyRelative("lookTime2");
        SerializedProperty lookTime3 = property.FindPropertyRelative("lookTime3");

        SerializedProperty duration = property.FindPropertyRelative("waypointDuration");

        SerializedProperty facingType = property.FindPropertyRelative("facingType");
        SerializedProperty lookPoint = property.FindPropertyRelative("lookPoint");

        SerializedProperty effectType = property.FindPropertyRelative("effectType");
        SerializedProperty shakeIntensity = property.FindPropertyRelative("shakeIntensity");
        SerializedProperty splatterFade = property.FindPropertyRelative("splatterFade");
        SerializedProperty splatterFadeSpeed = property.FindPropertyRelative("splatterFadeSpeed");

        EditorGUI.PropertyField(movementTypeDisplay, movementType, GUIContent.none);

        switch ((MovementType) movementType.enumValueIndex)
        {
            case MovementType.STRAIGHTLINE:
                EditorGUI.indentLevel++;
                float offsetX = position.x;

                Rect waypointDurationRect = new Rect(offsetX, position.y + 20, position.width/2, 15f);
                offsetX += 35;
                EditorGUI.PropertyField(waypointDurationRect, duration);

                Rect startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
                offsetX += 77;
                EditorGUI.LabelField(startingPointLabelRect, "Starting Point");

                Rect startingPointRect = new Rect(offsetX, position.y + 37, position.width / 2, 15f);
                EditorGUI.PropertyField(startingPointRect, point1, GUIContent.none);

                Rect endingPointLabelRect = new Rect(position.x, position.y + 54, position.width, 17f);
                EditorGUI.LabelField(endingPointLabelRect, "Ending Point");

                Rect endingPointRect = new Rect(offsetX, position.y + 54, position.width / 2, 15f);
                EditorGUI.PropertyField(endingPointRect, point2, GUIContent.none);

                Rect facingTypeLabelRect = new Rect(position.x, position.y + 71, position.width, 17f);
                EditorGUI.LabelField(facingTypeLabelRect, "Facing Type");

                Rect facingTypeRect = new Rect(offsetX, position.y + 71, position.width / 2, 15f);
                EditorGUI.PropertyField(facingTypeRect, facingType, GUIContent.none);

                if ((FacingTypes) facingType.enumValueIndex == FacingTypes.FIXEDPOINT)
                {
                    EditorGUI.indentLevel++;

                    Rect lookPointLabelRect = new Rect(position.x, position.y + 88, position.width, 17f);
                    EditorGUI.LabelField(lookPointLabelRect, "Look At Point");

                    Rect lookPointRect = new Rect(offsetX, position.y + 88, position.width / 2, 15f);
                    EditorGUI.PropertyField(lookPointRect, lookPoint, GUIContent.none);

                    EditorGUI.indentLevel--;

                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 105, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 105, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        EditorGUI.indentLevel++;
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        
                    }
                    else if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        
                    }
                }
                else
                {
                    
                }
                EditorGUI.indentLevel--;
                break;
            case MovementType.BEZIERCURVE:
                break;
            case MovementType.LOOKANDRETURN:
                break;
            case MovementType.WAIT:
                break;
        }

        EditorGUI.EndProperty();
    }
    
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + extraHeight;
    }
}
