using UnityEngine;

public class ActivateRune : MonoBehaviour
{
    public RuneEffects runeEffects;
    private void OnTriggerEnter(Collider other)
    {
        runeEffects.Apply(gameObject); // Activate power
        UIRuneCollection.Instance.AddRune(runeEffects); // Display rune on UI
        Destroy(gameObject);

        if (TutorialManager.isTutorial2) // To use only on tutorial scene
            TutorialManager.Instance.StepCompleted();
    }
}
