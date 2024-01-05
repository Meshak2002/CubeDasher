using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.IO;
using LootLocker.Requests;
using Firebase.Storage;

public class PlayerCollision : MonoBehaviour
{
    private admanager admanager;
    public GameObject continuePanel;
    public PlayerScript playerScript;
    public GameController gameController;
    public AudioSource audioSource;
    public int currentCube;
    public Text ScoreX;
    public GameObject player;
    public int zoffset;
    public GameObject Milestone;
    public int MilestoneNumber;
    public ParticleSystem MilestoneParticle;
    [SerializeField] bool isMagnet;
    [SerializeField] bool isSprint;
    public float magnetTime,f;
    public LayerMask collectableLayer;
    [SerializeField] int chance = 4;
    public Text countText;
    public float sprintTime = 5f;
    public Text scoreDisplayText;
    [Range(10, 25)]
    public int SprintPowerValue = 20;
    private bool repaet;

    public Text batery;
    public GameObject SprintUI;
    public GameObject MagnetUI,batteryUI;
    public Ease CoinStyle;
    Vector3 PlayerPosition;
    public GameObject gameOverPanel;
    public Camera cam1,cam2;
    private int k, h,t;
    public prefs pref;
    public data dataa;
    private string path;
    public string temp;
    FirebaseStorage storage;
    StorageReference sref;
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        path = Application.persistentDataPath + "/PlayerP.json";
        read();
        Time.timeScale = 1;

        //SprintUI.SetActive(false);
        //MagnetUI.SetActive(false);
        
        Debug.Log(PlayerPrefs.GetInt("PlayerCube").ToString());
        scoreDisplayText.text = PlayerPrefs.GetInt("PlayerCube").ToString();
        admanager = GameObject.FindGameObjectWithTag("ad").GetComponent<admanager>();
        continuePanel.SetActive(false);
        countText.gameObject.SetActive(false);

