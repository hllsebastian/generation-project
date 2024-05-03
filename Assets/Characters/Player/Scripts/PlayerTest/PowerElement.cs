using UnityEngine;

public class PowerElement : MonoBehaviour
{
    private float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision other)
    {
        bool isEnemyTag = other.gameObject.tag == "Enemy";
        if (isEnemyTag)
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);

        // To use only on tutorial scene
        if (TutorialManager.isStep3 && isEnemyTag)
        {
            TutorialManager.Instance.StepCompleted();
            TutorialManager.isStep3 = false;
            TutorialManager.isStep4 = true;

            ParticleSystem particleSystem = GameObject.Find("LightParticles").GetComponent<ParticleSystem>();
            Light[] lights = FindObjectsOfType<Light>();
            lights = System.Array.FindAll(lights, light => light.gameObject.CompareTag("TurnOnLight"));

            particleSystem.Play();
            foreach (Light light in lights)
                light.enabled = true;
        }

    }
}
