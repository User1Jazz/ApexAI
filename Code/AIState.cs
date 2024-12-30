using UnityEngine;

public abstract class AIState : ScriptableObject {
    public StateTransition[] transitions;
    public string stateName;

    public virtual void OnEnter(GameObject owner) {}
    public virtual void OnExit(GameObject owner) {}
    public abstract void OnUpdate(GameObject owner);

    public AIState CheckTransitions(GameObject owner) {
        foreach (var transition in transitions) {
            if (ConditionMet(owner)) {
                return transition.toState;
            }
        }
        return null;
    }

    [TransitionFunction]
    public virtual bool ConditionMet(GameObject owner) {
        // Add logic for various conditions, e.g.:
        /*if (condition == "playerNearby") {
            // Check proximity to player
            PlayerController player = FindObjectOfType<PlayerController>();
            return Vector3.Distance(owner.transform.position, player.transform.position) < 10f;
        }*/
        // Add more conditions as needed
        return false;
    }
}
