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
    private float extraHeight = 205f;

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
        SerializedProperty fadeTime = property.FindPropertyRelative("fadeTime");

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

                // Set indent level
                EditorGUI.indentLevel = 2;
                
                // Set the offset in the x direction for the different rectangles
                float offsetX = position.x + (position.width / 3);

                // Create label and input for waypoint duration
                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                duration.floatValue = EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

                // Make sure designer doesn't set duration to a negative number
                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                // Label and input for starting point
                Rect startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
                EditorGUI.LabelField(startingPointLabelRect, "Starting Point");

                Rect startingPointRect = new Rect(offsetX, position.y + 37, position.width / 2, 15f);
                EditorGUI.PropertyField(startingPointRect, point1, GUIContent.none);

                // Label and input for ending point
                Rect endingPointLabelRect = new Rect(position.x, position.y + 54, position.width, 17f);
                EditorGUI.LabelField(endingPointLabelRect, "Ending Point");

                Rect endingPointRect = new Rect(offsetX, position.y + 54, position.width / 2, 15f);
                EditorGUI.PropertyField(endingPointRect, point2, GUIContent.none);

                // Rectangle for the facing type
                facingTypeLabelRect = new Rect(position.x, position.y + 71, position.width, 17f);
                EditorGUI.LabelField(facingTypeLabelRect, "Facing Type");

                Rect facingTypeRect = new Rect(offsetX, position.y + 71, position.width / 2, 15f);
                EditorGUI.PropertyField(facingTypeRect, facingType, GUIContent.none);

                // If the facing type is fixed point
                // Main reason for making the drastic fixed point and free look completely separate is for the formatting
                if ((FacingTypes)facingType.enumValueIndex == FacingTypes.FIXEDPOINT)
                {
                    #region StraightLine > FixedPoint
                    // Increase the indent level
                    EditorGUI.indentLevel++;

                    // Rectangles for point the camera will be looking at
                    Rect lookPointLabelRect = new Rect(position.x, position.y + 88, position.width, 17f);
                    EditorGUI.LabelField(lookPointLabelRect, "Look At Point");

                    Rect lookPointRect = new Rect(offsetX, position.y + 88, position.width / 2, 15f);
                    EditorGUI.PropertyField(lookPointRect, lookPoint, GUIContent.none);

                    // Decrease the indent level
                    EditorGUI.indentLevel--;

                    // Effect type info
                    Rect effectTypeLabelRect = new Rect(position.x, position.y + 105, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect, "Camera Effect");

                    Rect effectTypeRect = new Rect(offsetX, position.y + 105, position.width / 2, 17f);
                    EditorGUI.PropertyField(effectTypeRect, effectType, GUIContent.none);

                    // If the camera effect isn't none, add an effect delay rectangle
                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        // Increase indent level
                        EditorGUI.indentLevel++;

                        // Effect delay rectangles
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 105 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 105 + 17, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    // If the camera effect isn't none or fade
                    if ((CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.NONE &&
                       (CameraEffectTypes)effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        // Effect duration rectangles
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 139, position.width, 17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 139, position.width / 2, 17f);
                        EditorGUI.PropertyField(effectDurationRect, effectDuration, GUIContent.none);
                    }

                    // Perform checks to make sure that the effect timings don't go longer than the waypoint length
                    // If they are, it shrinks the effect duration to the difference of the waypoint duration and the effect delay.
                    // If effect delay is greater than waypoint duration, the delay is set to the wp duration, and effect duration becomes 0.
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

                    // Camera effect Inspector stuff
                    // Camera shake
                    if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        // Creates a slider for the shake intensity
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    // Camera splatter
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        // Boolean option to fade the splatter
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        // If it's true
                        if (splatterFade.boolValue)
                        {
                            // Get the time for fading the splatter
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 191 - 17, position.width, 17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 191 - 17, position.width / 2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    // Camera fade
                    else if ((CameraEffectTypes)effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        // Rectangles for the label and speed of the fade out/in
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17, position.width / 2, 17f);

                        // Flip flop for label text
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

                        // Set the effect duration
                        effectDuration.floatValue = EditorGUI.FloatField(fadeOutSpeedRect, effectDuration.floatValue);
                        
                        // Decrement indent level
                        EditorGUI.indentLevel--;
                    }
                }
                    #endregion

                // The facing type is free look
                else
                {
                    #region StraightLine > FreeLook
                    // Everything in here is done the exact same as fixed point, just the y positions are different
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
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
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
            // Bezier curve movement type
            case MovementType.BEZIERCURVE:
                #region BezierCurve
                // Set indent level
                EditorGUI.indentLevel = 2;

                // Set the offset
                offsetX = position.x + (position.width / 3);

                // Waypoint stuff
                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                duration.floatValue = EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

                // waypoint duration check for negative
                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                // Starting point rectangles
                startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
                EditorGUI.LabelField(startingPointLabelRect, "Starting Point");

                startingPointRect = new Rect(offsetX, position.y + 37, position.width / 2, 15f);
                EditorGUI.PropertyField(startingPointRect, point1, GUIContent.none);

                // Ending point labels
                endingPointLabelRect = new Rect(position.x, position.y + 54, position.width, 17f);
                EditorGUI.LabelField(endingPointLabelRect, "Ending Point");

                endingPointRect = new Rect(offsetX, position.y + 54, position.width / 2, 15f);
                EditorGUI.PropertyField(endingPointRect, point2, GUIContent.none);

                // Control points label
                Rect controlPointLabelRect = new Rect(position.x, position.y + 71, position.width, 17f);
                EditorGUI.LabelField(controlPointLabelRect, "Control Point");

                Rect controlPointRect = new Rect(offsetX, position.y + 71, position.width / 2, 15f);
                EditorGUI.PropertyField(controlPointRect, point3, GUIContent.none);

                // Facing type info
                facingTypeLabelRect = new Rect(position.x, position.y + 71 + 17, position.width, 17f);
                EditorGUI.LabelField(facingTypeLabelRect, "Facing Type");

                facingTypeRect = new Rect(offsetX, position.y + 71 + 17, position.width / 2, 15f);
                EditorGUI.PropertyField(facingTypeRect, facingType, GUIContent.none);

                // Code done the exact same way as the straight line from here on, only difference is the y position
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
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
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
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
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
            // Look and return movement type
            case MovementType.LOOKANDRETURN:
                #region LookAndReturn
                // Set indent level
                EditorGUI.indentLevel = 2;

                // Set offset
                offsetX = position.x + (position.width / 3);

                    // Set the waypoint duration rectangles
                    waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                    EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                    waypointDurationRect = new Rect(offsetX, position.y + 20, position.width/2, 15f);
                    duration.floatValue = EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

                    // Make sure it can't go below zero
                    if (duration.floatValue < 0f)
                        duration.floatValue = 0f;

                    // Starting point rectangles
                    startingPointLabelRect = new Rect(position.x, position.y + 37, position.width, 17f);
                    EditorGUI.LabelField(startingPointLabelRect, "Starting Focus Point");

                    startingPointRect = new Rect(offsetX, position.y + 37, position.width/2, 15f);
                    EditorGUI.PropertyField(startingPointRect, point1, GUIContent.none);

                    // Time to reach target point rectangles
                    Rect timeToReachTargetLabelRect = new Rect(position.x, position.y + 54, position.width, 17f);
                    EditorGUI.LabelField(timeToReachTargetLabelRect, "Time To Look At Target Point");

                    Rect timeToTargetPointReachRect = new Rect(offsetX + 30, position.y + 54, (position.width / 2) - 30, 15f);
                    lookTime1.floatValue = EditorGUI.FloatField(timeToTargetPointReachRect, lookTime1.floatValue);

                    // Target point rectangles
                    Rect targetPointLabelRect = new Rect(position.x, position.y + 54 + 17, position.width, 17f);
                    EditorGUI.LabelField(targetPointLabelRect, "Target Focus Point");

                    Rect targetPointRect = new Rect(offsetX, position.y + 54 + 17, position.width/2, 15f);
                    EditorGUI.PropertyField(targetPointRect, point2, GUIContent.none);

                    // Time to stay at target point rectangles
                    Rect timeToStayTargetPointLabelRect = new Rect(position.x, position.y + 88, position.width/2, 17f);
                    EditorGUI.LabelField(timeToStayTargetPointLabelRect, "Time Looking At Target Point");

                    Rect targetPointTimeRect = new Rect(offsetX + 30, position.y + 88, (position.width / 2) - 30, 15f);
                    lookTime2.floatValue = EditorGUI.FloatField(targetPointTimeRect, lookTime2.floatValue);

                    // Time to look at final point rectangles
                    Rect timeToLookFinalPointLabelRect = new Rect(position.x, position.y + 105, position.width/2, 17f);
                    EditorGUI.LabelField(timeToLookFinalPointLabelRect, "Time To Look At Final Point");

                    Rect targetPointFinalTimeRect = new Rect(offsetX + 30, position.y + 105, (position.width/2) - 30, 15f);
                    lookTime3.floatValue = EditorGUI.FloatField(targetPointFinalTimeRect, lookTime3.floatValue);

                    // Ending point rectangles
                    endingPointLabelRect = new Rect(position.x, position.y + 71 + 51, position.width, 17f);
                    EditorGUI.LabelField(endingPointLabelRect, "Ending Focus Point");

                    endingPointRect = new Rect(offsetX, position.y + 71 + 51, position.width/2, 15f);
                    EditorGUI.PropertyField(endingPointRect, point3, GUIContent.none);

                    // There will be no facing type since this movement changes the rotation
                    // Everything is same as other drawers except for y position.

                    Rect effectTypeLabelRect2 = new Rect(position.x, position.y + 105 + 17 + 17, position.width, 17f);
                    EditorGUI.LabelField(effectTypeLabelRect2, "Camera Effect");

                    Rect effectTypeRect2 = new Rect(offsetX, position.y + 105 + 17 + 17, position.width/2, 17f);
                    EditorGUI.PropertyField(effectTypeRect2, effectType, GUIContent.none);

                    if ((CameraEffectTypes) effectType.enumValueIndex != CameraEffectTypes.NONE)
                    {
                        EditorGUI.indentLevel++;
                        Rect effectDelayLabelRect = new Rect(position.x, position.y + 139 + 17, position.width, 17f);
                        EditorGUI.LabelField(effectDelayLabelRect, "Effect Delay");

                        Rect effectDelayRect = new Rect(offsetX, position.y + 139 + 17, position.width/2, 17f);
                        EditorGUI.PropertyField(effectDelayRect, effectDelay, GUIContent.none);
                    }

                    if ((CameraEffectTypes) effectType.enumValueIndex != CameraEffectTypes.NONE &&
                        (CameraEffectTypes) effectType.enumValueIndex != CameraEffectTypes.FADE)
                    {
                        Rect effectDurationLabelRect = new Rect(position.x, position.y + 122 + 17 + 17 + 17, position.width,
                            17f);
                        EditorGUI.LabelField(effectDurationLabelRect, "Effect Duration");

                        Rect effectDurationRect = new Rect(offsetX, position.y + 122 + 17 + 17 + 17, position.width/2, 17f);
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

                    if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.SHAKE)
                    {
                        Rect shakeIntensitySliderRect = new Rect(position.x, position.y + 139 + 17 + 17 + 17, position.width,
                            17f);
                        EditorGUI.Slider(shakeIntensitySliderRect, shakeIntensity, 0f, 100f);
                        EditorGUI.indentLevel--;
                    }
                    else if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.SPLATTER)
                    {
                        Rect fadeSplatterRect = new Rect(position.x, position.y + 139 + 17 + 17 + 17, position.width, 17f);
                        EditorGUI.PropertyField(fadeSplatterRect, splatterFade);

                        if (splatterFade.boolValue)
                        {
                            EditorGUI.indentLevel++;
                            Rect fadeSplatterDurationLabelRect = new Rect(position.x, position.y + 191 + 17, position.width,
                                17f);
                            EditorGUI.LabelField(fadeSplatterDurationLabelRect, "Splatter Fade Time");

                            Rect fadeSplatterDurationRect = new Rect(offsetX, position.y + 191 + 17, position.width/2, 17f);
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
                            EditorGUI.indentLevel--;
                        }
                    }
                    else if ((CameraEffectTypes) effectType.enumValueIndex == CameraEffectTypes.FADE)
                    {
                        Rect fadedOutLabelRect = new Rect(position.x, position.y + 122 + 17 + 17 + 17, position.width, 17f);
                        Rect fadeOutSpeedRect = new Rect(offsetX, position.y + 122 + 17 + 17 + 17, position.width/2, 17f);

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

                    #endregion
                break;
            // Wait movement type
            case MovementType.WAIT:
                #region Wait
                EditorGUI.indentLevel = 2;
                offsetX = position.x + (position.width / 3);

                // Waypoint duration time rectangles
                waypointDurationLabelRect = new Rect(position.x, position.y + 20, position.width, 17f);
                EditorGUI.LabelField(waypointDurationLabelRect, "Waypoint Duration");

                waypointDurationRect = new Rect(offsetX, position.y + 20, position.width / 2, 15f);
                duration.floatValue = EditorGUI.FloatField(waypointDurationRect, duration.floatValue);

                // Make sure designer can't go negative
                if (duration.floatValue < 0f)
                    duration.floatValue = 0f;

                // Facing type and camera effect type is all same as other drawers
                // Only the y position is different

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
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
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
                            EditorGUI.FloatField(fadeSplatterDurationRect, fadeTime.floatValue);
                            if (fadeTime.floatValue < 0f)
                                fadeTime.floatValue = 0f;
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

        // Ends the property
        EditorGUI.EndProperty();
    }

    // Increases the amount of space between the array elements
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + extraHeight;
    }


}
