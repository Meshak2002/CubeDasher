using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using System.IO;
using TMPro;
using TMPro.EditorUtilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using System;

public class clan : MonoBehaviour
{
    // Start is called before the first frame update
    public Button myclan,create,Fcreate;
    public Button searchclans,lev,srchbutton,close;
    public GameObject srchpanel, clnpanel,crpanel,wrngtxt,wrngsrchtxt,frprefab,content,ourclan,memprefab,content2,availableclan,clanpanel,notipanel;
    public Text clan_name;
    public InputField crclanname,srchinput;
    FirebaseStorage storage;
    StorageReference sref;
    public Color c,s;
    public clandata cd;
    private string path;
    public frnds f;
    public data d,other;
    public List<string> selfrnds;
    int index;
    int index2;
    public static clan instance;
    public Text cubes;
    bool yes;
    public TMP_Dropdown dropd;
    bans b;
    string secondss;
    private void Awake()
    {
       
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        storage = FirebaseStorage.DefaultInstance;
        path = Application.persistentDataPath + "/Clans_.json";
        retrieveclan();
        read();
        if (this.gameObject.activeSelf)
        {
            retrievedata(Profile.NameInputFieldd.text);
            read_d();
            for(int i = 0; i < d.friends.Count; i++)
            {
                retrievedata(d.friends[i]);
            }
            if (d.clan != "")
            {
                ourclan.SetActive(true);
                cubes.text = d.cubes.ToString();
                wrngtxt.SetActive(false);
                create.gameObject.SetActive(false);
                clan_name.text = d.clan.ToString();
                for(int j = 0; j < cd.clans.Count; j++)
                {
                    if (d.clan == cd.clans[j].clan)
                    {
                        index = j;
                    }
                }
                for (int i = 0; i < cd.clans[index].cfrndnames.Count; i++)
                {
                    GameObject tmp;
                    tmp = Instantiate(memprefab, content2.transform);
                    tmp.transform.GetChild(0).GetComponent<Text>().text = cd.clans[index].cfrndnames[i];
                    if (cd.clans[index].admin == cd.clans[index].cfrndnames[i])
                    {
                        tmp.transform.GetChild(2).GetComponent<Text>().text = "Leader";
                    }

                    tmp.name = cd.clans[index].cfrndnames[i];
                    if (d.nane== cd.clans[index].admin && tmp.name!=d.nane)
                    {
                        tmp.transform.GetChild(3).gameObject.SetActive(true);
                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(()=>Kick(tmp.transform));
                    }
                    Debug.Log(i);
                }
                //this.gameObject.transform.Find("PhotonConnect").GetComponent<photonConnect>().enabled = true;
            }
        }
        myclan.onClick.AddListener(delegate
        {
            clnpanel.SetActive(true);
            srchpanel.SetActive(false);
            myclan.gameObject.GetComponent<Image>().color = c;
            searchclans.gameObject.GetComponent<Image>().color = s;
        });
        searchclans.onClick.AddListener(delegate
        {
            clnpanel.SetActive(false);
            srchpanel.SetActive(true);
            myclan.gameObject.GetComponent<Image>().color = s;
            searchclans.gameObject.GetComponent<Image>().color = c;
        });
        create.onClick.AddListener(delegate         
        {
            Debug.Log("Create");
            read_d();
            frnds();
            crpanel.SetActive(true);                
            wrngtxt.SetActive(false);               
            create.gameObject.SetActive(false);
        });
        Fcreate.onClick.AddListener(delegate
        {
            if (crclanname.text.Length > 4)
            {
                for(int i = 0; i < cd.clans.Count; i++)
                {
                    if (cd.clans[i].clan == srchinput.text)
                    {

                    }
                }
                Debug.Log(crclanname.text);
                f.clan = crclanname.text;
                selfrnds.Add(Profile.NameInputFieldd.text);
                f.cfrndnames = selfrnds;
                f.admin = Profile.NameInputFieldd.text;
                cd.clans.Add(f);
               
                d.clan= crclanname.text;
                
                crpanel.SetActive(false);
                ourclan.SetActive(true);
                cubes.text = d.cubes.ToString();
                wrngtxt.SetActive(false);
                create.gameObject.SetActive(false);
                clan_name.text = d.clan.ToString();
                for (int j = 0; j < cd.clans.Count; j++)
                {
                    if (d.clan == cd.clans[j].clan)
                    {
                        index = j;
                    }
                }
                for (int i = 0; i < cd.clans[index].cfrndnames.Count; i++)
                {
                    GameObject tmp;
                    tmp = Instantiate(memprefab, content2.transform);
                    tmp.transform.GetChild(0).GetComponent<Text>().text = cd.clans[index].cfrndnames[i];
                    if (cd.clans[index].admin == cd.clans[index].cfrndnames[i])
                    {
                        tmp.transform.GetChild(2).GetComponent<Text>().text = "Leader";
                    }
                    tmp.name = cd.clans[index].cfrndnames[i];
                    if (d.nane == cd.clans[index].admin && tmp.name != d.nane)
                    {
                        tmp.transform.GetChild(3).gameObject.SetActive(true);
                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Kick(tmp.transform));
                    }
                    read_od(cd.clans[index].cfrndnames[i]);
                    other.clan = crclanname.text;
                    uploaddata(cd.clans[index].cfrndnames[i], 0);
                    Debug.Log(i);
                }
                uploadclan();
                uploaddata(Profile.NameInputFieldd.text, 1);
            }
        });
        lev.onClick.AddListener(delegate
        {
            for (int i = 0; i < content2.transform.childCount; i++)
            {
                Destroy(content2.transform.GetChild(i).gameObject);
            }
            selfrnds.Remove(Profile.NameInputFieldd.text);
            cd.clans[index].cfrndnames.Remove(Profile.NameInputFieldd.text);
            ourclan.SetActive(false);
            wrngtxt.SetActive(true);
            create.gameObject.SetActive(true);
            d.clan = "";
            uploaddata(Profile.NameInputFieldd.text,1);
            uploadclan();
        });
        
