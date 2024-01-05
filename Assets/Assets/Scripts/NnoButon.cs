using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NnoButon : MonoBehaviour
{
    private admanager ad;
    private bool start;
    private bool quit;
    private bool options;
    public GameObject optionsMenu, creditsMenu;
    private bool credits;
    private bool back;
    private bool creditsBack;
    public string URL;
    public Button[] removeBtn;

    void Start()
    {
        ad = GameObject.FindGameObjectWithTag("ad").GetComponent<admanager>();
        creditsBack = false;
        //creditsMenu.SetActive(false);
        credits = false;
        PlayerPrefs.GetInt("qualityIndex", 1);
        back = false;
        optionsMenu.SetActive(false);
        start = false;
        quit = false;
        options = false;
    }
    void Update()
    {
        StartGame();
        Option();
        QuitGame();
        Back();
        Credits();
        CreditsBack();
    }
    public void CarrerButton()
    {
        SceneManager.LoadSceneAsync("Carrer");
    }
    public void OnButtonDownStart()
    {
        start = true;
    }
    public void OnButtonDownQuit()
    {
        quit = true;
    }
    public void OnButtonDownOptions()
    {
        options = true;
        back = false;
    }
    public void OnButtondownback()
    {
        back= true;
    }
    public void OnButtonDownCredits()
    {
        credits = true;
        creditsBack = false;
    }
    public void OnButtonDownCreditsBack()
    {
        creditsBack = true;
    }
    public void SetGraphics(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        
        PlayerPrefs.SetInt("qualityIndex", 1);
    }
    void StartGame()
    {
        if(start)
        {
            ad.ShowInteristialAd();
        }
    }
    void QuitGame()
    {
        if(quit)
        {
            Application.Quit();
        }
    }
    void Option()
    {
        if(options)
        {
            optionsMenu.SetActive(true);
        }
    }
    void Back()
    {
        if(back)
        {
            optionsMenu.SetActive(false);
        }
    }
    void Credits()
    {
        if(credits)
        {
            creditsMenu.SetActive(true);
        }
    }
    void CreditsBack()
    {
        if(creditsBack)
        {
            creditsMenu.SetActive(false);
        }
    }
    public void LinkOpen()
    {
        Application.OpenURL(URL);
    }

}
    
