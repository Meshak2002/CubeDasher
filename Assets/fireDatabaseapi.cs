using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;

public class fireDatabaseapi : MonoBehaviour
{
    // Start is called before the first frame update
    public FirebaseDatabase database;
    DatabaseReference dref;
    public InputField msgInput;

    private void Awake()
    {
        FirebaseApp.GetInstance("https://cube-dasher-f6a12-default-rtdb.firebaseio.com/");
        FirebaseDatabase.GetInstance("https://cube-dasher-f6a12-default-rtdb.firebaseio.com/");
        database = FirebaseDatabase.GetInstance("https://cube-dasher-f6a12-default-rtdb.firebaseio.com/");
        dref = database.RootReference;
    }
    public void SendMesg(Message msg, Action callback)
    {
        //msg.sender = Profile.NameInputFieldd.text;
        //msg.text = msgInput.text;
        var messageJSON = StringSerializationAPI.Serialize(typeof(Message), msg);
        dref.Child("messages").Push().SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("MSG sent :" + msg.sender + "  " + msg.text+"  "+msg.clan);
            }
            else
            {
                //Debug.Log("Sender" + msg.sender);
                //Debug.Log("Message" + msg.text);
                Debug.Log("Sent failed");
                callback();
            }
        });
    }
    public void ListenForNewMessages(Action<Message> callback)
    {
        void CurrentListener(object o, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null) { Debug.Log(args.DatabaseError); }
            else
                Debug.Log("passed");
                callback(
                    StringSerializationAPI.Deserialize(typeof(Message), args.Snapshot.GetRawJsonValue()) as Message);
        }
        dref.Child("messages").ChildAdded += CurrentListener;
    }
}
