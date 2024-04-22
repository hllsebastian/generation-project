using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBaseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject login,signup,profile;
    public InputField Logemail,logpass,Sigpass,Sigemail,SigCpass,Siguser;
    
    public void Oplog(){
        login.SetActive(true);
        signup.SetActive(false);
        profile.SetActive(false);
    }
    public void Opsignup(){
        login.SetActive(false);
        signup.SetActive(true);
        profile.SetActive(false);
    }
    public void Opprofile(){
        login.SetActive(false);
        signup.SetActive(false);
        profile.SetActive(true);
    }
    public void LoginUser(){
        if(string.IsNullOrEmpty(Logemail.text)&&string.IsNullOrEmpty(logpass.text)){
            return;
        }
        //log in 
    }
    public void SignUpUser(){
        if(string.IsNullOrEmpty(Sigemail.text)&&string.IsNullOrEmpty(Sigpass.text)&&string.IsNullOrEmpty(SigCpass.text)&&string.IsNullOrEmpty(Siguser.text)){
            return;
        }
        //Registro 
    }
}
