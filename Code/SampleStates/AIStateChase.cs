using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName="new chase state", menuName="ApexAI/Built in/Chase State", order = 1)]
public class AIStateChase : AIState
{
    private AgentPrefs agentPrefs;

    public override void OnEnter(GameObject owner)
    {
        Debug.Log($"{owner.name} entered Idle State.");
        
        // Prepare agent for patrol behaviour
        agentPrefs = owner.GetComponent<AgentPrefs>(); // Get npc preferences component
    }

    public override void OnExit(GameObject owner)
    {
        Debug.Log($"{owner.name} exited Chase State.");
    }
    
    public override void OnUpdate(GameObject owner)
    {
        // Chase target
        agentPrefs.navMeshAgent.destination = agentPrefs.target.position;
    }

    [TransitionFunction]
    public bool PlayerNearby(GameObject owner){
        AgentPrefs aPrefs = owner.GetComponent<AgentPrefs>();
        if(Vector3.Distance(owner.transform.position, aPrefs.target.position) <= aPrefs.detectionRange)
        {
            Debug.Log($"{owner.name}: Chase State condition met.");
            return true;
        }
        return false;
    }
}
