using UnityEngine;
using System.Reflection;

[System.Serializable]
public class StateTransition {
    
    [Tooltip("The state to transition to.")]
    public AIState toState; // Target state

    [Tooltip("Selected method from the target AIState.")]
    public string selectedMethod;

    public void EvaluateTransition()
    {
        if (toState != null && !string.IsNullOrEmpty(selectedMethod))
        {
            // Use reflection to find and invoke the selected method
            MethodInfo method = toState.GetType().GetMethod(selectedMethod, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                object returnValue = method.Invoke(toState, null); // Invoke with no parameters
                Debug.Log($"Invoked method: {selectedMethod}. Return value: {returnValue}");
            }
            else
            {
                Debug.LogError($"Method {selectedMethod} not found on {toState.name}");
            }
        }
        else
        {
            Debug.LogError("AIState or method not assigned.");
        }
    }
}