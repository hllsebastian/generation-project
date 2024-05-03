using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngineInternal;

public class AnubisController : MonoBehaviour
{
    [Header("Attributes")]
    private int routine; 
    private float timer;
    public float timeRoutine; //time beetween attacks
    private Animator anim;
    private NavMeshAgent agent;
    private Quaternion angle;
    private float grade;
    private GameObject target;
    public bool isAttacking;
    public float speed;
    public GameObject[] hit;
    //public int hitSelect;
    public AnubisRange range;

    [Header("States")]
    public int phase = 1;
    public float hpMin, hpMax;
    public Image healthBar;
    //public AudioSource music;
    public bool onDead;

    //Skill Attack
    public float jumpDistance;
    private bool directionSkill;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        agent.speed = speed;
    }

    private void AnubisBehaviour()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15f)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos); //always face the player
            transform.rotation = rotation;

            if(Vector3.Distance(transform.position, target.transform.position) > 1f && !isAttacking) //when is in range and is no attacking
            {
                switch(routine) //controls the routines of the boss
                {
                    case 0:
                        //Walk
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("walk", true);
                        
                        if(transform.rotation == rotation)
                        {
                            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
                            agent.SetDestination(target.transform.position);
                        }
                        anim.SetBool("attack", false);

                        timer += 1 * Time.deltaTime;
                        if(timer > timeRoutine)
                        {
                            routine = Random.Range(0, 4); //number of routines
                            timer = 0;
                        }
                        break;
                    case 1:
                        //Run
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        anim.SetBool("walk", true);
                        if (transform.rotation == rotation)
                        {
                            //transform.Translate(Vector3.forward * speed * 2.5f * Time.deltaTime);
                            agent.speed *= 3f;
                            agent.SetDestination(target.transform.position);
                        }
                        anim.SetBool("attack", false);
                        break;
                    case 2:
                        //Combos
                        anim.SetBool("walk", false);
                        anim.SetBool("attack", true);
                        anim.SetFloat("skills", 0); //blend tree that manage all the attacks
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        range.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                    case 3:
                        //Skill
                        if(phase == 2)
                        {
                            jumpDistance += 1 * Time.deltaTime;
                            anim.SetBool("walk", false);
                            anim.SetBool("attack", true);
                            anim.SetFloat("skills", 1);
                            //hitSelect = 3;
                            range.GetComponent<CapsuleCollider>().enabled = false;

                            if (directionSkill)
                            {
                                if(jumpDistance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }
                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            routine = 0;
                            timer = 0;
                        }
                        break;
                }
            }
        }
    }

    public void FinalAnimation()
    {
        routine = 0;
        anim.SetBool("attack", false);
        isAttacking = false;
        range.GetComponent <CapsuleCollider>().enabled = true;
        agent.speed = speed;
        jumpDistance = 0;
        directionSkill = false;
    }

    public void DirectionAttackStart()
    {
        directionSkill = true;
    }
    public void DirectionAttackEnd()
    {
        directionSkill = false;
    }

    //Melee Attack
    public void ColliderWeaponTrue()
    {
        foreach(GameObject hitBox in hit)
        {
            hitBox.GetComponent<Collider>().enabled = true;
        }
    }
    public void ColliderWeaponFalse()
    {
        foreach (GameObject hitBox in hit)
        {
            hitBox.GetComponent<Collider>().enabled = false;
        }
    }

    //Fase 2
    public void OnAlive()
    {
        if (hpMin < (hpMax/2)) //when the hp is less than the half of the max hp start Phase2
        {
            phase = 2;
            timeRoutine = 1;
        }
        AnubisBehaviour();
    }

    public void OnHit()
    {
        if(hpMin > 0)
        {
            anim.SetTrigger("hit");
            anim.ResetTrigger("hit");
        }
    }

    private void Update()
    {
        healthBar.fillAmount = hpMin/hpMax;
        if(hpMin > 0)
        {
            OnAlive();
        }
        else
        {
            if (!onDead)
            {
                anim.SetBool("dead", true);
                //music.enable = false;
                onDead = true;
            }
        }
    }
}
