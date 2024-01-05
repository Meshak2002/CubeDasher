using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor.Media;
using UnityEngine.UI;
using Firebase.Storage;
using System.IO;
using System.Threading.Tasks;

public class videoStory : MonoBehaviour
{
    private string path;
    FirebaseStorage storage;
    StorageReference reference;
    [SerializeField] private VideoClip vClip;
    [SerializeField] private VideoPlayer vplayer;
    [SerializeField] public bool yes,other;
    public static videoStory instance;
    private void OnEnable()
    {
        instance = this;
        other = false;
    }
    public void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
    }

    public void UploadVideo()
    {
        path = Application.persistentDataPath + "/"+ Profile.instance.dataa.nane + "-" + "video.mp4";

        // Open a file explorer to select the video file
        string videoPath = UnityEditor.EditorUtility.OpenFilePanel("Select video file", "", "mp4");

        if (!string.IsNullOrEmpty(videoPath))
        {
            // Copy the selected video file to the persistent data path
            File.Copy(videoPath, path, true);

            // Upload the video file to Firebase Storage
            reference = storage.RootReference.Child(Profile.instance.dataa.nane+"-"+"video.mp4");
            reference.PutFileAsync(path).ContinueWith(task => {
                if (task.IsCompleted)
                {
                    Debug.Log("Video uploaded successfully.");
                }
                else
                {
                    Debug.Log("Failed to upload video.");
                }
            });
        }
    }
    
    public void PlayVideo()
    {
        if (other == false)
        {
            path = Application.persistentDataPath + "/" + Profile.instance.dataa.nane + "-" + "video.mp4";
            StartCoroutine(wait(path));
            // Download the video file from Firebase Storage
            reference = storage.RootReference.Child(Profile.instance.dataa.nane + "-" + "video.mp4");
            reference.GetFileAsync(path).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    yes = true;
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
    public void pathpush(string path)
    {
        other = true;
        StartCoroutine(wait(path));
    }
    IEnumerator wait(string path)
    {
        yield return new WaitUntil(() => yes);
        yes = false;
        vplayer.url = path;
        vplayer.Prepare();
        yield return new WaitUntil(() => vplayer.isPrepared);
        vplayer.Play();
    }
}
