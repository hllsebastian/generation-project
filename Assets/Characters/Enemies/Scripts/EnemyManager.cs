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

        Vector3 dirRotate = new Vector3(target.transform.position.x - transform.position.x, transform.position.y, target.transform.position.z - transform.position.z);
        transform.rotation = Quaternion.LookRotation(dirRotate);
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange + 1.5f)
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }      

        if (!isAttack)
        {
            // Attack Patron
            anim.SetTrigger("attack");

            isAttack = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }
    
    private void ResetAttack()
    {
        anim.ResetTrigger("attack");
        isAttack= false;
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
