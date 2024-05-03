using UnityEngine;

// This script handle the message to display on tutorial scene

public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialSteps;
    private int currentStepIndex = 0;
    public static bool isStep1 = true;
    public static bool isStep2;
    public static bool isStep3;
    public static bool isStep4;
    public static bool isStep5;
    // public static bool reStartTutorial;



    public static TutorialManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowCurrentStep();
    }

    void ShowCurrentStep(bool finalStep = false)
    {
        if (finalStep)
        {
            isStep1 = false;
            isStep2 = false;
            isStep3 = false;
            isStep4 = false;
            isStep5 = false;
        }

        foreach (GameObject panel in tutorialSteps)
        {
            panel.SetActive(false);
        }
        if (currentStepIndex < tutorialSteps.Length)
        {
            tutorialSteps[currentStepIndex].SetActive(true);
        }

    }

    public void StepCompleted(bool finalStep = false)
    {
        currentStepIndex++;
        ShowCurrentStep(finalStep);
    }

    public void RestartTutorial()
    {
        // reStartTutorial = false;
        isStep1 = true;
        currentStepIndex = 0;
        ShowCurrentStep();
    }
}