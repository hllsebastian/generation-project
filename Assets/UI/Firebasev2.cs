using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class Firebasev2 : MonoBehaviour
{
    private FirebaseApp _app;

    // Start is called before the first frame update
    void Start()
    {
                Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
     Debug.Log("Hola");
      _app = Firebase.FirebaseApp.DefaultInstance;

       
  
       

    // Set a flag here to indicate whether Firebase is ready to use by your app.
        login();
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }
private void login(){
    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    /*if(auth.CurrentUser != null){
        Debug.Log("cuenta existente");
        return;
    }*/
    auth.SignInAnonymouslyAsync().ContinueWith(task => {
  if (task.IsCanceled) {
    Debug.LogError("SignInAnonymouslyAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
    return;
  }

  Firebase.Auth.AuthResult result = task.Result;
  Debug.LogFormat("User signed in successfully: {0} ({1})",
      result.User.DisplayName, result.User.UserId);
});
}
}
