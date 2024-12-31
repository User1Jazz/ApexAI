using UnityEngine;
using System.Collections.Generic;

public abstract class AIState : ScriptableObject {
    public List<StateTransition> transitions;
    public string stateName;

    public virtual void OnEnter(GameObject owner) {}
    public virtual void OnExit(GameObject owner) {}
    public abstract void OnUpdate(GameObject owner);

    public AIState CheckTransitions(GameObject owner) {
        foreach (StateTransition transition in transitions) {
            if (transition.EvaluateTransition(owner)) {
                return transition.toState;
            }
        }
        return null;
    }
}
