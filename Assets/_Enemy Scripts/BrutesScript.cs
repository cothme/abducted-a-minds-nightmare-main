using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class BrutesScript : MonoBehaviour
{
    [SerializeField] AudioSource brutesAlert;
    [SerializeField] AudioSource brutesAttack1;
    [SerializeField] AudioSource brutesAttack2;
    [SerializeField] AudioSource brutesAttack3;
    [SerializeField] AudioSource brutesWalk;
    [SerializeField] AudioSource brutesHit;

    Animator anim;
    bool animationPlayed = false;
    bool walking = false;
    int attackType;
    public float health = 50f;
    public float deathAnimTime;
    NavMeshAgent agent;
    public Transform playerTransform;
    public LayerMask ground, player;
    public Vector3 walkPoint;
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
        if(knocked)
        {
            agent.SetDestination(this.gameObject.transform.position);
        }
        if(attacked)
        {
            agent.SetDestination(this.gameObject.transform.position);
        }
        if(transform.position != Vector3.zero && !attacked)
        {
            //brutesWalk.Play();
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
        } 
        else if(playerInSightRange && !playerInAttackRange)
        {
            StartCoroutine(ChasePlayerCoroutine());
        }
        else if(playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
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
        anim.SetBool("Alerted",false);
        agent.SetDestination(playerTransform.position);
    }
    void AttackPlayer()
    {
        if(!attacked)
        {
            //attack animations and types here
            attackType = UnityEngine.Random.Range(1,4);
            switch(attackType)
            {
                case 1:
                anim.Play("Attack 1");
                    brutesAttack1.Play();
                    attackDelay = 4.10f;
                break;
                case 2:
                anim.Play("Attack 2");
                    brutesAttack2.Play();
                    attackDelay = 1.70f;
                break;
                case 3:
                anim.Play("Attack 3");
                    brutesAttack3.Play();
                    attackDelay = 3.70f;
                break;
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
        agent.SetDestination(gameObject.transform.position);
        StartCoroutine(BrutesDeathCoroutine());
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
            agent.SetDestination(transform.position);
            anim.Play("Alerted");
            // brutesAlert.Play();
        }
            animationPlayed = true;
        yield return new WaitForSeconds(alertTime);
        ChasePlayer();
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Bullet")
        {
            health -= GunManager.Instance.Damage;
            TakeDamage();
            //anim.Play("Hit");
            //brutesHit.Play();
        }
        if(ctr >= 5)
        {
            knocked = true;
            anim.Play("Hit");
            agent.SetDestination(this.transform.position);
        }
        else
        {
            knocked = false;
        }
    }
    IEnumerator BrutesDeathCoroutine()
    {   
        anim.Play("Death");
        yield return new WaitForSeconds(deathAnimTime);
        Destroy(gameObject);
        PlayerState.Instance.LevelOneBossDefeated = true;
        levelOneCutscene.Play();
        Cursor.lockState = CursorLockMode.None;
    }
}
