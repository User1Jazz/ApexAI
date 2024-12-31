using UnityEngine;
using System.Reflection;

[System.Serializable]
public class StateTransition
{

    [Tooltip("The state to transition to.")]
    public AIState toState; // Target state

    [Tooltip("Selected method from the target AIState.")]
    public string selectedMethod;

    public bool EvaluateTransition(GameObject owner)
    {
        Debug.Log("EvaluateTransition called");
        if (toState != null && !string.IsNullOrEmpty(selectedMethod))
        {
            // Use reflection to find and invoke the selected method
            MethodInfo method = toState.GetType().GetMethod(selectedMethod, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                // Invoke the method
                object[] args = new object[] { owner };
                object returnValue = method.Invoke(toState, args); // Invoke with no parameters

                // Ensure the return value is a boolean
                if (returnValue is bool result)
                {
                    return result; // Return the boolean value
                }
                else
                {
                    Debug.LogError($"Method {selectedMethod} did not return a boolean value.");
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Method {selectedMethod} not found on {toState.name}");
                return false;
            }
        }
        else
        {
            Debug.LogError("AIState or method not assigned.");
            return false;
        }
    }
}