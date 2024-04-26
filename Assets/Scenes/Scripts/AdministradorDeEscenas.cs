using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorDeEscenas : MonoBehaviour
{
    /*public GameObject Canvas; // Referencia al canvas para retener imagenes
    public GameObject eventos;*/
    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject); // Mantén este objeto
    }

   /* public void CambiarEscena(int nombreDeLaEscena)
    {
        // Guarda los objetos que quieres mantener
        DontDestroyOnLoad(Canvas);
        DontDestroyOnLoad(eventos);

        // Carga la nueva escena
        SceneManager.LoadScene(nombreDeLaEscena);
    }*/
    private void OnTriggerEnter(Collider other) {
    Debug.Log("Collision Detected with " + other.gameObject.name);
    if(other.CompareTag("Player")){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
}
}
