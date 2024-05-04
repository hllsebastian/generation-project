using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("Reference")]
    private Animator anim;
    private GameObject target;
    private NavMeshAgent agent;

    [Header("Movement")]
    private Vector3 destinationPoint;
    private bool destinationPointSet;
    [SerializeField] private float destinationPointRange;

    [Header("States")]
    public float attackRange;
    public float visionRange;
    public Transform offset;
    private bool  playerInAttackRange, playerInVisionRange;
    [SerializeField] private bool isAttack;
    [SerializeField] private float attackDelay;

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask obstacleLayer;

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks = 3f;
    [SerializeField] private float timeToHit = 0.8f;
    [SerializeField] private float timeDamage = 0.3f;
    [SerializeField] public int health;
    [SerializeField] private int attackDamage;
    bool hasHit = false;
    private bool isDamagable = true;
    private bool _isAlive = true;
    protected bool isAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            anim.SetBool("isAlive", value);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRange = Physics.CheckSphere(offset.position, visionRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInAttackRange && !playerInVisionRange) EnemyPatroling(); //Patroling while there is no player in range
        if (!playerInAttackRange && playerInVisionRange) EnemyChasing(); //Chase if the player is in vision range
        if (playerInAttackRange && playerInVisionRange) EnemyAttack(); //Attack when the player is in attack range
        if(!playerInAttackRange && !playerInVisionRange) EnemyPatroling(); //Patroling while there is no player in range
        if(!playerInAttackRange && playerInVisionRange) EnemyChasing(); //Chase if the player is in vision range
        if(playerInAttackRange && playerInVisionRange) 
        {
            agent.velocity = Vector3.zero;
            EnemyAttack(); //Attack when the player is in attack range
        }


    }
    private void EnemyPatroling()
    {
        //Search a new destination point
        if (!destinationPointSet) SearchDestinationPoint();
        //If the destination point exist, move towards that point whit nav mesh
        if (destinationPointSet)
        {
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
            agent.SetDestination(destinationPoint);
        }

        //Reach Destination Point
        if (Vector3.Distance(transform.position, destinationPoint) < 1f)
        {
            anim.SetBool("walk", false);
            destinationPointSet = false;
        }
    }
    private void SearchDestinationPoint()
    {
        float destinationZ = Random.Range(-destinationPointRange, destinationPointRange);
        float destinationX = Random.Range(-destinationPointRange, destinationPointRange);

        destinationPoint = new Vector3(transform.position.x + destinationX, transform.position.y, transform.position.z + destinationZ);

        if(Physics.Raycast(destinationPoint, -transform.up, 2f, groundLayer) && !Physics.Raycast(destinationPoint, -transform.up, 2f, obstacleLayer))
            destinationPointSet = true;
        else
            destinationPointSet = false;
    }
    private void EnemyChasing()
    {
        //when chasing start, erase the last destination point
        destinationPointSet = false;
        anim.SetBool("run", true);
        agent.SetDestination(target.transform.position);
    }

    private void EnemyAttack()
    {
        //agent.SetDestination(target.transform.position);
        //transform.LookAt(target.transform.position);
        if (Vector3.Distance(transform.position, target.transform.position) < 1.5f)

       // Vector3 dirRotate = new Vector3(target.transform.position.x - transform.position.x, transform.position.y, target.transform.position.z - transform.position.z);
      //  transform.rotation = Quaternion.LookRotation(dirRotate);
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange + 1.5f)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }

        if (!isAttack)
        {
            // Attack Patron
            anim.SetTrigger("isAttacking");

            isAttack = true;
            Invoke(nameof(ResetAttack), attackDelay);
            Invoke(nameof(DealDamage), timeToHit);

        }
    }

    private void DealDamage()
    {
        StartCoroutine(activateHitBox());
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el componente PlayerController
            PlayerController playerController = other.GetComponent<PlayerController>();
            
            // Si encontramos el componente PlayerController, le enviamos el daño
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }
        }
    }

    IEnumerator activateHitBox()
    {
        GameObject[] enemyHands = GameObject.FindGameObjectsWithTag("EnemyHand");
        foreach (GameObject enemyHand in enemyHands)
        {
            BoxCollider boxCollider = enemyHand.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                yield return new WaitForSeconds(1.0f);
                boxCollider.enabled = false;
            }
            else
            {
                Debug.LogWarning("El objeto con tag 'EnemyHand' no tiene un BoxCollider adjunto.");
            }
        }
    }

    #region TakeDamage
    private void ResetDamagable()
    {
        isDamagable = true;
    }

    public void TakeDamage(int damage)
    {
        if (isDamagable)
        {
            health -= damage;
            anim.SetTrigger("isHit");
            isDamagable = false;
            Invoke(nameof(ResetDamagable), 1f);
        }

        if (health <= 0) StartCoroutine(onDeath());
    }

    IEnumerator onDeath()
    {
        isAlive = false;
        isDamagable = false;
        yield return new WaitForSeconds(5f);
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void ResetAttack()
    {
        anim.ResetTrigger("attack");
        isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(offset.position, visionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(destinationPoint, 0.5f);
    }
}
#endregion
