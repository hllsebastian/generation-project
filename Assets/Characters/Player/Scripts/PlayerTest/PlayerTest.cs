using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    private float speed = 5;
    private PlayerInput playerInput;
    private InputAction moveAction;

    // public InputsUI uiInput;

    // private void Awake()
    // {
    //     uiInput = new InputsUI();
    // }


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }


    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed;
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log("Colisionó con: " + other.gameObject.name);
    // }
}
