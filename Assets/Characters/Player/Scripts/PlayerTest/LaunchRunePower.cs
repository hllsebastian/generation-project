using System;
using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchRunePower : MonoBehaviour
{

    [SerializeField] GameObject powerPrefab;
    [SerializeField] GameObject firePoint;
    [SerializeField] float firingRate;
    static public bool isFireEnable;
    static public bool isInvisibleEnable;
    static public bool isInvisibleLayer;
    public InputsUI uiInput;
    private bool isFireLayer;
    private Color originalColor;
    private float timeLimit = 3;
    private bool timerIsActive;

    private void Awake()
    {
        uiInput = new InputsUI();
        uiInput.Buttons.LeftClick.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (isFireLayer && isFireEnable && !timerIsActive)
            LaunchPower();
        if (isInvisibleLayer && isInvisibleEnable)
        {
            Debug.Log("Invisible Layer");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            isInvisibleLayer = false;
            isFireLayer = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("Invisible Layer");
            isFireLayer = false;
            isInvisibleLayer = true;
        }
    }

    private void LaunchPower()
    {
        Debug.Log("FIREEE");
        AudioManager.instance.PlaySoundEffect("SFX1");
        GameObject launchedPower = Instantiate(powerPrefab, firePoint.transform.position, firePoint.transform.rotation);
        timerIsActive = true;
        RuneTimer.StartTimer(timeLimit, onTimerComplete);
        Destroy(launchedPower, 2.0f);
    }

    private void onTimerComplete()
    {
        timerIsActive = false;
        Debug.Log("Now Can Shoot");
    }

    private void OnEnable()
    {
        uiInput.Enable();
    }

    private void OnDisable()
    {
        uiInput.Disable();
    }
}
