using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName="new attack state", menuName="ApexAI/Built in/Attack State", order = 1)]
public class AIStateAttack : AIState
{
    private AgentPrefs agentPrefs;

    public override void OnEnter(GameObject owner)
    {
        Debug.Log($"{owner.name} entered Attack State.");

        // Prepare agent for attack behaviour
        agentPrefs = owner.GetComponent<AgentPrefs>(); // Get npc preferences component
    }

    public override void OnExit(GameObject owner)
    {
        Debug.Log($"{owner.name} exited Attack State.");
    }

    public override void OnUpdate(GameObject owner)
    {
        // Stop at current position
        agentPrefs.navMeshAgent.destination = owner.transform.position;
        owner.transform.LookAt(agentPrefs.target.position);
        // Attack target
        Debug.Log($"{owner.name} performed an attack!");
        agentPrefs.FireProjectile();
    }

    [TransitionFunction]
    public bool CheckAttackCondition(GameObject owner){
        // Check distance between agent and target objects
        if(Vector3.Distance(owner.transform.position, owner.GetComponent<AgentPrefs>().target.position) <= owner.GetComponent<AgentPrefs>().attackRange)
        {
            return true;
        }
        return false;
    }
}
