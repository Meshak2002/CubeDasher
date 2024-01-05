using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Firebase.Storage;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Media;
using SFB;

public class story : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Profile.instance.retj();
        this.transform.GetChild(0).GetComponent<TMP_InputField>().text = Profile.instance.dataa.story;
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }
       
    
    public void story_POST()
    {
        Profile.instance.dataa.story = this.transform.GetChild(0).GetComponent<TMP_InputField>().text;
        Debug.Log(Profile.instance.dataa.story);
        Profile.instance.createj();
    }
    
   
    }

