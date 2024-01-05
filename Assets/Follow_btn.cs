using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Firebase.Storage;
using System.Threading.Tasks;

public class Follow_btn : MonoBehaviour
{
    // Start is called before the first frame update
    FirebaseStorage storage;
    StorageReference sref;
   [SerializeField] private data otherperson;
    [SerializeField] private bool yes;
    public Text view_Status;
    [SerializeField]string namee;
    private void Awake()
    {
        storage = FirebaseStorage.DefaultInstance;
    }
    private void Start()
    {
        namee = this.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        retrievedata(namee);
        StartCoroutine(wai(namee));
    }
    public void buttonClicked()
    {
        if (otherperson.nane!="" && yes)
        {
            if (otherperson.privateS == false)
            {
                Profile.instance.dataa.following.Add(namee);
                Profile.instance.createj();
                otherperson.followers.Add(Profile.instance.dataa.nane);
                uploaddata(namee);
                this.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                if (!otherperson.requests.Contains(Profile.instance.dataa.nane))
                {
                    otherperson.requests.Add(Profile.instance.dataa.nane);
                    uploaddata(namee);
                }
                this.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Requested";
                this.transform.GetChild(1).GetComponent<Button>().interactable=false;
            }
        }
    }
    IEnumerator wai(string name)
    {
        yield return new WaitUntil(() => yes);
        read_d(name);
        view_Status.text = otherperson.privateS ? "Private" : "Public";
    }
    public void Req_Accept()
    {
        if (otherperson.nane != "" && yes)
        {
            Profile.instance.dataa.followers.Add(namee);
            Profile.instance.dataa.requests.Remove(otherperson.nane);
            Profile.instance.createj();
            otherperson.following.Add(Profile.instance.dataa.nane);
            uploaddata(namee);
            this.transform.GetChild(3).gameObject.SetActive(false);
        }
     }
    public void function()
    {
        Debug.Log("324234242");
    }
       public void retrievedata(string name)
    {
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" + name + ".json";
        sref.GetFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                function();
                yes = true;
                Debug.Log("Sucesssss");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
    public void read_d(string name)
    {
        Debug.Log(name);
        string uname = Application.persistentDataPath + "/" + name + ".json";
        string content = File.ReadAllText(uname);
        otherperson = JsonUtility.FromJson<data>(content);
    }

    public void writedata(string name)
    {
        string uname = Application.persistentDataPath + "/" + name + ".json";
        File.WriteAllText(uname, JsonUtility.ToJson(otherperson));
    }
    public void uploaddata(string name)
    {
        writedata(name);
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" + name + ".json";
        sref.PutFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Sucesssss");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
}
