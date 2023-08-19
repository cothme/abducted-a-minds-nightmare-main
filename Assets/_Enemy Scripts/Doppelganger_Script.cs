using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class Doppelganger_Script : MonoBehaviour
{
    [SerializeField] AudioSource attack1Sound;
    [SerializeField] AudioSource attack2Sound;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject bullet;
    [SerializeField] PlayableDirector endingCutscene;
    float meter = 0;
    Animator anim;
    bool rangeAttacked,meleeAttacked;
    public float gunHeat;
    float shotCD;
    bool animationPlayed = false;
    bool shooting;
    bool walking = false;
    int attackChoice = 2;
    bool alerted = false;
    public float health = 50f;
    public float deathAnimTime;
    NavMeshAgent agent;
    Transform playerTransform;
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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(alerted == true)
        {
            agent.speed = 15;
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
            meter += Time.deltaTime;
            // StartCoroutine(ChasePlayerCoroutine());
            // alerted = true;
            // anim.SetBool("Alerted",alerted);
            // StartCoroutine(IdentifyAttackType());
            ChasePlayer();
        }
        else if(playerInAttackRange && playerInSightRange)
        {
            agent.SetDestination(playerTransform.position);
            agent.SetDestination(gameObject.transform.position);
            AttackPlayer();
        }
    }
    void Patrolling()
    {
        agent.speed = 10;
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
        Debug.Log(meter);
        if(meter <= 15f)
        {
            if(shotCD > 0)
            {
                shotCD -= Time.deltaTime;
                meter += Time.deltaTime;
            }
            agent.speed = 15;
            shooting = true;
            alerted = true;
            walking = false;
            anim.SetBool("Alerted",alerted);
            anim.SetBool("Walking",walking);
            anim.SetBool("Shooting",shooting);
            gameObject.transform.LookAt(playerTransform.transform.position);
            // agent.SetDestination(playerTransform.position);
            if(shotCD <= 0)
            {   
                shotCD = gunHeat;
                Instantiate(bullet,bulletSpawn.transform.position,gameObject.transform.rotation);
            }
        }
        else if(meter >= 15f)
        {
            shooting = false;
            anim.SetBool("Shooting",shooting);
            alerted = true;
            walking = true;
            anim.SetBool("Alerted",alerted);
            anim.SetBool("Walking",walking);
            agent.SetDestination(playerTransform.position);
        }
        if(meter >= 20f)
        {
            shooting = true;
            alerted = true;
            walking = false;
            anim.SetBool("Alerted",alerted);
            anim.SetBool("Walking",walking);
            anim.SetBool("Shooting",shooting);
            gameObject.transform.LookAt(playerTransform.transform.position);
            meter = 0;
        }
    }
    void AttackPlayer()
    {
        if(!attacked)
        {
            // if(shotCD > 0)
            //     {
            //         shotCD -= Time.deltaTime;
            //     }
            // int attackType = Random.Range(1,3);
            // attack animations and types here
            // if(attackType == 1)
            // { 
            //     anim.Play("Attack 1");
            //     attack1Sound.Play();
            //     attackDelay = 1.80f;
            // }
            // else 
            // { 
            //     anim.Play("Attack 2");
            //     attack2Sound.Play();
            //     attackDelay = 3.57f;
            // }
            //     if(attackType == 1)
            //     {
            //         anim.Play("Attack 1");
            //         attack1Sound.Play();
            //         attackDelay = 1.80f;
            //         agent.SetDestination(gameObject.transform.position);  
            //     }  
            //     else
            //     {
            //         gameObject.transform.LookAt(playerTransform.transform.position);
            //         if(shotCD <= 0)
            //         {   
            //             shotCD = gunHeat;
            //             Instantiate(bullet,bulletSpawn.transform.position,gameObject.transform.rotation);
            //         }
            //     }  
                    agent.SetDestination(gameObject.transform.position);
                    anim.Play("Attack 1");
                    attack1Sound.Play();
                    attackDelay = 1.80f;
                    agent.SetDestination(gameObject.transform.position);                
            attacked = true;
            Invoke(nameof(ResetAttack),attackDelay);
        }
    }
    void ResetAttack()
    {
        agent.SetDestination(gameObject.transform.position);
        alerted = false;
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
        agent.SetDestination(this.transform.position);
        anim.Play("Death");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(this.gameObject, deathAnimTime);
        endingCutscene.Play();
        PlayerState.Instance.LevelFourBossDefeated = true;
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
        }
            animationPlayed = true;
        yield return new WaitForSeconds(alertTime);
        ChasePlayer();
    }
    IEnumerator AttackPlayerRangeCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        Instantiate(bullet,bulletSpawn.transform.position,gameObject.transform.rotation);
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
