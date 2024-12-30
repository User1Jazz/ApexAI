using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="new idle state", menuName="ApexAI/Built in/Idle State", order = 1)]
public class AIStateIdle : AIState
{
    public override void OnEnter(GameObject owner) {}
    public override void OnExit(GameObject owner) {}
    public override void OnUpdate(GameObject owner)
    {

    }

    [TransitionFunction]
    public override bool ConditionMet(GameObject owner){
        return false;
    }

    [TransitionFunction]
    public bool AnotherConditionMet(GameObject owner){
        return false;
    }
}
