using SimpleFirebaseUnity;

using UnityEngine;
using CandyCoded.env;
using UnityEngine.UI;


public class ServicesFirebase : MonoBehaviour
{
    private Firebase firebase;
    public Text Message;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("FirebaseService");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
      
    }


    void Start()
    {

        env.TryParseEnvironmentVariable("DATABASE_FIREBASE", out string url_firebase);
        firebase = Firebase.CreateNew($"{url_firebase}/usernames");
        firebase.OnGetSuccess += delegate (Firebase firebase, DataSnapshot value) { ShowResult(firebase, value); };

    }



    public void consultUserData(string user)
    {
      //  orderBy =\"usernames\"
        var urlSecure = WWW.EscapeURL($"equalTo=\"{user}\"\"print=pretty\"");

       // firebase.GetValue(urlSecure);
        firebase.GetValue(FirebaseParam.Empty.EqualTo(user).LimitToFirst(1));
         Debug.Log(urlSecure);



    }
    private void ShowResult(Firebase firebase, DataSnapshot value)
    {
        Message.text = value.RawJson;
        Debug.Log(value.RawJson);
        
    }


}
