using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AgentPrefs : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public List<Transform> patrolPoints;
    public float moveSpeed = 1f;
    public float detectionRange = 5.0f;
    public float attackRange = 2.0f;
    public Transform target;

    public void Start()
    {

    }

    public void Update()
    {

    }

    public Transform GetNextPatrolPoint()
    {
        return patrolPoints[Random.Range(0, patrolPoints.Count-1)];
    }
}