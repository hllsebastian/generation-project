using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialSteps;
    private int currentStepIndex = 0;

    public static bool isStep1 = true;
    public static bool isStep2;
    public static bool isStep3;
    public static bool isStep4;
    public static bool isStep5;

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
        foreach (GameObject panel in tutorialSteps)
        {
            panel.SetActive(false);
        }
        if (currentStepIndex < tutorialSteps.Length)
        {
            tutorialSteps[currentStepIndex].SetActive(true);
        }

    }

    public void StepCompleted()
    {
        currentStepIndex++;
        ShowCurrentStep();
    }
}