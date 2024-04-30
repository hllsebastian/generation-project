using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerElement : MonoBehaviour
{

    private float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        bool isEnemyTag = other.gameObject.tag == "Enemy";
        if (isEnemyTag)
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);

        if (TutorialManager.isStep3 && isEnemyTag)
        {
            TutorialManager.Instance.StepCompleted();
            TutorialManager.isStep3 = false;
            TutorialManager.isStep4 = true;
            Debug.Log("Step4: " + TutorialManager.isStep4);
        }

    }
}
