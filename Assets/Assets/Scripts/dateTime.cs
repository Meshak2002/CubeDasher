using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker;
using LootLocker.Requests;

public class dateTime : MonoBehaviour
{
    public Text txt_timer;
    public GameObject RewardButton;
    
    void Start()
    {
        StartCoroutine(CheckTimer());
    }
    
    public void GetReward()
    {
        System.DateTime CurrentTime = System.DateTime.Now.AddMinutes(1440);
        CurrentTime.SaveDate();
        StopAllCoroutines();
        StartCoroutine(CheckTimer());
        LootLockerSDKManager.UploadPlayerFile("/path/to/file/save_game.zip", "save_game", response =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded player file, url: " + response.url);
            }
            else
            {
                Debug.Log("Error uploading player file");
            }
        });
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public IEnumerator CheckTimer()
    {
        System.DateTime SavedTime = DateTimeExtension.GetSavedDate();
        System.TimeSpan RemaningTime = SavedTime.Subtract(System.DateTime.Now);
        if (RemaningTime.TotalMinutes > 0)
        {
            RewardButton.SetActive(false);
        }
        else
        {
            RewardButton.SetActive(true);
            txt_timer.text = "Collect Reward";
        }
        while (RemaningTime.TotalMinutes > 0)
        {
            RemaningTime = SavedTime.Subtract(System.DateTime.Now);
            txt_timer.text = "Wait For Reward " + RemaningTime.Hours + ":" + RemaningTime.Minutes + ":" + RemaningTime.Seconds;
            yield return new WaitForSeconds(1f);
        }
        if (RemaningTime.TotalMinutes <= 0)
        {
            RewardButton.SetActive(true);
            txt_timer.text = "Collect Reward";
        }
        Debug.Log("StopCoroutine");
    }

}

public static class DateTimeExtension
{
    public static void SaveDate(this System.DateTime _date, string Key = "SavedDate") { string d = System.Convert.ToString(_date); PlayerPrefs.SetString(Key, d); }
    public static System.DateTime GetSavedDate(string key = "SavedDate") { if (PlayerPrefs.HasKey("SavedDate")) { string d = PlayerPrefs.GetString("SavedDate"); return System.Convert.ToDateTime(d); } else { return System.DateTime.Now; } }
}

