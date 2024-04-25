using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;
using UnityEngine.SceneManagement;



public class BasedeDatos : MonoBehaviour
{
    public GameObject Player;
    private bool carga=false;
    private Vector3 miVector;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    
    // Start is called before the first frame update
    private void Awake() {
        Player=GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter(Collider other) {
    Debug.Log("Collision Detected with " + other.gameObject.name);
    if(other.CompareTag("Player")){
      UpdateData();
      Debug.Log("x"+ transform.position.x);
      Debug.Log("y"+ transform.position.y);
      Debug.Log("z"+ transform.position.z);
        
    }

}
    void Start() {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       InitializeFirebase();

    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }
        void InitializeFirebase() {
  auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  auth.StateChanged += AuthStateChanged;
  AuthStateChanged(this, null);
}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
           ReadData();
          
        }
        if(Input.GetKeyDown(KeyCode.G)){
          Player.transform.position= miVector;
          carga=false;
        }
    }
  
    private void UpdateData(){
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Debug.Log(Player.transform.position);
        Debug.Log(transform.position);
Dictionary<string, object> user = new Dictionary<string, object>
{
        { "x", transform.position.x },
        { "y", transform.position.y },
        { "z", transform.position.z },
        { "scene", SceneManager.GetActiveScene().buildIndex },
        
};
docRef.UpdateAsync(user).ContinueWithOnMainThread(task => {
        Debug.Log("Added data to the alovelace document in the users collection."+auth.CurrentUser.UserId);
});
    }
   /* private void ReadData(){ //ejemplo
        CollectionReference usersRef = db.Collection("users");
    usersRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
{
  QuerySnapshot snapshot = task.Result;
  foreach (DocumentSnapshot document in snapshot.Documents)
  {
    Debug.Log(String.Format("User: {0}", document.Id));
    Dictionary<string, object> documentDictionary = document.ToDictionary();
    Debug.Log(String.Format("First: {0}", documentDictionary["First"]));
    if (documentDictionary.ContainsKey("Middle"))
    {
      Debug.Log(String.Format("Middle: {0}", documentDictionary["Middle"]));
    }

    Debug.Log(String.Format("Last: {0}", documentDictionary["Last"]));
    Debug.Log(String.Format("Born: {0}", documentDictionary["Born"]));
  }

  Debug.Log("Read all data from the users collection.");
});
    } */
    private void ReadData(){
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
{
  DocumentSnapshot snapshot = task.Result;
  if (snapshot.Exists) {
    Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
    Dictionary<string, object> user = snapshot.ToDictionary();
    Debug.Log("ahora voy a");
  
    miVector=new Vector3(Convert.ToSingle(user["x"]), Convert.ToSingle(user["y"]), Convert.ToSingle(user["z"]));
    carga=true;
    Debug.Log("new pos"+ miVector +"player actual "+Player.transform.position);
                
   /* foreach (KeyValuePair<string, object> pair in user) {

      Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
    }*/
   // miVector=new Vector3(Convert.ToSingle(user["x"]), Convert.ToSingle(user["y"]), Convert.ToSingle(user["z"]));
  } else {
    Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
  }
});


    }
    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
  if (auth.CurrentUser != user) {
    bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
        && auth.CurrentUser.IsValid();
    if (!signedIn && user != null) {
      Debug.Log("Signed out " + user.UserId);
    }
    user = auth.CurrentUser;
    if (signedIn) {
      Debug.Log("Signed in " + user.UserId);
      //isSigned=true;
    }
  }
}
}
