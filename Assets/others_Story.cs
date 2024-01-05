using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Firebase.Storage;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using WebSocketSharp;

public class others_Story : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference sref;
    [SerializeField] private data otherperson;
    [SerializeField] private bool yes;
    [SerializeField] private GameObject storyPanel;
    [SerializeField] private Text likeb;

    private void Start()
    {
        storyPanel = following.instance.story;
        storage = FirebaseStorage.DefaultInstance;
    }
    public void stat(Text name)
    {
        retrievedata(name.text);
        StartCoroutine(waiStat(name.text));
    }
    IEnumerator waiStat(string name)
    {
        yield return new WaitUntil(() => yes);
        yes = false;
        read_d(name);
        Debug.Log(name);
        for (int i = 0; i < Profile.instance.pref.nane.Count; i++)
        {
           string saveName = Profile.instance.pref.nane[i];
            Debug.Log(saveName);
            if (name == saveName)
            {
                Profile.instance.incrt.enabled = false;
                Profile.instance.ccube.SetActive(true);
                Profile.instance.cscore.text = Profile.instance.pref.cubes[i].ToString();
                Profile.instance.notimes.text = Profile.instance.pref.notimes[i].ToString();
                Profile.instance.likeT.text = otherperson.like.Count.ToString();
                Profile.instance.avg.text = Profile.instance.pref.avg[i].ToString();
                Profile.instance.exp.text = Profile.instance.pref.exp[i].ToString();
                Profile.instance.level.text = (Profile.instance.pref.cubes[i] / 10).ToString();
                break;
            }
        }
    }
    public void others_View(Text name)
    {
        retrievedata(name.text);
        StartCoroutine(wai(name.text));
    }
    public void like(Text name)
    {
        retrievedata(name.text);
        StartCoroutine(waiLike(name.text));
    }
    public void OpenChat(Text name)
    {
        chat.instance.privat_ChatBtnClk(name);
    }
    IEnumerator wai(string name)
    {
        yield return new WaitUntil(() => yes);
        yes = false;
        read_d(name);
        storyPanel.SetActive(true);
        storyPanel.transform.GetChild(1).gameObject.SetActive(false);
        storyPanel.transform.GetChild(0).gameObject.SetActive(false);
        storyPanel.transform.GetChild(4).gameObject.SetActive(false);
        storyPanel.transform.GetChild(2).gameObject.SetActive(true);
        storyPanel.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = otherperson.story;
        Debug.Log("asdasd");
        otherPlayvideo();
    }
    IEnumerator waiLike(string name)
    {
        yield return new WaitUntil(() => yes);
        yes = false;
        read_d(name);
        if (!otherperson.like.Contains(Profile.instance.NameInputField.text))
        {
            otherperson.like.Add(Profile.instance.NameInputField.text);
            Uploaddata(name);
            likeb.text = "Liked";
        }
        else
        {
            likeb.transform.parent.gameObject.SetActive(false);
        }
        
    }
    public void retrievedata(string name)
    {
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" + name + ".json";
        sref.GetFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                yes = true;
                Debug.Log("Sucesssss");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    } 
    public void Uploaddata(string name)
    {
        write_d(name);
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" + name + ".json";
        sref.PutFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
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
        string uname = Application.persistentDataPath + "/" + name + ".json";
        string content = File.ReadAllText(uname);
        otherperson = JsonUtility.FromJson<data>(content);
    }
    public void write_d(string name)
    {
        string uname = Application.persistentDataPath + "/" + name + ".json";
        File.WriteAllText(uname, JsonUtility.ToJson(otherperson));
    }
    public void otherPlayvideo()
    {
        string path = Application.persistentDataPath + "/" + this.transform.GetChild(0).GetComponent<Text>().text + "-" + "video.mp4";
       videoStory.instance. pathpush(path);
        // Download the video file from Firebase Storage
        sref = storage.RootReference.Child(this.transform.GetChild(0).GetComponent<Text>().text + "-" + "video.mp4");
        sref.GetFileAsync(path).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                videoStory.instance.yes = true;
                Debug.Log("Video downloaded successfully.");
                // Set the video player's texture to the downloaded video file

            }
            else
            {
                Debug.Log("Failed to download video.");
            }
        });
    }
}
