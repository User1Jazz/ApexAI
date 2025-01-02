using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;

[CustomPropertyDrawer(typeof(StateTransition))]
public class StateTransitionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Calculate the heights for each field
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        // "To State" field
        SerializedProperty toStateProp = property.FindPropertyRelative("toState");
        Rect toStateRect = new Rect(position.x, position.y, position.width, lineHeight);
        EditorGUI.PropertyField(toStateRect, toStateProp);

        // Check if "To State" is assigned
        AIState toState = toStateProp.objectReferenceValue as AIState;
        if (toState != null)
        {
            // Get the "selectedMethod" property
            SerializedProperty selectedMethodProp = property.FindPropertyRelative("selectedMethod");

            // Get available methods in the assigned AIState
            var methods = toState.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(TransitionFunctionAttribute), false).Any())
                .Select(m => m.Name)
                .ToArray();

            // Dropdown for selecting a method
            int selectedIndex = System.Array.IndexOf(methods, selectedMethodProp.stringValue);
            if (selectedIndex < 0) selectedIndex = 0; // Default to first method if none selected

            Rect methodRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);
            selectedIndex = EditorGUI.Popup(methodRect, "Selected Method", selectedIndex, methods);

            // Update the selected method
            if (methods.Length > 0)
            {
                selectedMethodProp.stringValue = methods[selectedIndex];
            }
            else
            {
                selectedMethodProp.stringValue = null;
                EditorGUI.HelpBox(methodRect, "No methods available", MessageType.Info);
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        SerializedProperty toStateProp = property.FindPropertyRelative("toState");
        AIState toState = toStateProp.objectReferenceValue as AIState;

        // Height includes the "To State" field and optionally the method dropdown
        return toState != null ? lineHeight * 2 + spacing : lineHeight;
    }
}
