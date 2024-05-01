using UnityEngine;

public class RunePickup : MonoBehaviour
{
    [SerializeField] RunePower powerData;
    [SerializeField] GameObject playerPrefab, TutorialEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RuneCollection.Instance.AddRune(powerData);
            Destroy(gameObject);
        }

        if (TutorialManager.isStep2) // To use only on tutorial scene
        {
            TutorialManager.Instance.StepCompleted();
            TutorialManager.isStep2 = false;
            TutorialManager.isStep3 = true;
            TutorialEnemy.SetActive(true);
        }
    }
}
