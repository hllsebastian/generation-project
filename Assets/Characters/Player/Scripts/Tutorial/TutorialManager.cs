using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialPanels;
    private int currentStepIndex = 0;

    public static bool isTutorial1 = true;
    public static bool isTutorial2;
    public static bool isTutorial3;

    public static TutorialManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }

    void Start()
    {
        ShowCurrentStep();
    }

    void ShowCurrentStep()
    {
        Debug.Log("INDEX:" + currentStepIndex);
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }
        if (currentStepIndex < tutorialPanels.Length)
        {
            tutorialPanels[currentStepIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Tutorial completado");
        }
    }

    public void StepCompleted()
    {
        currentStepIndex++;
        ShowCurrentStep();
        Debug.Log("NEXT STEP");
    }
}


// public class TutorialManager : MonoBehaviour
// {
//     public TutorialStep[] steps;
//     [SerializeField] public TextMeshProUGUI stepText;
//     private int currentStepIndex = 0;

//     void Start()
//     {
//         ShowCurrentStep();
//     }

//     void ShowCurrentStep()
//     {
//         if (currentStepIndex < steps.Length)
//         {
//             stepText.text = steps[currentStepIndex].stepText;
//             steps[currentStepIndex].stepRequired.action.Enable();
//             steps[currentStepIndex].stepRequired.action.performed += HandleActionPerformed;
//         }
//         else
//         {
//             stepText.text = "Tutorial Completed";
//         }
//     }

//     private void HandleActionPerformed(InputAction.CallbackContext context)
//     {
//         if (steps[currentStepIndex].isStepCompleted(context.action))
//         {
//             context.action.performed -= HandleActionPerformed;
//             context.action.Disable();
//             currentStepIndex++;
//             ShowCurrentStep();
//         }
//     }
// }
