using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnubisRange : MonoBehaviour
{
    public Animator anim;
    public AnubisController boss;
    private int melee;
    public NavMeshAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.StopMovement();
            //agent.velocity = Vector3.zero;
            melee = Random.Range(0, 5);
            switch(melee)
            {
                case 0:
                    //Attack 1
                    anim.SetFloat("skills", 0f);
                    //boss.hitSelect = 0;
                    break;
                case 1:
                    //Attack 2
                    anim.SetFloat("skills", 0.25f);
                    //boss.hitSelect = 1;
                    break;
                case 2:
                    //Combo 1
                    anim.SetFloat("skills", 0.5f);
                    //boss.hitSelect = 2;
                    break;
                case 3:
                    //Combo 2
                    anim.SetFloat("skills", 0.75f);
                    //boss.hitSelect = 3;
                    break;
                case 4:
                    //Skill
                    if(boss.phase == 2)
                    {
                        anim.SetFloat("skills", 1f);
                    }
                    else
                    {
                        melee = 0;
                    }
                    break;
            }
            anim.SetBool("walk", false);
            anim.SetBool("attack", true);
            boss.isAttacking = true;
            GetComponent<CapsuleCollider>().enabled = false; 
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    agent.speed = boss.speed;
    //}
}
