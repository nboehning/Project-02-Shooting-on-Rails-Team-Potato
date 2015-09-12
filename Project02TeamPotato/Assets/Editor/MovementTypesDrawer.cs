﻿using UnityEngine;
using System.Collections;
using UnityEditor;

public enum MovementTypes
{
    BEZIERCURVE,
    STRAIGHTLINE,
    LOOKCHAIN,
    WAIT
}

[CustomPropertyDrawer(typeof(MovementTypes))]
public class MovementTypesDrawer : PropertyDrawer
{

    private MovementTypes thisObject;

    public GameObject[] waypoints;
    public Transform[] facings;

    private float extraHeight = 200f;
    private float waitTime;

    private bool isFadedOut = false;

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
        SerializedProperty effectDuration = property.FindPropertyRelative("effectDuration");

        SerializedProperty shakeIntensity = property.FindPropertyRelative("shakeIntensity");

        SerializedProperty splatterFade = property.FindPropertyRelative("splatterFade");
        SerializedProperty sIsFadedOut = property.FindPropertyRelative("isFadedOut");
        SerializedProperty fadeSpeed = property.FindPropertyRelative("fadeTime");

        EditorGUI.PropertyField(movementTypeDisplay, movementType, GUIContent.none);

        switch ((MovementTypes)movementType.enumValueIndex)
        {
            case MovementTypes.STRAIGHTLINE:
                #region StraightLine
                EditorGUI.indentLevel++;
                float offsetX = position.x;

                Rect waypointDurationRect = new Rect(position.x, position.y + 20, position.width, 15f);
                offsetX += position.width / 3;
                EditorGUI.PropertyField(waypointDurationRect, duration);

                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                Rect startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
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
                if ((FacingTypes)facingType.enumValueIndex == FacingTypes.FIXEDPOINT)
                {
                    #region StraightLine > FixedPoint
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

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDurationRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration);
                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationRect = new Rect(position.x, position.y + 157, position.width, 17f);
                            EditorGUI.PropertyField(fadeSplatterDurationRect, fadeSpeed);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122, position.width / 2, 17f);

