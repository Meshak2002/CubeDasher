using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using UnityEngine.UI;

public class chat : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField msgInput,privatmsgInput;
    public fireDatabaseapi rd;
    public GameObject message, clanview, globalview;
    public Transform container,cccontainer,pcontainer;
    public GameObject chatpanel, PrivateChatPanel;
    public Button global, cla, send;
    public Color bef, afclick;
    private bool repeat;
    [SerializeField] private string otherPersonName;
    private string privateMSGFormat;
    public static chat instance;
    private int prino,once;
    void Start()
    {
        instance = this;
        send.onClick.AddListener(delegate
        {
            sendmsg();
        });
        cla.onClick.AddListener(delegate
        {
            if (Profile.instance.dataa.clan != "")
            {
                Debug.Log(Profile.instance.dataa.clan);
                cla.GetComponent<Image>().color = afclick;
                global.GetComponent<Image>().color = bef;
                globalview.SetActive(false);
                clanview.SetActive(true);
                send.onClick.RemoveAllListeners();
                send.onClick.AddListener(delegate
                {
                    Csendmsg();
                });
            }
        });
        global.onClick.AddListener(delegate
        {
            cla.GetComponent<Image>().color = bef;
            global.GetComponent<Image>().color = afclick;
            globalview.SetActive(true);
            clanview.SetActive(false);
            send.onClick.RemoveAllListeners();
            send.onClick.AddListener(delegate
            {
                sendmsg();
            });
        });
    }
    public void privat_ChatBtnClk(Text name)
    {
        once += 1;
        PrivateChatPanel.SetActive(true);
        //Debug.Log("Retrieved");
        if (once == 1)
        {
            otherPersonName = name.text;
            privateMSGFormat = Profile.instance.dataa.nane + otherPersonName;
            rd.ListenForNewMessages(instantiatemsg);
        }
    }
    public void privat_SendBtnClk()
    {
        //initial value=0
        
        privatemsg(privateMSGFormat,prino);
        Debug.Log("asdaksc");
        privatmsgInput.text ="";
    }
        public void sendmsg() => rd.SendMesg(new Message(Profile.NameInputFieldd.text, msgInput.text, null, null,0), () => Debug.Log("GMessage Sent"));
    public void Csendmsg() => rd.SendMesg(new Message(Profile.NameInputFieldd.text, msgInput.text,Profile.instance.dataa.clan,null,0), () => Debug.Log("CMessage Sent"));
    public void privatemsg(string persons,int n)=> rd.SendMesg(new Message(Profile.NameInputFieldd.text, privatmsgInput.text, null, persons,n), () => Debug.Log("Private Message Sent"));
    void instantiatemsg(Message msg)
    {
        //Debug.Log(msg.text);
        if ((msg.clan == null || msg.clan=="")&&(msg.privat == null || msg.privat == ""))
        {
           //Debug.Log("Global");
            //Debug.Log("Message" + msg.text);
            GameObject newmsg = Instantiate(message, transform.position, Quaternion.identity);
            newmsg.transform.SetParent(container, false);
            newmsg.GetComponent<Text>().text = $"{msg.sender}: {msg.text}";
        }
        if(msg.clan== Profile.instance.dataa.clan && (msg.privat == null || msg.privat == ""))
        {
            //Debug.Log("clan");
            GameObject newmsg = Instantiate(message, transform.position, Quaternion.identity);
            newmsg.transform.SetParent(cccontainer, false);
            newmsg.GetComponent<Text>().text = $"{msg.sender}: {msg.text}";
        }

        if (msg.privat == (otherPersonName + Profile.instance.dataa.nane))
        {
            privateMSGFormat = otherPersonName + Profile.instance.dataa.nane;
        }
        if (msg.privat ==(Profile.instance.dataa.nane+otherPersonName) || msg.privat == (otherPersonName+Profile.instance.dataa.nane))
        {
            if (msg.text != null )
            {
                prino += 1;
                GameObject newmsg = Instantiate(message, transform.position, Quaternion.identity);
                newmsg.transform.name = msg.text;
                newmsg.transform.SetParent(pcontainer, false);
                newmsg.GetComponent<Text>().text = $"{msg.sender}: {msg.text}";
                if (msg.sender == otherPersonName)
                {
                    rd.SendMesg(new Message(Profile.NameInputFieldd.text, null, null, privateMSGFormat, prino), () => Debug.Log("seen"));
                    pcontainer.transform.GetChild(prino-1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        if(msg.text == null  && (msg.privat==privateMSGFormat  && msg.sender==otherPersonName))
        {
            pcontainer.transform.GetChild(msg.no-1).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
            

    }

    public void openchatpanel()
    {
        int record = Profile.instance.pref.record;
        string saveName, savepassow;
        for (int i = 0; i < record; i++)
        {
            Debug.Log("Clicked");
            saveName = Profile.instance.pref.nane[i];
            savepassow = Profile.instance.pref.pasword[i];
            if (Profile.instance.NameInputField.text == saveName && Profile.instance.password.text == savepassow)
            {
                chatpanel.SetActive(true);
                Debug.Log("Retrieved");
                Profile.instance.retrieves();
                Profile.instance.retj();
                if (repeat == false)
                {
                    rd.ListenForNewMessages(instantiatemsg);
                    repeat=true;
                }
            }
        }
        
    }
    public void closechatpanel()
    {
        chatpanel.SetActive(false);
        send.onClick.RemoveAllListeners();
        send.onClick.AddListener(delegate
        {
            sendmsg();
        });
    }
}