        srchbutton.onClick.AddListener(delegate
        {
            wrngsrchtxt.SetActive(true);
            if (d.clan == "" || d.clan==null) {
                if (srchinput.text.Length > 4)
                {
                    for (int i = 0; i < cd.clans.Count; i++)
                    {
                        if (srchinput.text == cd.clans[i].clan)
                        {
                            availableclan.SetActive(true);
                            wrngsrchtxt.SetActive(false);
                            availableclan.transform.GetChild(0).gameObject.GetComponent<Text>().text = srchinput.text;
                            availableclan.transform.GetChild(1).gameObject.GetComponent<Text>().text = cd.clans[i].cfrndnames.Count.ToString();
                            index2 = i;
                            Debug.Log(index2);
                            availableclan.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(delegate
                            {
                                //JOIN
                                DateTime dt = DateTime.Now;
                                if (cd.clans[index2].kicks.Exists(ban => ban.nam == Profile.NameInputFieldd.text))
                                {
                                    bans ba = cd.clans[index2].kicks.Find(ban => ban.nam == Profile.NameInputFieldd.text);
                                    DateTime pre = DateTime.Parse(ba.duration);
                                    if (dt <= pre)
                                    {
                                        Debug.Log("Nope");
                                        return;
                                    }
                                    else
                                    {
                                        cd.clans[index2].kicks.Remove(ba);
                                    }
                                }
                                Debug.Log("Cgk1");
                                cd.clans[index2].cfrndnames.Add(Profile.NameInputFieldd.text);
                                d.clan = cd.clans[index2].clan;
                                clnpanel.SetActive(true);
                                srchpanel.SetActive(false);
                                myclan.gameObject.GetComponent<Image>().color = c;
                                searchclans.gameObject.GetComponent<Image>().color = s;
                                ourclan.SetActive(true);
                                wrngtxt.SetActive(false);
                                crpanel.SetActive(false);
                                create.gameObject.SetActive(false);
                                clan_name.text = d.clan.ToString();
                                Debug.Log("Cgk2");
                                for (int k = 0; k < cd.clans[index2].cfrndnames.Count; k++)
                                {
                                    Debug.Log(cd.clans[index2].cfrndnames.Count);
                                    GameObject tmp;
                                    tmp = Instantiate(memprefab, content2.transform);
                                    tmp.transform.GetChild(0).GetComponent<Text>().text = cd.clans[index2].cfrndnames[k];
                                    if (cd.clans[index2].admin == cd.clans[index2].cfrndnames[k])
                                    {
                                        tmp.transform.GetChild(2).GetComponent<Text>().text = "Leader";
                                    }
                                    tmp.name = cd.clans[index2].cfrndnames[k];
                                    if (d.nane == cd.clans[index].admin && tmp.name != d.nane)
                                    {
                                        tmp.transform.GetChild(3).gameObject.SetActive(true);
                                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                                        tmp.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => Kick(tmp.transform));
                                    }
                                    Debug.Log(k);
                                }
                                Debug.Log("Cgk3");
                                uploaddata(Profile.NameInputFieldd.text, 1);
                                uploadclan();
                            });
                            break;
                        }
                    }
                }
            }
            
        });
        close.onClick.AddListener(delegate
        {
            clanpanel.SetActive(false);
        });
    }
    public void writedata(string name,int x)
    {
        string uname = Application.persistentDataPath + "/" + name + ".json";
        if(x==1)    File.WriteAllText(uname, JsonUtility.ToJson(d));
        else if(x==0) File.WriteAllText(uname, JsonUtility.ToJson(other));
    }
        public void uploaddata(string name,int d)
    {
        writedata(name,d);
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" + name + ".json";
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
    public void retrievedata(string name)
    {
        sref = storage.RootReference.Child(name + ".json");
        string pat = Application.persistentDataPath + "/" +name+ ".json";
        sref.GetFileAsync(pat).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                yes = true;
                Debug.Log("Sucesssss");
                read_d();
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
    public void uploadclan()
    {
        write();
        sref = storage.RootReference.Child("Clans_");
        sref.PutFileAsync(path).ContinueWith(response =>
        {
            if (response.IsCompleted)
            {
                Debug.Log("Clan updated");
            }
            else
            {
                Debug.Log("Clan Updation Failed");
            }
        });
    }
    public void retrieveclan()
    {
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
    public void read_d()
    {
        string uname = Application.persistentDataPath + "/" + Profile.NameInputFieldd.text + ".json";
        string content = File.ReadAllText(uname);
        d = JsonUtility.FromJson<data>(content);
        Debug.Log("I had rode ");
        
    }
    public void read_od(string name)
    {
        string uname = Application.persistentDataPath + "/" +name + ".json";
        string content = File.ReadAllText(uname);
        other = JsonUtility.FromJson<data>(content);
      
    }

    public void write()
    {
        File.WriteAllText(path, JsonUtility.ToJson(cd));
    }
    public void read()
    {
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            cd = JsonUtility.FromJson<clandata>(content);
        }
    }
    // Update is called once per frame
   public void Kick(Transform parent)
    {
        notipanel.SetActive(true);
        notipanel.transform.GetChild(0).gameObject.SetActive(false);
        notipanel.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("Kicked");
        notipanel.transform.GetChild(1).GetChild(2).GetComponent<Button>().onClick.AddListener(delegate {
            string selectedOption = dropd.options[dropd.value].text;
             secondss = ConvertDropdownToSeconds(selectedOption);
            for (int i = 0; i < cd.clans.Count; i++)
            {
                if (cd.clans[i].clan == clan_name.text)
                {
                    cd.clans[i].cfrndnames.Remove(parent.name);
                    b = new bans();
                    b.nam = parent.name;
                    b.duration = secondss;
                    cd.clans[i].kicks.Add(b);
                }
            }
            
            notipanel.SetActive(false);
            Destroy(parent.gameObject);
            retrievedata(parent.name);
            StartCoroutine(waitt(parent.name));
            uploadclan();
            selfrnds.Remove(parent.name);
        });
    }
    string ConvertDropdownToSeconds(string option)
    {
        DateTime currentTime = DateTime.Now;
        switch (option)
        {
            case "1 day":
                currentTime = currentTime.AddDays(1);
                return currentTime.ToString();
            case "1 week":
                currentTime = currentTime.AddDays(7);
                return currentTime.ToString();
            case "1 month":
                currentTime = currentTime.AddMonths(1);
                return currentTime.ToString();
            case "6 months":
                currentTime = currentTime.AddMonths(6);
                return currentTime.ToString();
            case "1 year":
                currentTime = currentTime.AddYears(1);
                return currentTime.ToString();
            case "Permanent":
                return "Permanent";
            default:
                return "0"; // Return 0 if the option is not recognized or invalid

        }
    }

    IEnumerator waitt(string nam)
    {
        yield return new WaitUntil(() => yes == true);
        yes = false;
        read_od(nam);
        other.clan = null;
        other.notification = notipanel.transform.GetChild(1).GetChild(1).GetComponent<TMP_InputField>().text + "| Banned Till "+secondss;
        uploaddata(nam, 0);
    }
    public void frnds()
    {
        for(int i=0;i<d.friends.Count;i++)
        {
            retrievedata(d.friends[i]);
            GameObject tmp;
            tmp = Instantiate(frprefab, content.transform);
            tmp.transform.GetChild(0).GetComponent<Text>().text = d.friends[i];
            tmp.name = d.friends[i];
            Debug.Log("f");
           
        }

    }
}
