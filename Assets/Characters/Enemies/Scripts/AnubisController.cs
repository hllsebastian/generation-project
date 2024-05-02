using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnubisController : MonoBehaviour
{
    [Header("Attributes")]
    private int routine;
    private float timer;
    private float timeRoutine;
    private Animator anim;
    private Quaternion angle;
    private float grade;
    private GameObject target;
    private bool isAttacking;
    public float speed;
    public GameObject[] hit;
    public int hitSelect;

    [Header("States")]
    private int fase = 1;
    public float hpMin, hpMax;
    //public Image healthBar;
    //public AudioSource music;
    public bool isDead;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void AnubisBehaviour()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15f)
        {
            Vector3 lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;
        }
    }

}
