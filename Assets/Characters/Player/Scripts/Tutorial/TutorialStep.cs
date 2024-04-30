using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Step", menuName = "Tutorial/NewStep")]
public class TutorialStep : ScriptableObject
{
    public string stepText;
    public InputActionReference stepRequired;

    public bool isStepCompleted(InputAction action)
    {
        return action.triggered;
    }
}
