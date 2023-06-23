using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnersScript : MonoBehaviour
{
    [SerializeField] AudioSource attack1Sound;
    [SerializeField] AudioSource attack2Sound;
    [SerializeField] AudioSource alertSound;
    Animator anim;
    bool animationPlayed = false;
    bool walking = false;
    int attackType;
    bool alerted = false;
    public float health = 50f;
    public float deathAnimTime;
    NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask ground, player;
    bool walkPointSet;
    public float walkPointRange;
    public float attackDelay;
    public float alertTime;
    bool attacked;
    public float sightRange, attackRange;
    public bool playerInSightRange,playerInAttackRange,isFacingObstacle;
    RaycastHit hit;
    Vector3 direction;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(alerted == true)
        {
            agent.speed = 10;
        }
        else
        {
            agent.speed = 5;
        }
        if(attacked)
        {
            agent.SetDestination(gameObject.transform.position);
        }
        if(transform.position != Vector3.zero && !attacked)
        {
            walking = true;
            anim.SetBool("Walking",walking);
        }
        else
        {
            walking = false;
            anim.SetBool("Walking",walking);
        }
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        if (!playerInAttackRange && !playerInSightRange)
        {
            Patrolling();
            alerted = false;
            anim.SetBool("Alerted",alerted);
        } 
        else if(playerInSightRange && !playerInAttackRange)
        {
            StartCoroutine(ChasePlayerCoroutine());
            alerted = true;
            anim.SetBool("Alerted",alerted);
        }
        else if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }
    void Patrolling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(this.transform.position, walkPointRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        { 
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    void ChasePlayer()
    {
        alerted = true;
        anim.SetBool("Alerted",alerted);
        agent.SetDestination(playerTransform.position);
    }
    void AttackPlayer()
    {
        if(!attacked)
        {
            //attack animations and types here
            attackType = UnityEngine.Random.Range(1,3);
            if(attackType == 1)
            { 
                anim.Play("Attack 1");
                AudioManager.Instance.PlaySound(alertSound, "Runners Attack 1");
                attackDelay = 1.80f;
            }
            else 
            { 
                anim.Play("Attack 2");
                AudioManager.Instance.PlaySound(alertSound, "Runners Attack 2");
                attackDelay = 3.57f;
            }
            attacked = true;
            Invoke(nameof(ResetAttack),attackDelay);
        }
    }
    void ResetAttack()
    {
        attacked = false;
    }
    public void TakeDamage()
    {
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        agent.SetDestination(transform.position);
        anim.Play("Death");
        Destroy(this.gameObject, deathAnimTime);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }
    IEnumerator ChasePlayerCoroutine()
    {   
        if(!animationPlayed)
        {
            agent.SetDestination(this.gameObject.transform.position);
            anim.Play("Alerted");
            AudioManager.Instance.PlaySound(alertSound,"Runners Alert");
        }
            animationPlayed = true;
        yield return new WaitForSeconds(alertTime);
        ChasePlayer();
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Bullet")
        {
            anim.Play("Hit 1");
            health -= GunManager.Instance.Damage;
            TakeDamage();
            agent.SetDestination(playerTransform.position);
            alerted = true;
        }
    }
}
