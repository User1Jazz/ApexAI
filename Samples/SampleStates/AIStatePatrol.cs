using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName="new patrol state", menuName="ApexAI/Built in/Patrol State", order = 1)]
public class AIStatePatrol : AIState
{
    private AgentPrefs agentPrefs;
    private Transform patrolPoint;

    public override void OnEnter(GameObject owner)
    {
        Debug.Log($"{owner.name} entered Patrol State.");

        // Prepare agent for patrol behaviour
        agentPrefs = owner.GetComponent<AgentPrefs>();              // Get npc preferences component
        patrolPoint = agentPrefs.GetNextPatrolPoint();              // Get patrol point
        agentPrefs.navMeshAgent.destination = patrolPoint.position; // Apply patrol point
    }

    public override void OnExit(GameObject owner)
    {
        Debug.Log($"{owner.name} exited Patrol State.");
    }
    
    public override void OnUpdate(GameObject owner)
    {
        Debug.Log($"{owner.name} Called patrol state OnUpdate()");
        
        // Walk to one of the patrol points
        if(agentPrefs.navMeshAgent.remainingDistance >= agentPrefs.navMeshAgent.stoppingDistance)
        {
            Debug.Log($"{owner.name} Moving agent...");
            //agentPrefs.navMeshAgent.Move(agentPrefs.navMeshAgent.velocity);
        }
        else
        {
            // request another patrol point
            Debug.Log($"{owner.name}: Requesting new patrol point");
            patrolPoint = agentPrefs.GetNextPatrolPoint();
            agentPrefs.navMeshAgent.destination = patrolPoint.position;
        }
    }

    [TransitionFunction]
    public bool CheckPatrolCondition(GameObject owner){
        AgentPrefs aPrefs = owner.GetComponent<AgentPrefs>();
        if(Vector3.Distance(owner.transform.position, aPrefs.target.position) > aPrefs.detectionRange)
        {
            Debug.Log($"{owner.name}: Patrol State condition met.");
            return true;
        }
        return false;
    }
}
