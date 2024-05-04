using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
    [SerializeField] public int attackDamage, health, maxhealth = 100;
    // [SerializeField] private BarraddeVida Healthslide;
    [SerializeField] private Slider healthSlider;
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
        healthSlider = GameObject.Find("Barra deVida").GetComponent<Slider>();

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
        //Healthslide.iniciarBarra(maxhealth);
        healthSlider.maxValue = maxhealth;
        healthSlider.value = healthSlider.maxValue;
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
            else
            {
                // Si el jugador no se está moviendo, resetea las animaciones
                anim.SetFloat("Turning", 0f);
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
        if (Mathf.Abs(moveinput.x) > 0.1 && Mathf.Abs(moveinput.y) > 0.1 && TutorialManager.isStep1 && SceneManager.GetActiveScene().buildIndex == 1)
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
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("Direction", 0f);
            anim.SetBool("Moving", false);
        }
    }

    private void Walk(Vector2 moveinput)
    {
        float y = moveinput.y;
        float x = moveinput.x;
        if (moveinput.y > 0)
        {
            speed = playerSpeed;
            anim.SetFloat("Speed", y);
        }
        if (moveinput.y < 0)
        {
            speed = backSpeed;
            anim.SetFloat("Speed", y);
        }
        if (moveinput.x > 0)
        {
            speed = playerSpeed;
            anim.SetFloat("Direction", x);
        }
        if (moveinput.x < 0)
        {
            speed = playerSpeed;
            anim.SetFloat("Direction", x);
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
        if (isDamagable)
        {
            speed = 0f;
            health -= damage;

            Debug.Log("Player hit");
            anim.SetTrigger("isHit");
            isDamagable = false;
            Invoke(nameof(ResetDamagable), 1f);
            // Healthslide.Cambiarvidaactial(health);
            healthSlider.value = health;
        }

        if (health <= 0) StartCoroutine(onDeath());
    }

    private void ResetDamagable()
    {
        isDamagable = true;
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
