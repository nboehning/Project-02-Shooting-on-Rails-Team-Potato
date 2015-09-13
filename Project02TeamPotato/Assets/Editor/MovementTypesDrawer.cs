using UnityEngine;
using System.Collections;
using UnityEditor;


/// <summary>
/// Creates the drawers of the custom inspector for the different waypoints.
/// @author Nathan
/// </summary>
[CustomPropertyDrawer(typeof(MovementTypes))]
public class MovementTypesDrawer : PropertyDrawer
{

    private MovementTypes thisObject;

    // Variable to create more room between the different elemenets in the waypoint array
    private float extraHeight = 200f;

    // bool to flipflop whether or not the camera is fadedin/out
    private bool isFadedOut = false;

    // Overrides the built in OnGUI method
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Creates position where the array name goes.
        Rect movementTypeDisplay = new Rect(position.x, position.y + 2, position.width, 15f);
        // Begins the definition of the property
        EditorGUI.BeginProperty(position, label, property);

        // Calls in all of the variables of the waypoint as a serialized property.

        // Holds the movement type of the waypoint
        SerializedProperty movementType = property.FindPropertyRelative("moveType");

        // Holds the different points (as transform) that could be used for various movements
        SerializedProperty point1 = property.FindPropertyRelative("point1");
        SerializedProperty point2 = property.FindPropertyRelative("point2");
        SerializedProperty point3 = property.FindPropertyRelative("point3");

        // Holds the variables necessary for the look and return movement type
        SerializedProperty lookTime1 = property.FindPropertyRelative("lookTime1");
        SerializedProperty lookTime2 = property.FindPropertyRelative("lookTime2");
        SerializedProperty lookTime3 = property.FindPropertyRelative("lookTime3");

        // Holds the duration that the waypoint will be
        SerializedProperty duration = property.FindPropertyRelative("waypointDuration");

        // Holds the facing type of the waypoint
        SerializedProperty facingType = property.FindPropertyRelative("facingType");
        
        // Holds the point that the camera will look at for a fixedpoint facing type
        SerializedProperty lookPoint = property.FindPropertyRelative("lookPoint");

        // Holds the camera effect type of the waypoint
        SerializedProperty effectType = property.FindPropertyRelative("effectType");
        // Delay before the effect occurs
        SerializedProperty effectDelay = property.FindPropertyRelative("effectDelay");
        // How long the effect will occur
        SerializedProperty effectDuration = property.FindPropertyRelative("effectDuration");

        // Variable necessary for the shake camera effect
        SerializedProperty shakeIntensity = property.FindPropertyRelative("shakeIntensity");

        // Boolean for whether or not the splatter will fade
        SerializedProperty splatterFade = property.FindPropertyRelative("splatterFade");
        // Serialized boolean to hold whether or not the camera will be faded out or not
        SerializedProperty sIsFadedOut = property.FindPropertyRelative("isFadedOut");
        // How long for the splatter to fade
        SerializedProperty fadeSpeed = property.FindPropertyRelative("fadeTime");

        // Displays the movement type of the waypoint
        EditorGUI.PropertyField(movementTypeDisplay, movementType, GUIContent.none);

