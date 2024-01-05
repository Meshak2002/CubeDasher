using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.Networking;
public class MainMenuController : MonoBehaviour
{
    public static bool gameIsPaused;
    bool no;
    private bool menu = false;
    public Button leftButton;
    public Button rightButton;
    public Text name,Tname,Tcube,loc;
    public prefs pref;
    private bool rode;
    private bool yes;
    public List<string> mname;
    public List<int> max;
   // private bool no;
    public GameObject menuPanel;
    public PlayerCollision pc;
    private int mval,lk;
    public Country countr;
    

    void Start()
    {

        read();
        no = false;
        yes = false;
        //no = false;
        menuPanel.SetActive(false);
        StartCoroutine(waitt());
    }

    // Update is called once per frame
    void Update()
    {   
        Menu();
        if (rode == true)
        {
            name.text = pref.nane[PlayerPrefs.GetInt("LastAccess") - 1].ToString();
            if (pc.currentCube > mval)
            {
                mname.Clear();
                max.Clear();
                StartCoroutine(waitt());
                rode = false;
            }
        }
    }
    public void loclk()
    {
        if (lk == 0)
        {
            StartCoroutine(countryfinder());
            mname.Clear();
            max.Clear();
            StartCoroutine(waitt());
        }
        else if (lk == 1)
        {
            loc.text = "Global".ToString();
            mname.Clear();
            max.Clear();
            StartCoroutine(waitt());
        }
        else
        {
            lk = 0;
            StartCoroutine(countryfinder());
            mname.Clear();
            max.Clear();
            StartCoroutine(waitt());
        }
        lk++;
    }
    IEnumerator countryfinder()
    {
        UnityWebRequest ur = UnityWebRequest.Get("https://extreme-ip-lookup.com/json/?key=CmobwLkY8dKdXt2B6y2J");
        ur.chunkedTransfer = false;
        yield return ur.Send();
        if (ur.isNetworkError)
        {
            Debug.Log("Unable   to  fetch   Location");
        }
        else
        {
            if (ur.isDone)
            {
                countr = JsonUtility.FromJson<Country>(ur.downloadHandler.text);
                Debug.Log("Country" + ur.downloadHandler.text);
                loc.text = countr.country.ToString();

            }
        }
    }
    IEnumerator waitt()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < pref.record; i++)
        {
            if (pref.cubes[i] > pc.currentCube)
            {
                if (loc.text == "Global".ToString())
                {
                    mname.Add(pref.nane[i]);
                    max.Add(pref.cubes[i]);
                }
                else
                {
                    if (loc.text == pref.location[i].ToString())
                    {
                        mname.Add(pref.nane[i]);
                        max.Add(pref.cubes[i]);
                    }
                }
            }
        }
        Tcube.text = max.Min().ToString();
        Tname.text = mname[(max.IndexOf(max.Min()))].ToString();
        mval = max.Min();
        rode = true;
    }
    public void OnButtonDownMenu()
    {
        menu = true;
        no = false;
        
    }
    public void read()
    {
        string path = Application.persistentDataPath + "/PlayerP.json";
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            pref = JsonUtility.FromJson<prefs>(content);
            Debug.Log("Rode");
        }
    }
        void Menu()
    {
        if(menu)
        {
            leftButton.interactable = false;
            rightButton.interactable = false;
            menuPanel.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused=true;
            No();
            //SceneManager.LoadScene("MainMenu");
        }
    }
    public void Yes()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
     // Update is called once per frame
    
    public void OnButtonDownNo()
    {
        no = true;
        
    }
    void No()
    {
        if (no == true)
        {
           leftButton.interactable = true;
            rightButton.interactable = true;
            menuPanel.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused=false;
        }
        
        
    }
    public void next()
    {

        PlayerPrefs.SetInt("LastAccess", 0);
        SceneManager.LoadScene(1);
    }
    
}