        for (int i = 0; i < pref.record; i++)
        {
            string savename = pref.nane[i];
            if (savename == Profile.NameInputFieldd.text)
            {
                Debug.Log(i + " " + Profile.NameInputFieldd.text);
               currentCube = pref.cubes[i];
            }
        }
    }
    IEnumerator Delay()
    {
        Milestone.SetActive(true);
        MilestoneParticle.Play();
        yield return new WaitForSeconds(2);
        Milestone.SetActive(false);
        MilestoneParticle.Stop();
    }
    void Update()
    {
        temp = Profile.NameInputFieldd.text;
        f = SystemInfo.batteryLevel * 100f;
        batery.text = (f+"%").ToString();
        if (f < 20f && repaet==false)
        {
            repaet = true;
            batteryUI.SetActive(true);
        }
        if (f > 60f)
        {
            repaet = false;
        }
        if (zoffset > 100)
        {
            t = (zoffset / 10)%10;
        }
        if (zoffset > 100)
        {
            h = zoffset/100;
        }
        if (zoffset > 1000)
        {
            k = zoffset/1000;
        }
        scoreDisplayText.text = currentCube.ToString();
        PlayerPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        zoffset = (int)player.transform.position.z;
        if (zoffset <= 100)
        {
            ScoreX.text = zoffset.ToString();
        }
        if (zoffset > 100 && zoffset<1000)
        {
            ScoreX.text = h.ToString()+"."+t.ToString()+"H";
        }
        if (zoffset >= 1000)
        {
            ScoreX.text = k.ToString()+"."+h.ToString()+"K";
        }

        
        if (zoffset % MilestoneNumber == 0)
        {
            StartCoroutine(Delay());
        }
        if (zoffset == 0)
        {
            MilestoneFalse();
        }
        if (isMagnet)
        {
            StartCoroutine(MagnetPower(magnetTime));
        }
        if (isSprint)
        {
            StartCoroutine(SprintPower(sprintTime));
        }

    }
    void MilestoneFalse()
    {
        Milestone.SetActive(false);
        MilestoneParticle.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectables")
        {
            Destroy(other.gameObject);
            audioSource.Play();
            currentCube++;
            scoreDisplayText.text = currentCube.ToString();


        }
        if (other.CompareTag("Magnet"))
        {
            Destroy(other.gameObject);
            isMagnet = true;
        }
        if (other.CompareTag("sprint"))
        {

            Destroy(other.gameObject);
            isSprint = true;
        }
    }
    IEnumerator MagnetPower(float t)
    {
        MagnetUI.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, collectableLayer);

        // Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        foreach (Collider c in colliders)
        {

            GameObject coin = c.gameObject;
            coin.GetComponent<Collider>().enabled = false;
            coin.transform.DOMove(PlayerPosition, 0.1f).SetEase(CoinStyle);
            yield return new WaitForSeconds(0.1f);
            //transform.position = Vector3.Lerp(targetPosition, c.transform.position, 5 * Time.deltaTime);
            if (c.isTrigger == true)
            {
                //Debug.Log("collect!");
                if (c.CompareTag("Collectables") && isMagnet == true)
                {
                    currentCube++;
                    PlayerPrefs.SetInt("XPAdded", PlayerPrefs.GetInt("XPAdded", 0) + 5);
                    // audioSource.Play();
                    Destroy(c.gameObject);
      //              CarrerManager.instance.AddXP(5);
                }
            }
        }
        yield return new WaitForSeconds(t);
        MagnetUI.SetActive(false);
        isMagnet = false;
    }

    IEnumerator SprintPower(float s)
    {
        playerScript.speed = SprintPowerValue;
        SprintUI.SetActive(true);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Obstacles") || c.CompareTag("SlightSystemTag") && isSprint == true)
            {
                c.isTrigger = true;
            }
        }
        yield return new WaitForSeconds(s);
        SprintUI.SetActive(false);

        playerScript.speed = 5;

        isSprint = false;
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
    public void dUpload(string name)
    {
        dwrite(name);
        sref = storage.RootReference.Child(name+".json");
        sref.PutFileAsync(Application.persistentDataPath+"/"+name+".json").ContinueWith(response =>
        {
            if (response.IsCompleted)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }
        public void write()
    {
        path = Application.persistentDataPath + "/PlayerP.json";
        File.WriteAllText(path, JsonUtility.ToJson(pref));
    }
    public void dwrite(string name)
    {
        File.WriteAllText(Application.persistentDataPath + "/" + name + ".json", JsonUtility.ToJson(dataa));
    }
    public void read()
    {
        path = Application.persistentDataPath + "/PlayerP.json";
        if (File.Exists(path))
        {
            string content = File.ReadAllText(path);
            pref = JsonUtility.FromJson<prefs>(content);
        }
        else
        {
           // string tmp = "{\"nane\":[\"Meshak\"],\"pasword\":[\"12345\"],\"cubes\":[0],\"notimes\":[0],\"avg\":[0],\"exp\":[0],\"record\":1}";
            //File.WriteAllText(path, ""); //THis will create new json file
        }
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacles")
        {
            Debug.Log("Fpoo + "+pref.record);
            for (int i = 0; i < pref.record; i++)
            {
                string savename = pref.nane[i];
                if (savename == Profile.NameInputFieldd.text)
                {
                    Debug.Log("Fk");
                    // int x = pref.cubes[i] + currentCube;
                    int x = currentCube;
                    int n = pref.notimes[i] + 1;
                    int avg = x / n;
                    int exp = 5 * x;
                    pref.cubes[i] = x;
                    pref.notimes[i] = n;
                    pref.avg[i] = avg;
                    pref.exp[i] = exp;
                    dataa.nane = savename;
                    dataa.pasword = Profile.passwordFielDD.text;
                    dataa.cubes = x;
                    dataa.notimes = n;
                    dataa.avg = avg;
                    dataa.exp = exp;
                    if (currentCube > 1000)
                    {
                        pref.Medal[i] = "Crystal";
                    }else if (currentCube > 500)
                    {
                        pref.Medal[i] = "Gold";
                    }else if (currentCube > 250)
                    {
                        pref.Medal[i] = "Silver";
                    }
                    else
                    {
                        pref.Medal[i] = "Bronze";
                    }
                    dUpload(Profile.NameInputFieldd.text);
                    uploadd();
                    //PlayerPrefs.SetInt("Cubes" +i, x);
                    //PlayerPrefs.SetInt("Notimes" + i, n);
                    //PlayerPrefs.SetInt("Avg" + i, avg);
                    //PlayerPrefs.SetInt("Exp" + i, exp);
                }
            }

            // audioSource1.Play();
            playerScript.enabled = false;
            const float duration = 0.5f;
            const float strength = 0.5f;
            if (cam1.gameObject.activeSelf==true)
            {
                cam1.DOShakePosition(duration, strength);
                cam1.DOShakeRotation(duration, strength);
                cam1.transform.DOShakeScale(duration, strength);
            }
            else
            {
                cam2.DOShakePosition(duration, strength);
                cam2.DOShakeRotation(duration, strength);
                cam2.transform.DOShakeScale(duration, strength);
            }
            if (chance > 0)
            {
                continuePanel.SetActive(true);
                chance -= 1;
            }
            else
            {
                dontrewardPlayer();
            }
            Destroy(other.gameObject);
        }
        if (other.collider.CompareTag("SlightSystemTag"))
        {
            const float duration = 0.5f;
            const float strength = 0.5f;
            Camera.main.DOShakePosition(duration, strength);
            Camera.main.DOShakeRotation(duration, strength);
            Camera.main.transform.DOShakeScale(duration, strength);
            Destroy(other.gameObject);
            gameOverPanel.SetActive(true);
            playerScript.enabled = false;
            Time.timeScale = 0.5f;
        }
    }
    public void rewardPlayer()
    {
        StartCoroutine(countDown());
    }
    public void dontrewardPlayer()
    {
        continuePanel.SetActive(false);
        gameController.GameOver();
    }
    IEnumerator countDown()
    {
        countText.gameObject.SetActive(true);
        countText.text = "3";
        yield return new WaitForSeconds(1);
        countText.text = "2";
        yield return new WaitForSeconds(1);
        countText.text = "1";
        yield return new WaitForSeconds(1);
        countText.gameObject.SetActive(false);
        continuePanel.SetActive(false);
        playerScript.enabled = true;
    }
    public void ok()
    {
        batteryUI.SetActive(false);
    }
}