        // Switch statement for the different move types
        Rect waypointDurationLabelRect;
        Rect waypointDurationRect;
        Rect facingTypeLabelRect;
        switch ((MovementType)movementType.enumValueIndex)
        {
            // Straight line movement type
            case MovementType.STRAIGHTLINE:
                #region StraightLine

                EditorGUI.indentLevel = 2;
                float offsetX = position.x + (position.width / 3);

                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

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

                facingTypeLabelRect = new Rect(position.x, position.y + 71, position.width, 17f);
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

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 139, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 139, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);
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
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 191 - 17, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 191 - 17, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);
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

                    if ((CameraEffectTypes) effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105, position.width/2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 122, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 122, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);

                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;
                    if (effectDelay.floatValue < 0f)
                        effectDelay.floatValue = 0f;

                    if (effectDelay.floatValue > duration.floatValue)
                    {
                        effectDelay.floatValue = duration.floatValue;
                        effectDuration.floatValue = 0f;
                    }
                    else if (effectDelay.floatValue + effectDuration.floatValue > duration.floatValue)
                    {
                        effectDuration.floatValue = duration.floatValue - effectDelay.floatValue;
                    }
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
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 139 + 17, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);

                    }
                }
                #endregion
                EditorGUI.indentLevel--;
                #endregion
                break;
            case MovementType.BEZIERCURVE:
                #region BezierCurve

                EditorGUI.indentLevel = 2;
                offsetX = position.x + (position.width / 3);

                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

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

                    if ((CameraEffectTypes) effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 139, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 139, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 122 + 17 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 122 + 17 + 17, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);

                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;
                    if (effectDelay.floatValue < 0f)
                        effectDelay.floatValue = 0f;

                    if (effectDelay.floatValue > duration.floatValue)
                    {
                        effectDelay.floatValue = duration.floatValue;
                        effectDuration.floatValue = 0f;
                    }
                    else if (effectDelay.floatValue + effectDuration.floatValue > duration.floatValue)
                    {
                        effectDuration.floatValue = duration.floatValue - effectDelay.floatValue;
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139 + 17 + 17, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139 + 17 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 191, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 191, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17 + 17, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);

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

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 122 + 17, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);

                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;
                    if (effectDelay.floatValue < 0f)
                        effectDelay.floatValue = 0f;

                    if (effectDelay.floatValue > duration.floatValue)
                    {
                        effectDelay.floatValue = duration.floatValue;
                        effectDuration.floatValue = 0f;
                    }
                    else if (effectDelay.floatValue + effectDuration.floatValue > duration.floatValue)
                    {
                        effectDuration.floatValue = duration.floatValue - effectDelay.floatValue;
                    }
                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 122 + 17 + 17, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 122 + 17 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 139 + 17 + 17, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 139 + 17 + 17, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 105 + 17 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 105 + 17 + 17, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);

                    }
                }
                #endregion
                EditorGUI.indentLevel--;
                #endregion
                break;
            case MovementType.LOOKCHAIN:
                #region LookChain
                int numFacings;
                //input for num facings
               // for (int i = 0; i <= facings.GetLength(0); i++)
                //{ 
                    //input for each facing as a Transform
               // }
                #endregion
                    break;
            case MovementType.WAIT:
                #region Wait
                EditorGUI.indentLevel = 2;
                offsetX = position.x + (position.width / 3);

                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                facingTypeLabelRect = new Rect(position.x, position.y + 71 - 34, position.width, 17f);
                EditorGUI.LabelField(facingTypeLabelRect, "Facing Type");

                facingTypeRect = new Rect(offsetX, position.y + 71 - 34, position.width / 2, 15f);
                EditorGUI.PropertyField(facingTypeRect, facingType, GUIContent.none);

                if ((FacingTypes)facingType.enumValueIndex == FacingTypes.FIXEDPOINT)
                {
                    #region Wait > FixedPoint
                    EditorGUI.indentLevel++;

                    Rect lookPointLabelRect = new Rect(position.x, position.y + 88 - 34, position.width, 17f);
                    EditorGUI.LabelField(lookPointLabelRect, "Look At Point");

                    Rect lookPointRect = new Rect(offsetX, position.y + 88 - 34, position.width / 2, 15f);
                    EditorGUI.PropertyField(lookPointRect, lookPoint, GUIContent.none);

                    EditorGUI.indentLevel--;

                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 105 - 34, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 105 - 34, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105 + 17 - 34, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105 + 17 - 34, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 139 - 34, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 139 - 34, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);
                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;

                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139 + 17 - 34, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139 + 17 - 34, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 191 - 17 - 34, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 191 - 17 - 34, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17 - 34, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17 - 34, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);
                        EditorGUI.indentLevel--;
                    }
                }
                #endregion
                else
                {
                    #region Wait > FreeLook
                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 88 - 34, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 88 - 34, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105 - 34, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105 - 34, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 122 - 34, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 122 - 34, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);

                    }
                    if (effectDuration.floatValue < 0f)
                        effectDuration.floatValue = 0f;
                    if (effectDelay.floatValue < 0f)
                        effectDelay.floatValue = 0f;

                    if (effectDelay.floatValue > duration.floatValue)
                    {
                        effectDelay.floatValue = duration.floatValue;
                        effectDuration.floatValue = 0f;
                    }
                    else if (effectDelay.floatValue + effectDuration.floatValue > duration.floatValue)
                    {
                        effectDuration.floatValue = duration.floatValue - effectDelay.floatValue;
                    }
                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 122 + 17 - 34, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 122 + 17 - 34, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 139 + 17 - 34, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 139 + 17 - 34, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeSpeed.floatValue);
                            if (fadeSpeed.floatValue < 0f)
                                fadeSpeed.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 105 + 17 - 34, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 105 + 17 - 34, position.width / 2, 17f);

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
                        EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);

                    }
                }
                #endregion
                EditorGUI.indentLevel--;
                #endregion
                break;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + extraHeight;
    }


}
