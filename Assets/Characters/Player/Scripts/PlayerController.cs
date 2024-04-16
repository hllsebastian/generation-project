using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 inputDir = moveInput.normalized;

        if (inputDir != Vector2.zero)
        {
            transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
        }

        transform.Translate(transform.forward * (5f * inputDir.magnitude) * Time.deltaTime, Space.World);
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
