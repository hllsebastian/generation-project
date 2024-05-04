using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorDeEscenas : MonoBehaviour
{
    /*public GameObject Canvas; // Referencia al canvas para retener imagenes
    public GameObject eventos;*/
    [SerializeField] private GameObject Rune;
    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject); // Mantén este objeto
    }
private void Update() {
    if(Rune.activeSelf){
        Debug.Log("hola");
        gameObject.SetActive(true);
    }
}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected with " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            TutorialManager.Instance.StepCompleted(true);
            PlayerPrefs.SetInt("PrimerJuego",SceneManager.GetActiveScene().buildIndex);
        }
    }
}
