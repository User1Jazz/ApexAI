using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="new idle state", menuName="ApexAI/Built in/Idle State", order = 1)]
public class AIStateIdle : AIState
{
    public override void OnEnter(GameObject owner)
    {
        Debug.Log($"{owner.name} entered Idle State.");
    }

    public override void OnExit(GameObject owner)
    {
        Debug.Log($"{owner.name} exited Idle State.");
    }
    
    public override void OnUpdate(GameObject owner)
    {
        // Staying idle, no change...
    }

    [TransitionFunction]
    public bool ConditionMet(GameObject owner){
        Debug.Log("ConditionMet called! :D");
        return false;
    }

    [TransitionFunction]
    public bool AnotherConditionMet(GameObject owner){
        Debug.Log("AnotherConditionMet called! :D");
        return false;
    }
}
