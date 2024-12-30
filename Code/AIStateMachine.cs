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
            ChangeState(startingState);
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(gameObject);
        }
    }

    public string GetCurrentStateName()
    {
        return currentState.stateName;
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
        if (currentState != null)
        {
            currentState.OnExit(gameObject);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(gameObject);
        }
    }
}
