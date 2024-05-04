using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform CameraMain;
    [SerializeField] private float backSpeed, rotationSpeed, gravityValue, jumpHeight, speed;
    public float playerSpeed;
    [SerializeField] private GameObject[] TutorialObject;
    private PlayerCam playerinput;
    private Transform child;
    private Animator anim;
    private bool _isAlive = true;
    private bool isDamagable = true;
    private bool canAttack = true;
    private bool hasHit = false;
    [SerializeField] public int attackDamage, health;
    [SerializeField] private BarraddeVida barradeVida;
    float lastAngle = 0f;
    float stopThreshold = 0.1f;


    public bool isAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            anim.SetBool("isAlive", value);
        }
    }

    void Awake()
    {
        playerinput = new PlayerCam();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        playerinput.Enable();
    }
    private void OnDisable()
    {
        playerinput.Disable();
    }


    private void Start()
    {
        CameraMain = Camera.main.transform;
        child = transform.GetChild(0).transform;
        // barradeVida.iniciarBarra(health);
    }

    private void Update()
    {
        float currentAngle = CameraMain.localEulerAngles.y;
        Move();
        // transform.GetChild(0).position = controller.transform.position;
        transform.GetChild(0).rotation = Quaternion.Euler(0, CameraMain.eulerAngles.y, 0);

        if (Mathf.Abs(currentAngle - lastAngle) > stopThreshold)
        {
            // El jugador se está moviendo
            if (currentAngle < lastAngle || (currentAngle - lastAngle > 180 && currentAngle - lastAngle < 360))
            {
                Debug.Log("izq");
                anim.SetFloat("Turning", -1.0f);
                anim.SetBool("Moving", false);
            }
            if (currentAngle > lastAngle && (currentAngle - lastAngle < 180))
            {
                Debug.Log("der");
                anim.SetFloat("Turning", 1.0f);
                anim.SetBool("Moving", false);
            }


        }
        lastAngle = currentAngle;

        if (canAttack)
            Attack();
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 moveinput = playerinput.Playercinem.Move.ReadValue<Vector2>();
        Vector3 move = (CameraMain.forward * moveinput.y + CameraMain.right * moveinput.x); //new Vector3(moveinput.x , 0f , moveinput.y);    
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // To display only on tutorial scene
        if (Mathf.Abs(moveinput.x) > 0.1 && Mathf.Abs(moveinput.y) > 0.1 && TutorialManager.isStep1)
        {
            TutorialManager.isStep1 = false;
            TutorialManager.Instance.StepCompleted();
            TutorialObject[0].gameObject.SetActive(true);
            TutorialManager.isStep2 = true;
        }

        // Changes the height position of the player..

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (moveinput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, CameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotationSpeed);
            Walk(moveinput);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
            anim.SetBool("Moving", false);
        }

    }

    private void Walk(Vector2 moveinput)
    {
        if (moveinput.y > 0)
        {
            speed = playerSpeed;
            anim.SetBool("Moving", true);
            anim.SetFloat("Forward", 1.0f);
        }
        if (moveinput.y < 0)
        {
            speed = backSpeed;
            anim.SetBool("Moving", true);
            anim.SetFloat("Forward", -1.0f);
        }
        if (moveinput.x > 0)
        {
            speed = playerSpeed;
            anim.SetFloat("Side", 1.0f);
            anim.SetBool("Moving", true);
        }
        if (moveinput.x < 0)
        {
            speed = playerSpeed;
            anim.SetFloat("Side", -1.0f);
            anim.SetBool("Moving", true);
        }
    }
    IEnumerator onDeath()
    {

        isAlive = false;
        isDamagable = false;
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        speed = 0f;
        health -= damage;
        barradeVida.Cambiarvidaactial(health);
        Debug.Log("Player hit");
        anim.SetTrigger("isHit");
        if (health <= 0) StartCoroutine(onDeath());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !hasHit)
        {
            Debug.Log("Damage to: " + other.gameObject.name);
            other.GetComponent<EnemyManager>().TakeDamage(attackDamage);
            hasHit = true;
        }

        // Only to use on Tutorial Scene
        if (TutorialManager.isStep4 && other.gameObject.CompareTag("Save_point"))
        {
            TutorialManager.Instance.StepCompleted();
            TutorialManager.isStep4 = false;
            TutorialManager.isStep5 = true;
            TutorialObject[1].gameObject.SetActive(true);
        }
    }

    private void Attack()
    {
        float rightStickPressed = playerinput.Playercinem.Action.ReadValue<float>();
        if (rightStickPressed >= 1)
        {
            anim.SetBool("isAttacking", true);
            canAttack = false;
            StartCoroutine(EnableAttack());
        }
    }

    private IEnumerator EnableAttack()
    {
        yield return new WaitForSeconds(1.0f);
        canAttack = true;
    }
}
