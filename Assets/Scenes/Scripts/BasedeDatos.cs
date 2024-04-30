using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Firebase.Firestore;
using Firebase.Extensions;
using System;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;



public class BasedeDatos : MonoBehaviour
{
    public GameObject Player;
     public Image load;

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
      PlayerPrefs.SetInt("PrimeraCarga", SceneManager.GetActiveScene().buildIndex); //actualiza que no es la primera vez que entra en escena
      int primeraCarga = PlayerPrefs.GetInt("PrimeraCarga");
      Debug.Log("Valor de PrimeraCarga: " + primeraCarga);
   
        
    }

}
    void Start() {
     
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       InitializeFirebase();
       //PlayerPrefs.SetInt("PrimeraCarga", 0); //usado para probar el juego 
       int primeraCarga = PlayerPrefs.GetInt("PrimeraCarga");

    // Imprime el valor en la consola
    Debug.Log("Valor de PrimeraCarga: " + primeraCarga);
      // Comprueba si no es la primera vez que se carga la escena
    if (PlayerPrefs.GetInt("PrimeraCarga") != SceneManager.GetActiveScene().buildIndex-1)
    {
        StartCoroutine(ReadDataCoroutine());
    }

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
        if(Input.GetKeyDown(KeyCode.C)){ //carga datos

           StartCoroutine(ReadDataCoroutine());
        
          
        }
    }
  
    private void UpdateData(){
      /*  FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
        Debug.Log(Player.transform.position);
        Debug.Log(transform.position);
Dictionary<string, object> user = new Dictionary<string, object>
{
        { "x", transform.position.x-1.31 },
        { "y", transform.position.y },
        { "z", transform.position.z },
        { "scene", SceneManager.GetActiveScene().buildIndex },
        
};
docRef.UpdateAsync(user).ContinueWithOnMainThread(task => {
        Debug.Log("Added data to the alovelace document in the users collection."+auth.CurrentUser.UserId);
});*/
    }

    private void ReadData(){
    /*  PlayerController controlador = Player.GetComponent<PlayerController>();
        controlador.enabled = false;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
{
  DocumentSnapshot snapshot = task.Result;
  if (snapshot.Exists) {
    Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
    Dictionary<string, object> user = snapshot.ToDictionary();
    Debug.Log("ahora voy a");
  

Debug.Log("x"+ Convert.ToSingle(user["x"]) +"y "+Convert.ToSingle(user["y"])+"z "+Convert.ToSingle(user["z"]));

   foreach (KeyValuePair<string, object> pair in user) {

      Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
    }

   Player.transform.position=new Vector3(Convert.ToSingle(user["x"]), Convert.ToSingle(user["y"]), Convert.ToSingle(user["z"]));
  } else {
    Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
  }
  
});

controlador.enabled = true;*/
    }
    private IEnumerator ReadDataCoroutine()
{
  //load.enabled = true;

    /*PlayerController controlador = Player.GetComponent<PlayerController>();
    controlador.enabled = false;

    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
    DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);

    Task<DocumentSnapshot> task = docRef.GetSnapshotAsync();
    yield return new WaitUntil(() => task.IsCompleted);
    //yield return new WaitForSeconds(2);
    if (task.Result.Exists)
    {
        Debug.Log(String.Format("Document data for {0} document:", task.Result.Id));
        Dictionary<string, object> user = task.Result.ToDictionary();
        Debug.Log("ahora voy a");

        Debug.Log("x" + Convert.ToSingle(user["x"]) + "y " + Convert.ToSingle(user["y"]) + "z " + Convert.ToSingle(user["z"]));

        foreach (KeyValuePair<string, object> pair in user)
        {
            Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
        }

        Player.transform.position = new Vector3(Convert.ToSingle(user["x"]), Convert.ToSingle(user["y"]), Convert.ToSingle(user["z"]));
        if(Convert.ToInt32(user["scene"])!=SceneManager.GetActiveScene().buildIndex){
         SceneManager.LoadScene(Convert.ToInt32(user["scene"]));
        }
    }
    else
    {
        Debug.Log(String.Format("Document {0} does not exist!", task.Result.Id));
    }
  yield return new WaitForSeconds(2);
    controlador.enabled = true;
   // load.enabled = true;*/
   yield return new WaitForSeconds(2);
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
    }
  }
}
}
