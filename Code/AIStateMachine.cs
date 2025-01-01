using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    public AIState startingState; // Initial state (set via Inspector)
    private AIState currentState;

    void Start()
    {
        if (startingState != null)
        {
            currentState = startingState;
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(gameObject);
            ChangeState(currentState.CheckTransitions(gameObject));
        }
    }

    public string GetCurrentStateName()
    {
        if(currentState != null)
        {
            return currentState.stateName;
        }else
        {
            return "";
        }
    }

    public List<AIState> GetAllStates()
    {
        if (currentState != null)
        {
            List<AIState> allStates = new List<AIState>();
            foreach (StateTransition transition in currentState.transitions)
            {
                allStates.Add(transition.toState);
            }
            return allStates;
        }
        return null;
    }

    public void ChangeState(AIState newState)
    {
        if (currentState != null && newState != null)
        {
            Debug.Log("Changing State...");
            currentState.OnExit(gameObject);
            currentState = newState;
            currentState.OnEnter(gameObject);
        }
    }
}