                        EditorGUI.indentLevel++;
                        if (isFadedOut)
                        {
                            isFadedOut = false;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade In Time");
                        }
                        else
                        {
                            isFadedOut = true;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade Out Time");
                        }
                        EditorGUI.FloatField(fadeOutSpeedRect, fadeSpeed.floatValue);
                        EditorGUI.indentLevel--;

                    }
                }
                    #endregion
                else
                {
                    #region StraightLine > FreeLook
                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 88, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 88, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDurationRect = new Rect(position.x, position.y + 105, position.width, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration);
                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationRect = new Rect(position.x, position.y + 139, position.width, 17f);
                            EditorGUI.PropertyField(fadeSplatterDurationRect, fadeSpeed);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 105, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 105, position.width / 2, 17f);

                        EditorGUI.indentLevel++;
                        if (isFadedOut)
                        {
                            isFadedOut = false;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade In Time");
                        }
                        else
                        {
                            isFadedOut = true;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade Out Time");
                        }
                        EditorGUI.FloatField(fadeOutSpeedRect, fadeSpeed.floatValue);
                        EditorGUI.indentLevel--;

                    }
                }
                    #endregion
                EditorGUI.indentLevel--;
                #endregion
                break;
            case MovementTypes.BEZIERCURVE:
                #region BezierCurve
                EditorGUI.indentLevel++;
                offsetX = position.x;

                waypointDurationRect = new Rect(position.x, position.y + 20, position.width, 15f);
                offsetX += position.width / 3;
                EditorGUI.PropertyField(waypointDurationRect, duration);

                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
                EditorGUI.LabelField(startingPointLabelRect, "Starting Point");

                startingPointRect = new Rect(offsetX, position.y + 37, position.width / 2, 15f);
                EditorGUI.PropertyField(startingPointRect, point1, GUIContent.none);

                endingPointLabelRect = new Rect(position.x, position.y + 54, position.width, 17f);
                EditorGUI.LabelField(endingPointLabelRect, "Ending Point");

                endingPointRect = new Rect(offsetX, position.y + 54, position.width / 2, 15f);
                EditorGUI.PropertyField(endingPointRect, point2, GUIContent.none);

                Rect controlPointLabelRect = new Rect(position.x, position.y + 71, position.width, 17f);
                EditorGUI.LabelField(controlPointLabelRect, "Control Point");

                Rect controlPointRect = new Rect(offsetX, position.y + 71, position.width / 2, 15f);
                EditorGUI.PropertyField(controlPointRect, point3, GUIContent.none);

                facingTypeLabelRect = new Rect(position.x, position.y + 71 + 17, position.width, 17f);
                EditorGUI.LabelField(facingTypeLabelRect, "Facing Type");

                facingTypeRect = new Rect(offsetX, position.y + 71 + 17, position.width / 2, 15f);
                EditorGUI.PropertyField(facingTypeRect, facingType, GUIContent.none);
                if ((FacingTypes)facingType.enumValueIndex == FacingTypes.FIXEDPOINT)
                {
                    #region BezierCurve > FixedPoint
                    EditorGUI.indentLevel++;

                    Rect lookPointLabelRect = new Rect(position.x, position.y + 88 + 17, position.width, 17f);
                    EditorGUI.LabelField(lookPointLabelRect, "Look At Point");

                    Rect lookPointRect = new Rect(offsetX, position.y + 88 + 17, position.width / 2, 15f);
                    EditorGUI.PropertyField(lookPointRect, lookPoint, GUIContent.none);

                    EditorGUI.indentLevel--;

                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDurationRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration);
                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationRect = new Rect(position.x, position.y + 157 + 17, position.width, 17f);
                            EditorGUI.PropertyField(fadeSplatterDurationRect, fadeSpeed);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17, position.width / 2, 17f);

                        EditorGUI.indentLevel++;
                        if (isFadedOut)
                        {
                            isFadedOut = false;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade In Time");
                        }
                        else
                        {
                            isFadedOut = true;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade Out Time");
                        }
                        EditorGUI.FloatField(fadeOutSpeedRect, fadeSpeed.floatValue);
                        EditorGUI.indentLevel--;

                    }
                }
                    #endregion
                else
                {
                    #region BezierCurve > Freelook
                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 88 + 17, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 88 + 17, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDurationRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration);
                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                            EditorGUI.PropertyField(fadeSplatterDurationRect, fadeSpeed);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);

                        EditorGUI.indentLevel++;
                        if (isFadedOut)
                        {
                            isFadedOut = false;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade In Time");
                        }
                        else
                        {
                            isFadedOut = true;
                            sIsFadedOut.boolValue = isFadedOut;
                            EditorGUI.LabelField(fadedOutLabelRect, "Fade Out Time");
                        }
                        EditorGUI.FloatField(fadeOutSpeedRect, fadeSpeed.floatValue);
                        EditorGUI.indentLevel--;

                    }
                }
                    #endregion
                EditorGUI.indentLevel--;
                #endregion
                break;
            case MovementTypes.LOOKCHAIN:
                #region LookCkain
                int numFacings;
                //input for num facings
                for (int i = 0; i <= facings.GetLength(0); i++)
                { 
                    //input for each facing as a Transform
                }
                #endregion
                    break;
            case MovementTypes.WAIT:
                #region Wait
                //input for waitTime
                #endregion
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + extraHeight;
    }

    /// <summary>
    /// @author Jake Skov
    /// </summary>
    public void OnDrawGizmos()
    {
        foreach (GameObject wp in waypoints)
        {
            switch (wp.type)
            {
                case MovementTypes.STRAIGHTLINE:
                    Gizmos.DrawLine(startPos, endPos);
                    break;

                case MovementTypes.BEZIERCURVE:
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
}
