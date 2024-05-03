using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
public class FireBaseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject login,signup,profile,Forgot,notipanel;
     int Scene;
    public TMP_InputField Logemail,logpass,Sigpass,Sigemail,SigCpass,Siguser,Forgotpass;    
    public TMP_Text noti_title,noti_mesage,profUser,profEmail;
    public Toggle remember;
    bool isSigneds=false;
    private FirebaseApp _app;
        public Button botonNew,botonSave;
    private bool save,neww;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    bool isSigned=false;
    void Start() {
      /*
      Debug.Log(""+remember.isOn.CompareTo(true));

      if(remember.isOn.CompareTo(true)==0){

      }*/
     // logout();

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
     Debug.Log("Hola");
      _app = Firebase.FirebaseApp.DefaultInstance;

       InitializeFirebase();
       StartCoroutine(ReadDataCoroutine());
        if (PlayerPrefs.GetInt("Recordar") == -1)
    {
        logout();
    }
       

    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});
    }
    public void Oplog(){
        login.SetActive(true);
        signup.SetActive(false);
        profile.SetActive(false);
        Forgot.SetActive(false);
    }
    public void Opsignup(){
        login.SetActive(false);
        signup.SetActive(true);
        profile.SetActive(false);
        Forgot.SetActive(false);
    }
    public void Opprofile(){
        login.SetActive(false);
        signup.SetActive(false);
        profile.SetActive(true);
        Forgot.SetActive(false);
    }
    public void OpForgot(){
        login.SetActive(false);
        signup.SetActive(false);
        profile.SetActive(false);
        Forgot.SetActive(true);
    }
        
    private IEnumerator ReadDataCoroutine()
{
  //load.enabled = true;



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

        

        foreach (KeyValuePair<string, object> pair in user)
        {
            Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
        }
        /*if(Convert.ToInt32(user["scene"])!=SceneManager.GetActiveScene().buildIndex){
         SceneManager.LoadScene(Convert.ToInt32(user["scene"]));
        }*/
        Scene=Convert.ToInt32(user["scene"]);
    }
    else
    {
        Debug.Log(String.Format("Document {0} does not exist!", task.Result.Id));
    }
  yield return new WaitForSeconds(2);

}
    public void StartGame(){
         //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
          if(save){
             
           // SavesinBase savesinBase = gameObject.AddComponent<SavesinBase>();

           // savesinBase.Load();

           ///////////////>/////> leer datos 
          StartCoroutine(ReadDataCoroutine());
          SceneManager.LoadScene(Scene);

        }else if(neww){
            //que borre la info del .json aun no implementado
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1);

        }
    }
    public void LoginUser(){
        if(string.IsNullOrEmpty(Logemail.text)&&string.IsNullOrEmpty(logpass.text)){
            notierror("Error ", "All fields are required");
            return;
        }
        //log in 
        PlayerPrefs.SetInt("Recordar",remember.isOn.CompareTo(true));
        SignUser(Logemail.text,logpass.text);
    }
    public void SignUpUser(){ //boton crear usuario 
        if(string.IsNullOrEmpty(Sigemail.text)&&string.IsNullOrEmpty(Sigpass.text)&&string.IsNullOrEmpty(SigCpass.text)&&string.IsNullOrEmpty(Siguser.text)){
            notierror("Error ", "All fields are required");
            return;
        }
        //Registro 
        CreateUser(Sigemail.text,Sigpass.text,Siguser.text);
    }
    public void Forgotpas(){
        if(string.IsNullOrEmpty(Forgotpass.text)){
            notierror("Error ", "forgot de email");
            return;
        }
        //Contraseña
    }
    private void notierror(string title , string message){
        noti_title.text = ""+title;
        noti_mesage.text = ""+message;
        notipanel.SetActive(true);
    }
    public void closenoti(){
        noti_title.text = "";
        noti_mesage.text = "";
        notipanel.SetActive(false);
    }
    public void logout(){
        auth.SignOut();
        profEmail.text = "";
        profUser.text = "";
        Oplog();
    }
    private void AddData(){
      FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
      DocumentReference docRef = db.Collection("users").Document(auth.CurrentUser.UserId);
      Dictionary<string, object> user = new Dictionary<string, object>
{
        
        { "scene", 0 },
        
};
docRef.SetAsync(user).ContinueWithOnMainThread(task => {
        Debug.Log("Added data to the alovelace document in the users collection.");
});
    }

    private void CreateUser(string email, string password, string user){
      
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
  if (task.IsCanceled) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    foreach (Exception exception in task.Exception.Flatten().InnerExceptions){
    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
    if (firebaseEx != null)
    {
        var errorCode = (AuthError)firebaseEx.ErrorCode;
        notierror( "Error",GetErrorMessage(errorCode));
    }
    }
    return;
  }

  // Firebase user has been created.
  Firebase.Auth.AuthResult result = task.Result;
  Debug.LogFormat("Firebase user created successfully: {0} ({1})",
      result.User.DisplayName, result.User.UserId);
      
      updateuserprofile(user);
      Oplog();
        });
       /* auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => { /////// codigo de documentacion
  if (task.IsCanceled) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    return;
  }

  // Firebase user has been created.
  Firebase.Auth.AuthResult result = task.Result;
  Debug.LogFormat("Firebase user created successfully: {0} ({1})",
      result.User.DisplayName, result.User.UserId);
});*/

        
    }
    private void SignUser(string email, string password){
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
  if (task.IsCanceled) {

    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
    foreach (Exception exception in task.Exception.Flatten().InnerExceptions){
    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
    if (firebaseEx != null)
    {
        var errorCode = (AuthError)firebaseEx.ErrorCode;
        notierror( "Error",GetErrorMessage(errorCode));
    }
    }
    return;
  }

  Firebase.Auth.AuthResult result = task.Result;
  Debug.LogFormat("User signed in successfully: {0} ({1})",
      result.User.DisplayName, result.User.UserId);
        profEmail.text = "" + result.User.Email;
        profUser.text = ""+ result.User.DisplayName;
        Debug.Log(result.User.DisplayName+"--"+result.User.Email);
        PlayerPrefs.SetInt("PrimerJuego",Scene);
        if(PlayerPrefs.GetInt("PrimerJuego")==0){
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)+1);

        }else {
        Opprofile();
        }
        });
        
    }
    void InitializeFirebase() {
  auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  auth.StateChanged += AuthStateChanged;
  AuthStateChanged(this, null);
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
      isSigned=true;
    }
  }
}

