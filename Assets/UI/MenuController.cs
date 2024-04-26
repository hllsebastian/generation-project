using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pause,buttpause;
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
}
