using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class share : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel,over;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onclk()
    {
        panel.SetActive(true);
        over.SetActive(false);
        Debug.Log("Clicked");
        StartCoroutine(Onshare());
    }
    IEnumerator Onshare()
    {
        yield return new WaitForEndOfFrame();
        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();
        string path = Path.Combine(Application.temporaryCachePath, "Sharedimage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());
        Destroy(tx);
        new NativeShare()
            .AddFile(path)
            .SetSubject("This is my score")
            .SetText("Share your score with friends")
            .Share();
        panel.SetActive(false);
        over.SetActive(true);

    }
}
