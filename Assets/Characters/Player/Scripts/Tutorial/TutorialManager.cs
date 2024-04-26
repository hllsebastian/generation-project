using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] public GameObject[] tutorialPanels;
    private int currentStepIndex = 0;

    public static bool isTutorial1 = true;
    public static bool isTutorial2;
    public static bool isTutorial3;
    public static bool isTutorial4;
    public static bool isTutorial5;

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
        Debug.Log("second INDEX:" + currentStepIndex);
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }
        if (currentStepIndex < tutorialPanels.Length)
        {
            tutorialPanels[currentStepIndex].SetActive(true);
        }
        if (currentStepIndex == 3)
        {
            tutorialPanels[4].SetActive(true);
        }

    }

    public void StepCompleted()
    {
        currentStepIndex++;
        ShowCurrentStep();
        Debug.Log("NEXT STEP");
    }
}