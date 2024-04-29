using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pause,buttpause,login,Save;
    public Button botonNew,botonSave;
    private bool save,neww;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Oppause(){
       
        pause.SetActive(true);
        buttpause.SetActive(false);
   
    }
     public void Close(){
        pause.SetActive(false);
        buttpause.SetActive(true);
     }
     public void oplogin(){
        Save.SetActive(false);
        login.SetActive(true);
     }
     public void opSave(){
        login.SetActive(false);
        Save.SetActive(true);
     }
     public void log(){
        int primerjuego = PlayerPrefs.GetInt("PrimerJuego");
        if(PlayerPrefs.GetInt("PrimerJuego")==0){
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1);

        }else{
            opSave();
        }
        opSave();
     }
    public void back(){
        oplogin();
    }
    public void play(){
        if(save){
            // SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1); //toca que guarde la escena aun no implementado
            SavesinBase savesinBase =new SavesinBase ();
            savesinBase.Load();

        }else if(neww){
            //que borre la info del .json aun no implementado
        }
    }
    public void btsave(){

    botonSave.image.color = Color.green;
    botonNew.image.color = Color.white;
    save=true;
    neww=false;


    }
     public void btnew(){

    botonNew.image.color = Color.green;
    botonSave.image.color = Color.white;
    save=false;
    neww=true;

    }
















}
