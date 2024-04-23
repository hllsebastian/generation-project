using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform CameraMain;
    [SerializeField] private float backSpeed, rotationSpeed, gravityValue, jumpHeight, playerSpeed, speed;
    private PlayerCam playerinput;
    private  Transform child;
    private Animator anim;
    private bool _isAlive = true;
    private bool isDamagable = true;
    private bool hasHit = false;
    [SerializeField] public int attackDamage, health;

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
        controller= GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }
      private void OnEnable() {
        playerinput.Enable();
    }
    private void OnDisable() {
        playerinput.Disable();
    }


    private void Start()
    {
        CameraMain=Camera.main.transform;
        child=transform.GetChild(0).transform;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 moveinput = playerinput.Playercinem.Move.ReadValue<Vector2>();
        Vector3 move =  (CameraMain.forward*moveinput.y+CameraMain.right*moveinput.x); //new Vector3(moveinput.x , 0f , moveinput.y);    
        move.y= 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
       

        // Changes the height position of the player..
        if (playerinput.Playercinem.Jump.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Jump();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if(moveinput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3 (child.localEulerAngles.x,CameraMain.localEulerAngles.y,child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation , rotation ,  Time.deltaTime* rotationSpeed  );
            Walk();
        }
        else{
            anim.SetFloat("Speed", 0f);
            anim.SetBool("Moving", false);
        }
        
    }

    private void Walk()
    {
        
        speed = playerSpeed;
        anim.SetBool("Moving", true);
        anim.SetFloat("Speed", 1.0f);

    }
    private void Jump()
    {

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
            //other.GetComponent<EnemyController>().TakeDamage(attackDamage, 1.0f);
            hasHit = true;
        }

    }

}
