using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public ParticleSystem deathExpl;
    public NavMeshAgent navMesh;
    public Transform player;
    public LayerMask isGround, isPlayer;

    // Patrolling
    public Transform[] patrolPoints = new Transform[2];
    bool walkPointSet;
    private int index;
    [SerializeField]
    private float distToPoint = 10.0f;

    // Attacking
    public float timeBetweenAttacks;
    public float force, upwardForce;
    public GameObject projectile;
    public Transform firePoint;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool inSightRange, inAttackRange;

    public bool allowInvoke = true;

    private void Awake()
    {
        // Set player and Nav mesh objects
        player = GameObject.Find("Player").transform;
        navMesh = this.gameObject.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        index = 1;
        Patrolling();
    }

    private void Update()
    {
        // Set sight and attack range to trigger
        inSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if(navMesh != null)
        {
            // Check if the player is within range and change state accordingly
            if (!inSightRange && !inAttackRange)
            {
                if (navMesh.remainingDistance <= distToPoint)
                {
                    Debug.Log("Reached target");
                    if (index >= patrolPoints.Length - 1)
                        index = 0;
                    else
                        index += 1;

                    Patrolling();
                }
            }
            if (inSightRange && !inAttackRange) ChasePlayer();
            if (inSightRange && inAttackRange) Attack();
        }
    }

    private void Patrolling()
    {
        Debug.Log("Patrolling");
        Debug.Log("Index = " + index);
        if(patrolPoints.Length > 0)
            navMesh.SetDestination(patrolPoints[index].position);
    }

    private void IncreaseIndex()
    {
        Debug.Log("Indexing");
        if (index >= patrolPoints.Length - 1)
            index = 0;
        else
            index += 1;

        Patrolling();
    }

    private void ChasePlayer()
    {
        // Move to player
        navMesh.SetDestination(player.position);
    }

    private void Attack()
    {
        // Stop Moving
        navMesh.SetDestination(transform.position);
        transform.LookAt(player);

        // Check if Attacked
        if(!alreadyAttacked)
        {
            /// Shoot At Player
            Rigidbody rb = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * force, ForceMode.Impulse);
            rb.AddForce(transform.up * upwardForce, ForceMode.Impulse);
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void Hit()
    {
        deathExpl.Play();
        Debug.Log("Destroyed " + this.gameObject.name);
        Destroy(this.gameObject);
    }
}
