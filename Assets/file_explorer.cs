using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Firebase.Storage;
using System.IO;

public class file_explorer : MonoBehaviour
{
    // Start is called before the first frame update
    private string pathe,path;
    public RawImage ri;
    FirebaseStorage fs;
    StorageReference sr;
    public Texture2D tx;
    public Profile pro;
    public void Start()
    {
        fs = FirebaseStorage.DefaultInstance;
    }
    public void FileX()
    {
        path = Application.persistentDataPath + "/" + Profile.NameInputFieldd.text + ".PNG";
        for (int i = 0; i < pro.pref.record; i++)
        {
            string savename = pro.pref.nane[i];
            if (savename == Profile.NameInputFieldd.text)
            {
                if (pro.pref.IUtimes[i] <= 0)
                {
                    pathe = EditorUtility.OpenFilePanel("Overwrite with png", "", "PNG");
                    if (pathe != null)
                    {
                        WWW access = new WWW("file://" + pathe);
                        ri.texture = access.texture;
                        Upload();
                        tx = (Texture2D)ri.texture;
                        byte[] img = tx.EncodeToPNG();
                        File.WriteAllBytes(path, img);
                    }
                }
                else
                {
                    if (pro.pref.cubes[i] >= 200)
                    {
                        pro.pref.cubes[i] -= 200;
                        pathe = EditorUtility.OpenFilePanel("Overwrite with png", "", "PNG");
                        if (pathe != null)
                        {
                            WWW access = new WWW("file://" + pathe);
                            ri.texture = access.texture;
                            Upload();
                            tx = (Texture2D)ri.texture;
                            byte[] img = tx.EncodeToPNG();
                            File.WriteAllBytes(path, img);
                            pro.uploadd();
                        }
                    }
                }
            }
        }
    }
    public void Upload()
    {
        path = Application.persistentDataPath + "/" + Profile.NameInputFieldd.text + ".PNG";
        sr = fs.RootReference.Child(Profile.NameInputFieldd.text + ".PNG");
        sr.PutFileAsync(path).ContinueWith(response =>
        {
            if (response.IsCompleted)
            {
                Debug.Log("IUploaded");
            }
            else
            {
                Debug.Log("IFailed");
            }
        });
        for (int i = 0; i < pro.pref.record; i++)
        {
            string savename = pro.pref.nane[i];
            if (savename == Profile.NameInputFieldd.text)
            {
                pro.pref.IUtimes[i] = 1;
                pro.uploadd();
            }
        }
    }
    public void retclk()
    {
        Debug.Log("Started"+Profile.NameInputFieldd.text);
        StartCoroutine(retrievei());
    }
    public void setIMG()
    {
        path = Application.persistentDataPath + "/" + Profile.NameInputFieldd.text + ".PNG";
        if (path != null)
        {
            WWW ia = new WWW("file://" + path);
            ri.texture = ia.texture;
            Debug.Log("THis is ");
        }
        else
        {
            Debug.Log("File doesn't exist");
        }
    }
    IEnumerator retrievei()
    {
        path = Application.persistentDataPath + "/" + Profile.NameInputFieldd.text + ".PNG";
        Debug.Log(path);
        sr = fs.RootReference.Child(Profile.NameInputFieldd.text + ".PNG");
        sr.GetFileAsync(path).ContinueWith(task =>
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
        setIMG();
        yield return new WaitForSeconds(.1f);
    }
}



