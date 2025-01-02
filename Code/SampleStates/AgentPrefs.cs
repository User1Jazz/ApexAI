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
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public Transform projectilesGroup;
    public float fireCooldown = 3f;
    private bool hasFired = false;

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

    public void FireProjectile()
    {
        if(!hasFired)
        {
            if(projectile != null && projectileSpawnPoint != null && projectilesGroup != null)
            {
                Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation, projectilesGroup);
                StartCoroutine(FireCooldown());
            }
            else
            {
                Debug.LogError("Projectile, Projectile Spawn Point, and/or Projectiles Group not assigned!");
            }
        }
    }

    System.Collections.IEnumerator FireCooldown()
    {
        hasFired = true;
        yield return new WaitForSeconds(fireCooldown);
        hasFired = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}