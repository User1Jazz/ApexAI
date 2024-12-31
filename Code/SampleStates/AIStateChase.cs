using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="new chase state", menuName="ApexAI/Built in/Chase State", order = 1)]
public class AIStateChase : AIState
{
    public override void OnEnter(GameObject owner) {}
    public override void OnExit(GameObject owner) {}
    public override void OnUpdate(GameObject owner)
    {

    }

    [TransitionFunction]
    public bool ConditionMet(GameObject owner){
        return false;
    }

    [TransitionFunction]
    public bool AnotherConditionMet(GameObject owner){
        return false;
    }
}
