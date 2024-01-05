﻿using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using ricRM.Time;

public class RewardActive : MonoBehaviour {

    int sec = 0, min = 0, hour = 0, days = 0;
    int money = 0;

    public GameObject TimeReward;
    public Text Money;
    public Text time;

    ActiveTimeReward timeR;

    void Start()
    {
        timeR = new ActiveTimeReward("active", 0, 15);
        timeR.AddCallBack((index) => {
            TimeReward.SetActive(true);
            Debug.Log("Congratz!");
        });

        money = PlayerPrefs.GetInt("money", 0);

        StartCoroutine(Time());
        timeR.Start();
        StartCoroutine(timeR.Run());
    }

    void Update()
    {
        time.text = days.ToString() + ":" + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
    }

    public void ButtonTime()
    {
        TimeReward.SetActive(false);
        this.money += 50;
        Money.text = "You have: " + this.money.ToString() + "$";
        PlayerPrefs.SetInt("money", this.money);
    }

    IEnumerator Time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            sec++;
            if (sec == 60)
            {
                sec = 0;
                min++;
                if (min == 60)
                {
                    min = 0;
                    hour++;
                    if (hour == 24)
                    {
                        days++;
                    }
                }
            }
        }
    }
}
