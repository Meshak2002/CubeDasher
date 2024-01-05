using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstItmeMainmenu : MonoBehaviour
{
    public int FirstMenuRun;
    public GameObject toBehidden;
    public GameObject toBehidden1; 
    
    // Start is called before the first frame update
    void Start()
    {
        FirstMenuRun = PlayerPrefs.GetInt("FirstMenuRun");
        if(FirstMenuRun == 0)
        {
            toBehidden.SetActive(false);
            //toBehidden1.SetActive(false);
            Debug.Log("FirstRun");
            PlayerPrefs.SetInt("FirstMenuRun",1);
        }
        else
        {
            toBehidden.SetActive(true);
            //toBehidden1.SetActive(true);
            //skipObject.SetActive(true);
            Debug.Log("Welcome Again");  
        }
    }
}
