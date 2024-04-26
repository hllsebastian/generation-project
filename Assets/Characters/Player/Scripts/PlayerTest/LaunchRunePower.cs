using System;
using System.Collections;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchRunePower : MonoBehaviour
{

    [SerializeField] GameObject powerPrefab, firePoint;
    [SerializeField] float firingRate;
    public InputsUI uiInput;
    public PlayerCam playerCam;
    static public bool isFireEnable, isInvisibleEnable, isInvisibleLayer;
    private bool timerIsActive /*  isFireLayer, */;
    private float timeLimit = 3;

    private void Awake()
    {
        playerCam = new PlayerCam();
        playerCam.Playercinem.Action.performed += OnClick;
        // uiInput = new InputsUI();
        // uiInput.Buttons.LeftClick.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {

        if (/* isFireLayer */  isFireEnable && !timerIsActive)
        {
            LaunchPower();
        }
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
            // isFireLayer = true;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("Invisible Layer");
            // isFireLayer = false;
            isInvisibleLayer = true;
        }
    }

    private void LaunchPower()
    {
        Debug.Log("FIREEE");
        AudioManager.instance.PlaySoundEffect("SFX1");
        GameObject launchedPower = Instantiate(powerPrefab, firePoint.transform.position, firePoint.transform.rotation);
        timerIsActive = true;
        UIRuneCollection.StartTimer(timeLimit, onTimerComplete);
        Destroy(launchedPower, 2.0f);

        if (TutorialManager.isTutorial3) // Display only on tutorial scene
            TutorialManager.Instance.StepCompleted();
    }

    private void onTimerComplete()
    {
        timerIsActive = false;
    }

    private void OnEnable()
    {
        // uiInput.Enable();
        playerCam.Enable();
    }

    private void OnDisable()
    {
        // uiInput.Disable();
        playerCam.Disable();
    }
}

