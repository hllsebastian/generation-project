using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform CameraMain;
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    private float  RotationSpeed =4f;
   private PlayerCam playerinput;
   private  Transform child;

    void Awake()
    {
        playerinput = new PlayerCam();
        controller= GetComponent<CharacterController>();
        
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

    void Update()
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
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if(moveinput != Vector2.zero){
            Quaternion rotation = Quaternion.Euler(new Vector3 (child.localEulerAngles.x,CameraMain.localEulerAngles.y,child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation , rotation ,  Time.deltaTime* RotationSpeed  );
        }
    }

}