void OnDestroy() {
  auth.StateChanged -= AuthStateChanged;
  auth = null;
}
void updateuserprofile(string Username){
    Firebase.Auth.FirebaseUser user = auth.CurrentUser;
if (user != null) {
  Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
    DisplayName = Username,
    
    PhotoUrl = new System.Uri("https://example.com/jane-q-user/profile.jpg"),
    

  };
  user.UpdateUserProfileAsync(profile).ContinueWith(task => {
    if (task.IsCanceled) {
      Debug.LogError("UpdateUserProfileAsync was canceled.");
      return;
    }
    if (task.IsFaulted) {
      Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
      return;
    }

    Debug.Log("User profile updated successfully."+Username);
    AddData();
    //notierror("Alert","Acount Succesfully created");
  });

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





    private void Update() {
    if(isSigned){
        if(!isSigneds){
            isSigneds=true;
            profEmail.text = "" + user.Email;
            profUser.text = ""+ user.DisplayName;
            Opprofile();
        }
    }
}
private static string GetErrorMessage(AuthError errorCode)
{
    var message = "";
    switch (errorCode)
    {
        case AuthError.AccountExistsWithDifferentCredentials:
            message = "Ya existe la cuenta con credenciales diferentes";
            break;
        case AuthError.MissingPassword:
            message = "Hace falta el Password";
            break;
        case AuthError.WeakPassword:
            message = "El password es debil";
            break;
        case AuthError.WrongPassword:
            message = "El password es Incorrecto";
            break;
        case AuthError.EmailAlreadyInUse:
            message = "Ya existe la cuenta con ese correo electrónico";
            break;
        case AuthError.InvalidEmail:
            message = "Correo electronico invalido";
            break;
        case AuthError.MissingEmail:
            message = "Hace falta el correo electrónico";
            break;
        default:
            message = "Ocurrió un error";
            break;
    }
    return message;
}
}
