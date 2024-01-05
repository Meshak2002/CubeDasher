using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using LootLocker;
using LootLocker.Requests;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string current, old;
    private DateTime d;
    public Text t;

    void Start()
    {
        StartCoroutine(Login());
        current = System.DateTime.UtcNow.ToLocalTime().ToString();
        d = System.DateTime.UtcNow.ToLocalTime();
    }

    // Update is called once per frame
    void Update()
    {
        //savs();
    }
    void savs()
    {
        // You can also set this to false, if omitted it defaults to false
        bool isPublic = true;

        LootLockerSDKManager.UploadPlayerFile("/path/to/file/save_game.zip", "save_game", isPublic, response =>
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
    IEnumerator Login()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    }
