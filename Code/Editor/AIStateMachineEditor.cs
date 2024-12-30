using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AIStateMachine))]
public class AIStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AIStateMachine stateMachine = (AIStateMachine)target;

        // Title
        EditorGUILayout.LabelField("AI State Machine", EditorStyles.boldLabel);

        // Display the current state
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Current State:", stateMachine.GetCurrentStateName());
        }
        else
        {
            EditorGUILayout.LabelField("Current State:", "Not Running");
        }

        // Starting State
        stateMachine.startingState = (AIState)EditorGUILayout.ObjectField("Starting State",
            stateMachine.startingState, typeof(AIState), false);

        // Transition List
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Available States", EditorStyles.boldLabel);

        if (stateMachine.startingState != null)
        {
            List<AIState> allStates = stateMachine.GetAllStates();
            if (allStates != null)
            {
                foreach (var state in allStates)
                {
                    EditorGUILayout.ObjectField("State", state, typeof(AIState), false);
                }
            }
        }

        // Save changes
        if (GUI.changed)
        {
            EditorUtility.SetDirty(stateMachine);
        }
    }
}
