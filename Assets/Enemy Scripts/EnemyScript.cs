using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float health;
    public NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask ground, player;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float attackDelay;
    bool attacked;
    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange,isFacingObstacle;
    RaycastHit hit;
    Vector3 direction;
    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Debug.Log(isFacingObstacle);
        CheckObstacle();
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        if (!playerInAttackRange && !playerInSightRange)
        {
            Patrolling();
        } 
        else if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void CheckObstacle()
    {
        direction = Vector3.forward;
        Ray ray = new Ray(transform.position, transform.TransformDirection(direction * attackRange));
        if(Physics.Raycast(ray, out hit,attackRange))
        {
            if(hit.collider.tag == "Wall")
            {
                isFacingObstacle = true;
            }
        }
        else
        {
            isFacingObstacle = false;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * attackRange));
    }
    void Patrolling()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(transform.position, walkPointRange, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange,walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange,walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint,-transform.up,ground))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(playerTransform);
         
        if(!attacked)
        {
            //attack animations and types here

            attacked = true;
            Invoke(nameof(ResetAttack),attackDelay);
        }
    }
    void ResetAttack()
    {
        attacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }
}
