using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.UI;

public class ProfanityFilter : MonoBehaviour
{
    public Text displayText;
    public Text inputText;

    public TextAsset textAssetBlockList;
    [SerializeField] string[] strBlockList;

    void Start()
    {
        strBlockList = textAssetBlockList.text.Split(new string[] { ",", "\n" }, StringSplitOptions.RemoveEmptyEntries);
    }
    public void CheckInput()
    {
        displayText.text = ProfanityCheck(inputText.text);
        inputText.text = "";
    }
    string ProfanityCheck(string strToCheck)
    {
        for (int i = 0; i < strBlockList.Length; i++)
        {
            string profanity = strBlockList[i];
            Debug.Log("profanity: " + profanity.Substring(1, profanity.Length - 1));
            if(strToCheck.IndexOf(profanity, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                return "****";
            }
        }
        return strToCheck;
    }   
    public void empty()
    {

    }
}
