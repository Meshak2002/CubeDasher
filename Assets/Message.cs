using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message
{
    public string text;
    public string sender;
    public string clan;
    public string privat;
    public int no;
    public Message(string sen,string txt,string cla,string priv,int mno)
    {
        text = txt; ;
        sender=sen;
        clan = cla;
        privat = priv;
        no = mno;
    }
}
