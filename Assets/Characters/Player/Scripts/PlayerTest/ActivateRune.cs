using UnityEngine;

public class ActivateRune : MonoBehaviour
{
    public RuneEffects runeEffects;
    [SerializeField] GameObject TutorialEnemy;
    private void OnTriggerEnter(Collider other)
    {
        runeEffects.Apply(gameObject); // Activate power
        UIRuneCollection.Instance.AddRune(runeEffects); // Display rune on UI
        Destroy(gameObject);

        if (TutorialManager.isStep2) // To use only on tutorial scene
        {

            TutorialManager.Instance.StepCompleted();
            TutorialManager.isStep2 = false;
            TutorialManager.isStep3 = true;
            TutorialEnemy.SetActive(true);
        }
    }
}
