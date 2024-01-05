using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstTimeRunCheck : MonoBehaviour
{
    public int FirstRun;
    private bool skip = false;
    public GameObject skipObject;
    void Start()
    {
        Time.timeScale = 1;
        FirstRun = PlayerPrefs.GetInt("FirstRun");
        if(FirstRun == 0)
        {
            skipObject.SetActive(false);
            Debug.Log("FirstRun");
            PlayerPrefs.SetInt("FirstRun",1);
        }
        else
        {
            skipObject.SetActive(true);
            Debug.Log("Welcome Again");
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Skip();
    }
    public void OnSkipButtonDown()
    {
        skip = true;
    }
    void Skip()
    {
        if(skip)
        {
            SceneManager.LoadScene("Game");
            Time.timeScale = 0;
        }
    }

}
