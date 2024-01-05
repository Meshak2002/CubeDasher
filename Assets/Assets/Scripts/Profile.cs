using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using Firebase.Storage;
using UnityEngine.Networking;
using System.Threading.Tasks;
using HutongGames.PlayMaker.Actions;
using TMPro;

public class Profile : MonoBehaviour
{
    public InputField NameInputField;
    public static InputField NameInputFieldd,passwordFielDD;
    public Text loaded_Name, notAvailaible,LOCATION;
    public string NameOfPlayer;
    public static string saveName,savepassow;
    public Button okayButton, backButton;
    public Toggle loginInOrOut;
    private bool notify,updat,logout,stop;
    public GameObject notipan,logou,ccube,UI1,UI2,dashpanel,rawimg,upbutton,notpanel;
    public InputField password;
    public static int record;
    public Text heading,log,pass,passtext;
    public Text buttontx, notavail, incrt,cscore,avg,notimes,exp,level,passhide,viewStat,likeT;
    public prefs pref;
    public data dataa;
    private string path,srefname;
    FirebaseStorage storage;
    StorageReference sref;
    public int la;
    private bool rode=false,remeber;
    private GameObject touch;
    public bool sa;
    private int ft,view,staddress,enaddress,xt;
    public GameObject img,li, text, butto,toucheffect,tmp,canvas,captcha,clanpanel;
    public int playerID { get; private set; }
    public string playerIdentifier { get; private set; }
    public bool playerLoggedIn,yess;
    public file_explorer fe;
    public Country countr;
    private string countri;
    public List<Text> Sno, Name, medal;
    public List<Button> frnd, sta;
    public List<int> oder;
    public static Profile instance;
    public GameObject menu,clanPanel;
    public List<data> tempdlist;
    public List<string> strlist;
    public GameObject logn, menuopened, hide;
    string oname;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        StartCoroutine(countryfinder());
        StartCoroutine(loading());
        storage = FirebaseStorage.DefaultInstance;
        retrieveclan();
        path = Application.persistentDataPath + "/PlayerP.json";
        retrievee();
        StartCoroutine(wa());
        Debug.Log(path);
        NameInputField.characterLimit = 8;
        updat = true;
        NameInputFieldd = NameInputField;
        passwordFielDD = password;
        ft = PlayerPrefs.GetInt("FirstTime");
        if (ft == 0)
        {
            logout = true;
            yes();
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        staddress = 0;
        enaddress = 4;
        //StartSession();
    }
    public void retrieveclan()
    {
        path = Application.persistentDataPath + "/Clans_.json";
        sref = storage.RootReference.Child("Clans_");
        sref.GetFileAsync(path).ContinueWith(response =>
        {
            if (response.IsCompleted)
            {
                Debug.Log("Clan retrieved");
            }
            else
            {
                Debug.Log("Clan retrieval Failed");
            }
        });
    }
    public void uploadd()
    {
        write();
        sref = storage.RootReference.Child("PlayerP.json");
        sref.PutFileAsync(path).ContinueWith(task =>
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
    public void uploadds()
    {
        sref = storage.RootReference.Child(NameInputField.text + ".json");
        string pat = Application.persistentDataPath + "/" + NameInputField.text + ".json";
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
    public void rUnlock()
    {
        for (int i = 0; i < pref.nane.Count; i++)
        {
           
            saveName = pref.nane[i];
            savepassow = pref.pasword[i];
            Debug.Log(saveName);
            if (NameInputField.text == saveName && password.text == savepassow)
            {
                //hide.SetActive(false);
                hide.transform.localPosition = new Vector3(10000, 0, 0);
                logn.SetActive(false);
                menuopened.SetActive(true);
                retrieves();
                StartCoroutine(wait());
            }
        }
    }
    IEnumerator wait()
    {
        Debug.Log("Asdsadadasa");
        yield return new WaitUntil(() => yess);
        retj();
        if (dataa.notification != null&& dataa.notification != "")
        {
            notpanel.SetActive(true);
            notpanel.transform.GetChild(0).gameObject.SetActive(true);
            notpanel.transform.GetChild(1).gameObject.SetActive(false);
            notpanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = dataa.notification;
            notpanel.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate {
                dataa.notification = "";
                notpanel.SetActive(false);
                createj();
            });
        }
        yess = false;
        viewStat.text = (dataa.privateS == false) ? "Public" : "Private";
    }

    public void retrieves()
    {
        string pat = Application.persistentDataPath + "/" + NameInputField.text + ".json";
        sref = storage.RootReference.Child(NameInputField.text + ".json");

        sref.GetFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                yess = true;
                Debug.Log("Sucesssss");
            }
            else
            {
                Debug.Log("Failed");
            }
        });

    }
    public void createj()
    {
        string uname = Application.persistentDataPath + "/" + NameInputField.text+".json";
        File.WriteAllText(uname,JsonUtility.ToJson(dataa));
        uploadds();
    }
    public void retj()
    {
       // Debug.Log("WRITE");
        string uname = Application.persistentDataPath + "/" + NameInputField.text + ".json";
        string content = File.ReadAllText(uname);
        dataa= JsonUtility.FromJson<data>(content);
      //  Debug.Log(content);
     
    }
    public void retrievee() {
        path = Application.persistentDataPath + "/PlayerP.json";
        sref = storage.RootReference.Child("PlayerP.json");
        sref.GetFileAsync(path).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log(task.Exception);
                sa = true;
            }
        });
    }
    IEnumerator wa()
    {
        yield return new WaitUntil(() => sa);
        sa = false;
        read();
    }
    private void Update()
    {
        tmp.transform.position = Input.mousePosition;
       if (Input.GetButtonDown("Fire1"))
        {
           touch = Instantiate(toucheffect,new Vector3(tmp.transform.position.x,tmp.transform.position.y, tmp.transform.position.z),Quaternion.identity);
            touch.transform.SetParent(canvas.transform);
            touch.transform.SetSiblingIndex(23);
            touch.transform.position = Input.mousePosition;
            StartCoroutine(Dest(touch));
        }
        int len = password.text.Length;
        string astr = "*";
        if (password.text.Length > passhide.text.Length)
        {
            passhide.text += astr.ToString();
        }
        if (password.text.Length < passhide.text.Length)
        {
            Debug.Log("REmove");
            passhide.text="".ToString();
        }
        if (rode == false)
        {
            read();
        }
        //PlayerPrefs.SetInt("LastAccess",0);
        la = PlayerPrefs.GetInt("LastAccess");
        //Debug.Log(path);
        NameOfPlayer = PlayerPrefs.GetString("Name", "none");
        if (loaded_Name.text.Length < 5)
        {
            loaded_Name.text = "";
            backButton.interactable = false;
        }
        else
        {
            loaded_Name.text = NameOfPlayer;
            backButton.interactable = true;
        }
        if ((!loginInOrOut.isOn || logout==true) && updat == true)
        {
            updat = false;
            logout = false;
        }
        if (loginInOrOut.isOn)
        {
            buttontx.text = "Play";
        }
        else
        {
            buttontx.text = "Signup";
        }

        }
    void EnabledText()
    {
        notAvailaible.enabled = true;
    }
    void DisbableText()
    {
        notAvailaible.enabled = false;
    }
    public void clkclan()
    {
        record = pref.record;
        for (int i = 0; i < record; i++)
        {
            //Debug.Log("Clicked");
            saveName = pref.nane[i];
            savepassow = pref.pasword[i];
            if (NameInputField.text == saveName && password.text == savepassow)
            {
                clanpanel.SetActive(true);
                retrieves();
            }
        }
    }
    public void AIcheck()
    {
        la = PlayerPrefs.GetInt("LastAccess");
        if (la>0)
        {
            Debug.Log("AI");
            int i = la - 1;
            //record = PlayerPrefs.GetInt("Record");
            Debug.Log(la-1);
            saveName = pref.nane[i];
            savepassow = pref.pasword[i];
            //saveName = PlayerPrefs.GetString("Name"+i);
            //savepassow = PlayerPrefs.GetString("Password"+i);
            NameInputField.text = saveName;
            password.text = savepassow;
            NameInputFieldd = NameInputField;
            passwordFielDD = password;
            Debug.Log("v**********************************************************************" +
                "***************************************************************************" +
                "******************************************************************************" +
                "************************************************************");
            if (NameInputField.text == saveName && password.text == savepassow)
            {
                Debug.Log("Success");
                incrt.enabled = false;
                StartCoroutine(fa());

            }
            else
            {
                incrt.enabled = true;
            }
        }
    }
    public void SetName()
    {
        DisbableText();
        backButton.interactable = true;
        okayButton.interactable = true;
        if (NameInputField.text.Length < 5)
        {
            notAvailaible.enabled = true;
        }
        else
        {
            notAvailaible.enabled = false;
            if (!loginInOrOut.isOn || logout==true) //signin
            {
                stop = false;
                record = pref.record;
                //record = PlayerPrefs.GetInt("Record");
                for (int i = 0; i < record; i++)
                {
                    saveName = pref.nane[i];
                    //saveName = PlayerPrefs.GetString("Name" + i);
                    if (NameInputField.text == saveName)
                    {
                        notavail.enabled = true;
                        stop = true;
               
                    }
                }
                if (stop == false)
                {
                    NameInputFieldd = NameInputField;
                    passwordFielDD = password;
                    notavail.enabled=false;
                    pref.nane.Add("");
                    pref.pasword.Add("");
                    pref.avg.Add(0);
                    pref.exp.Add(0);
                    pref.cubes.Add(0);
                    pref.notimes.Add(0);
                    pref.IUtimes.Add(0);
                    pref.location.Add("");
                    pref.Medal.Add("");
          
                    pref.nane[record] = NameInputField.text;
                    dataa.nane = NameInputField.text;
                    pref.pasword[record] = password.text;
                    dataa.pasword = password.text;
                    pref.location[record] = countri;
                    pref.Medal[record] = "Bronze";
                    createj();
                    //PlayerPrefs.SetString("Name" + record, NameInputField.text);
                    //PlayerPrefs.SetString("Password" + record, password.text);
                    Debug.Log("stored" + record);
                    record += 1;
                    pref.record = record;
                    PlayerPrefs.SetInt("LastAccess", record);
                    //PlayerPrefs.SetInt("Record", record);
                    // You can also set this to false, if omitted it defaults to false
                    StartCoroutine(faa());
                }
            }
            if (loginInOrOut.isOn) //login
            {
                updat = true;
                record = pref.record;
                //record = PlayerPrefs.GetInt("Record");
                Debug.Log(record); 
                for (int i = 0; i < record; i++)
                {
                    saveName = pref.nane[i];
                    savepassow = pref.pasword[i];
                    //saveName = PlayerPrefs.GetString("Name"+i);
                    //savepassow = PlayerPrefs.GetString("Password"+i);
                    if (NameInputField.text == saveName && password.text == savepassow)
                    {
                        NameInputFieldd = NameInputField;
                        passwordFielDD = password;
                        Debug.Log("Success");
                        incrt.enabled = false;
                        StartCoroutine(fa());
                        PlayerPrefs.SetInt("LastAccess",i+1);
                        break;
                    }
                    else
                    {
                        incrt.enabled = true;
                    }
                }
                
            }
        }
       
    }
    public void userToggle(bool tog)
    {
        remeber = tog;
        PlayerPrefs.SetInt("Remember",remeber?1:0);
        Debug.Log(tog);
    }
    IEnumerator fa()
    {
        uploadd();
        float g = GameObject.Find("Fade").GetComponent<fade>().BeginFade(1);
        yield return new WaitForSeconds(g);
        SceneManager.LoadScene(2);
    }
    IEnumerator faa()
    {
        uploadd();
        captcha.SetActive(true);
        yield return new WaitForSeconds(.01f);
    }
    public void write()
    {
        path = Application.persistentDataPath + "/PlayerP.json";
        File.WriteAllText(path, JsonUtility.ToJson(pref));
    }
    public void read()
    {
        path = Application.persistentDataPath + "/PlayerP.json";
        Debug.Log("boom");
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            pref = JsonUtility.FromJson<prefs>(content);
            remeber = PlayerPrefs.GetInt("Remember") == 1 ? true : false;
            if (remeber == true)
            {
                AIcheck();
            }
            rode = true;
            Debug.Log("Rode");
        }
        else
        {
            //temp= "{\"nane\":[\"Meshak\"],\"pasword\":[\"12345\"],\"cubes\":[0],\"notimes\":[0],\"avg\":[0],\"exp\":[0],\"record\":1}";
            //File.WriteAllText(path,temp); //THis will create new json file
        }
    }
    public void stats()
    {
        fe.retclk();
        Debug.Log("Clicked");
        if (ccube.activeSelf)
        {
            ccube.SetActive(false);
        }
        else
        {
            record = pref.record;
            //record = PlayerPrefs.GetInt("Record");
            for (int i = 0; i < record; i++)
            {
                Debug.Log("Clicked");
                saveName = pref.nane[i];
                savepassow = pref.pasword[i];
                //saveName = PlayerPrefs.GetString("Name" + i);
                //savepassow = PlayerPrefs.GetString("Password" + i);
                if (NameInputField.text == saveName && password.text == savepassow)
                {
                    retrieves();
                    Debug.Log("Success");
                    incrt.enabled = false;
                    ccube.SetActive(true);
                    cscore.text = pref.cubes[i].ToString();
                    notimes.text =pref.notimes[i].ToString();
                    likeT.text = dataa.like.Count.ToString();
                    avg.text = pref.avg[i].ToString();
                    exp.text = pref.exp[i].ToString();
                    level.text = (pref.cubes[i] / 10).ToString();
                    break;
                }
                else
                {
                    incrt.enabled = true;
                }
            }
        }
    }

    public void yes()
    {
        ccube.SetActive(false);
        notify = true;
        loginInOrOut.isOn = false;
        loginInOrOut.interactable = false;
        loaded_Name.text = "";
        notipan.SetActive(false);
        logou.SetActive(false);
        heading.text = "Signup - Form";
        incrt.enabled = false;
    }
    public void no()
    {
        notify = false;
        loginInOrOut.isOn = true;
        updat = true;
        notipan.SetActive(false);
        logou.SetActive(true);
        heading.text = "Login - Form";
    }
    public void logoutclk()
    {
        logout = true;
        UI1.SetActive(true);
        UI2.SetActive(false);
        PlayerPrefs.SetInt("Remember", 0);
        notipan.SetActive(true);
        logou.SetActive(false);
    }
    IEnumerator upwait()
    {
        yield return new WaitForSeconds(1);
        //upload();
    }
    IEnumerator Dest(GameObject t)
    {
        yield return new WaitForSeconds(.35f);
        Destroy(t);
    }
    IEnumerator loading()
    {
        UnityWebRequest req = new UnityWebRequest("www.google.com");
        yield return req.SendWebRequest();
        if (req.error != null)
        {
            img.SetActive(true);
            li.SetActive(false);
            butto.SetActive(true);
            text.SetActive(true);
        }
        else
        {
            img.SetActive(false);
        }
    }
    public void tryagain()
    {
        SceneManager.LoadScene(1);
    }
    public void vieww()
    {
        view++;
        if (view == 2)
        {
            Color a = passtext.color;
            a.a = 0.0f;
            passhide.enabled = true;
            passtext.color = a;
            view = 0;
        }else if (view == 1)
        {
            Color a = passtext.color;
            a.a = 100f;
            passhide.enabled = false;
            passtext.color = a;
        }
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
                LOCATION.text = countr.country.ToString();
                countri = countr.country;
                
            }
        }           
    }
    IEnumerator waii()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("sadsada");
        retj();
    }
    public void OnDash()
    {
        for (int i = 0; i < pref.record; i++)
        {
            saveName = pref.nane[i];
            savepassow = pref.pasword[i];
            if (NameInputField.text == saveName && password.text == savepassow)
            {
                retrieves();
                waii();
                retj();
                statbuttons();
                frndbuttons();
                dashpanel.SetActive(true);
                List<int> tmp = new List<int>(pref.cubes);
                oder = tmp;
                oder.Sort();
                oder.Reverse();
                xt = 0;
                for (int j = staddress; j <= enaddress; j++)
                {
                    Sno[xt].text = (j + 1).ToString();
                    Name[xt].text = pref.nane[pref.cubes.IndexOf(oder[j])].ToString();
                    medal[xt].text = pref.Medal[pref.cubes.IndexOf(oder[j])].ToString();
                    xt++;
                }
            }
        }
       
    }
    public void OffDash()
    {
        dashpanel.SetActive(false);
    }
    public void nextpage()
    {
        if ((enaddress+5) < oder.Count)
        {
            staddress += 5;
            enaddress += 5;
            xt = 0;
            for (int j = staddress; j <= enaddress; j++)
            {
                Sno[xt].text = (j + 1).ToString();
                Name[xt].text = pref.nane[pref.cubes.IndexOf(oder[j])].ToString();
                medal[xt].text = pref.Medal[pref.cubes.IndexOf(oder[j])].ToString();
                xt++;
            }
        }
    }
    public void previouspage()
    {
        if ((staddress - 5) >= 0)
        {
            staddress -= 5;
            enaddress -= 5;
            xt = 0;
            for (int j = staddress; j <= enaddress; j++)
            {
                Sno[xt].text = (j + 1).ToString();
                Name[xt].text = pref.nane[pref.cubes.IndexOf(oder[j])].ToString();
                medal[xt].text = pref.Medal[pref.cubes.IndexOf(oder[j])].ToString();
                xt++;
            }
        }
    }
    public void frndbuttons()
    {
        frnd[0].onClick.AddListener(delegate
        {
            if (dataa.friends.Contains(Name[0].text) || Name[0].text == NameInputField.ToString())
            {

            }
            else
            {
                dataa.friends.Add("");
                dataa.friends[dataa.friends.Count - 1] = Name[0].text;
                createj();
            }
        });
        frnd[1].onClick.AddListener(delegate
        {
            if (dataa.friends.Contains(Name[1].text) || Name[1].text == NameInputField.text)
            {

            }
            else
            {
                dataa.friends.Add("");
                dataa.friends[dataa.friends.Count - 1] = Name[1].text;
                createj();
            }
        });
        frnd[2].onClick.AddListener(delegate
        {
            if (dataa.friends.Contains(Name[2].text) || Name[2].text == NameInputField.text)
            {

            }
            else
            {
                dataa.friends.Add("");
                dataa.friends[dataa.friends.Count - 1] = Name[2].text;
                createj();
            }
        });
        frnd[3].onClick.AddListener(delegate
        {
        if (dataa.friends.Contains(Name[3].text) || Name[3].text == NameInputField.text)
        {

        }
        else
        {
            dataa.friends.Add("");
            dataa.friends[dataa.friends.Count - 1] = Name[3].text;
                createj();
            }
        });
        frnd[4].onClick.AddListener(delegate
        {
            if (dataa.friends.Contains(Name[4].text) || Name[4].text== NameInputField.text)
            {
                Debug.Log("IF");
            }
            else
            {
                Debug.Log("ELSE");
                dataa.friends.Add("");
                dataa.friends[dataa.friends.Count - 1] = Name[4].text;
                createj();
            }
        });
    }
    public void statbuttons()
    {
        sta[0].onClick.AddListener(delegate
        {
            rawimg.SetActive(false);
            upbutton.SetActive(false);
            Debug.Log("Clicked");
            if (ccube.activeSelf)
            {
                ccube.SetActive(false);
            }
            else
            {
                Debug.Log("Success");
                incrt.enabled = false;
                ccube.SetActive(true);
                cscore.text = pref.cubes[pref.cubes.IndexOf(oder[staddress+0])].ToString();
                notimes.text = pref.notimes[pref.cubes.IndexOf(oder[staddress + 0])].ToString();
                avg.text = pref.avg[pref.cubes.IndexOf(oder[staddress + 0])].ToString();
                exp.text = pref.exp[pref.cubes.IndexOf(oder[staddress + 0])].ToString();
                level.text = (pref.cubes[pref.cubes.IndexOf(oder[staddress + 0])] / 10).ToString();
            }
        });
        sta[1].onClick.AddListener(delegate
        {
            rawimg.SetActive(false);
            upbutton.SetActive(false);
            Debug.Log("Clicked");
            if (ccube.activeSelf)
            {
                ccube.SetActive(false);
            }
            else
            {
                Debug.Log("Success");
                incrt.enabled = false;
                ccube.SetActive(true);
                cscore.text = pref.cubes[pref.cubes.IndexOf(oder[staddress +1])].ToString();
                notimes.text = pref.notimes[pref.cubes.IndexOf(oder[staddress + 1])].ToString();
                avg.text = pref.avg[pref.cubes.IndexOf(oder[staddress + 1])].ToString();
                exp.text = pref.exp[pref.cubes.IndexOf(oder[staddress + 1])].ToString();
                level.text = (pref.cubes[pref.cubes.IndexOf(oder[staddress + 1])] / 10).ToString();
            }
        });
        sta[2].onClick.AddListener(delegate
        {
            rawimg.SetActive(false);
            upbutton.SetActive(false);
            Debug.Log("Clicked");
            if (ccube.activeSelf)
            {
                ccube.SetActive(false);
            }
            else
            {
                Debug.Log("Success");
                incrt.enabled = false;
                ccube.SetActive(true);
                cscore.text = pref.cubes[pref.cubes.IndexOf(oder[staddress + 2])].ToString();
                notimes.text = pref.notimes[pref.cubes.IndexOf(oder[staddress + 2])].ToString();
                avg.text = pref.avg[pref.cubes.IndexOf(oder[staddress + 2])].ToString();
                exp.text = pref.exp[pref.cubes.IndexOf(oder[staddress + 2])].ToString();
                level.text = (pref.cubes[pref.cubes.IndexOf(oder[staddress + 2])] / 10).ToString();
            }
        });
        sta[3].onClick.AddListener(delegate
        {
            rawimg.SetActive(false);
            upbutton.SetActive(false);
            Debug.Log("Clicked");
            if (ccube.activeSelf)
            {
                ccube.SetActive(false);
            }
            else
            {
                Debug.Log("Success");
                incrt.enabled = false;
                ccube.SetActive(true);
                cscore.text = pref.cubes[pref.cubes.IndexOf(oder[staddress + 3])].ToString();
                notimes.text = pref.notimes[pref.cubes.IndexOf(oder[staddress + 3])].ToString();
                avg.text = pref.avg[pref.cubes.IndexOf(oder[staddress + 3])].ToString();
                exp.text = pref.exp[pref.cubes.IndexOf(oder[staddress + 3])].ToString();
                level.text = (pref.cubes[pref.cubes.IndexOf(oder[staddress + 3])] / 10).ToString();
            }
        });
        sta[4].onClick.AddListener(delegate
        {
            rawimg.SetActive(false);
            upbutton.SetActive(false);
            Debug.Log("Clicked");
            if (ccube.activeSelf)
            {
                ccube.SetActive(false);
            }
            else
            {
                Debug.Log("Success");
                incrt.enabled = false;
                ccube.SetActive(true);
                cscore.text = pref.cubes[pref.cubes.IndexOf(oder[staddress + 4])].ToString();
                notimes.text = pref.notimes[pref.cubes.IndexOf(oder[staddress + 4])].ToString();
                avg.text = pref.avg[pref.cubes.IndexOf(oder[staddress + 4])].ToString();
                exp.text = pref.exp[pref.cubes.IndexOf(oder[staddress + 4])].ToString();
                level.text = (pref.cubes[pref.cubes.IndexOf(oder[staddress + 4])] / 10).ToString();
            }
        });
    }
    public void viewStatus(Text txt)
    {
        if (txt.text == "Public")
        {
            txt.text = "Private";
            dataa.privateS = true;
        }
        else
        {
            txt.text = "Public";
            dataa.privateS = false;
        }
        createj();
    }
    public void DeleteData()
    {
        menu.SetActive(false);
        NameInputField.text = "";
        passwordFielDD.text = "";
        string pat = Application.persistentDataPath + "/" + NameInputField.text + ".json";
        File.Delete(pat);
        sref = storage.RootReference.Child(NameInputField.text + ".json");
        sref.DeleteAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Data deleted successfully");
            }
            else
            {
                Debug.Log("Failed to delete data");
            }
        });
        int i= pref.nane.IndexOf(dataa.nane);
        pref.nane.RemoveAt(i);
        pref.IUtimes.RemoveAt(i);
        pref.exp.RemoveAt(i);
        pref.cubes.RemoveAt(i);
        pref.avg.RemoveAt(i);
        pref.location.RemoveAt(i);
        pref.Medal.RemoveAt(i);
        pref.notimes.RemoveAt(i);
        pref.record -= 1;
        uploadd();
    }
    public void RemoveDATa()
    {
        foreach (string s in dataa.followers)
        {
            StartCoroutine(databaseRemovel(s,0));
        }
        foreach (string s in dataa.following)
        {
           StartCoroutine(databaseRemovel(s,1));
        }
        DeleteData();
        clanMemberRemove();
    }
    IEnumerator databaseRemovel(string s, int i)   //i=0  followers remove                     
    {
        bool ret=false;//i=1 following remove
        strlist.Add(s);
        oname = s;

        sref = storage.RootReference.Child(oname + ".json");
        string pat = Application.persistentDataPath + "/" + oname + ".json";
        sref.GetFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                ret = true;
                Debug.Log("Sucesssss");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
        yield return new WaitUntil(()=>ret);

        string uname = Application.persistentDataPath + "/" + oname + ".json";
        string content = File.ReadAllText(uname);
        data tempd = JsonUtility.FromJson<data>(content);
        if (i == 0)
        {
            tempd.following.Remove(dataa.nane);
        }
        else
        {
            tempd.followers.Remove(dataa.nane);
        }
        sref = storage.RootReference.Child(oname + ".json");
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
        tempdlist.Add(tempd);
    }
    public void clanMemberRemove()
    {
        clanPanel.SetActive(true);
        clanPanel.GetComponent<Image>().enabled = false;
        foreach(Transform t in clanPanel.transform)
        {
            t.gameObject.SetActive(false);
        }
        StartCoroutine(Waitt());
    }
    IEnumerator Waitt()
    {
        clan.instance.retrieveclan();
        yield return new WaitForSeconds(2);
        clan.instance.read();
        for(int i=0; i< clan.instance.cd.clans.Count; i++)
        {
            if (clan.instance.cd.clans[i].clan == dataa.clan)
            {
                clan.instance.cd.clans[i].cfrndnames.Remove(dataa.nane);
            }
        }
        clan.instance.uploadclan();
    }

}