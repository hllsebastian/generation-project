// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RotateRune : MonoBehaviour
// {
//     public InputsUI uiInput;
//     public Rigidbody rb;

//     private void Awake()
//     {
//         uiInput = new InputsUI();
//     }
//     private void OnEnable()
//     {
//         uiInput.Enable();
//     }
//     private void OnDisable()
//     {
//         uiInput.Disable();
//     }

//     private void Update()
//     {
//         float horizontal = uiInput.Buttons.Rotation.ReadValue<float>() * 10f;
//         rb.rotation = Quaternion.Euler(horizontal, 0, 0);
//         //transform.Rotate(Vector3.forward * horizontal * Time.deltaTime);
//     }
// }
