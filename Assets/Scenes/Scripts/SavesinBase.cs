using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.UI;

public class SavesinBase : MonoBehaviour
{
    public GameObject Player;
    public string Archivo;
  


    public Persistencia persistencia= new Persistencia();
    private void Awake() {
        Player=GameObject.FindGameObjectWithTag("Player");
        Archivo = Path.Combine(Application.persistentDataPath, "datosJuego");


      

    }
    // Start is called before the first frame update
    public void Load(){
        StartCoroutine(Cargar());
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){ //carga datos

           StartCoroutine(Cargar());
        
          
        }
        
    }
    private void OnTriggerEnter(Collider other) { //Guardado de datos
        
        Debug.Log("Collision Detected with " + other.gameObject.name);
        if(other.CompareTag("Player")){
        PlayerPrefs.SetInt("PrimerJuego",SceneManager.GetActiveScene().buildIndex);  //actualiza que no es la primera vez que entra en escena

        Persistencia nuevos= new Persistencia(){
            posicion=Player.transform.position
        };
        string cadenaJson=JsonUtility.ToJson(nuevos);
        File.WriteAllText(Archivo, cadenaJson);

       /* int primeraCarga = PlayerPrefs.GetInt("PrimeraCarga");
        Debug.Log("Valor de PrimeraCarga: " + primeraCarga);*/
        Debug.Log("Correctamete guardado");

            
    }
     

    }
    public IEnumerator Cargar(){
         PlayerController controlador = Player.GetComponent<PlayerController>();
    controlador.enabled = false;
         if (File.Exists(Archivo)){
            string contenido=File.ReadAllText(Archivo);
            persistencia=JsonUtility.FromJson<Persistencia>(contenido);
            Debug.Log("posicion del jugador"+ persistencia.posicion);
            Player.transform.position=persistencia.posicion;

         }else{
            Debug.Log("El archivo no existe");
         }
         yield return new WaitForSeconds(2);
         controlador.enabled = true;
    }

    

}
