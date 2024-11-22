using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 5f;
    public float walkRadius = 15f;
    public float attackCoolDown = 2f;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;
    private Vector3 walkTarget;
    private bool isWalking;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.Log("No Player");
        }

        SetNewWalkTarget();
    }

    
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);

        }
        else
        {
            if (!isWalking || Vector3.Distance(transform.position,walkTarget)< 1f)
            {
                SetNewWalkTarget();
            }
        }
    }

    void SetNewWalkTarget()
    {
        //Random direction
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
        {
            walkTarget = hit.position;
            agent.SetDestination(walkTarget);
            isWalking = true;
        }
    }

    void AttackPlayer()
    {
        if (Time.time > lastAttackTime + attackCoolDown)
        {
            Debug.Log("Enemy Attacks");
            lastAttackTime = Time.time;
        }
    }

   
}
