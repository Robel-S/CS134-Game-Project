using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;

    public Vector3 walkPoint;
    public Vector3 walkPoint2;
    bool walkPointReached;
    public float walkPointRange;
    public float speed;

    public float sightRange;
    public bool playerInSightRange;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        if (!playerInSightRange) {
            agent.enabled = false;
            Patrolling();
         }
        if (playerInSightRange)
        {
            agent.enabled = true;
            ChasePlayer();
        }
        Debug.Log(Physics.Raycast(transform.position + Vector3.up, Vector3.down, 5f, isGround));

    }

    private void Patrolling()
    {
        Vector3 target = walkPointReached ? walkPoint2 : walkPoint;

        //Ground check FIRST
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 5f, isGround))
        {
            // Move toward target
            Vector3 nextPos = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            //Snap to ground
            nextPos.y = hit.point.y;

            transform.position = nextPos;
        }

        // Handle rotation (Y axis only)
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        //Switch points
        if (Vector3.Distance(transform.position, target) < 0.5f)
            walkPointReached = !walkPointReached;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
