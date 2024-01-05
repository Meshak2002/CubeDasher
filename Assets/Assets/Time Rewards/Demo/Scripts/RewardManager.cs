using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using ricRM.Time;

public class RewardManager : MonoBehaviour {
    
    int sec = 0, min = 0, hour = 0, days = 0;
    int money = 0;

    public GameObject TimeReward;
    public GameObject DailyRewardG;
    public GameObject[] buttonsDaily;
    public Text Money;
    public Text time;

    DailyReward timeR;

	void Start () {
        timeR = new DailyReward("daily", 6);
        timeR.AddCallBack((dia) => {
            DailyRewardG.SetActive(true);
            buttonsDaily[dia - 1].SetActive(true);
            Debug.Log("Congratz!");
        });

        money = PlayerPrefs.GetInt("money", 0);

        StartCoroutine(Time());
        timeR.Check();
    }
	
	void Update () {
        time.text = days.ToString() + ":" + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
	}

    public void ButtonTime(int money)
    {
        DailyRewardG.SetActive(false);
        for (int i = 0; i < buttonsDaily.Length; i++)
        {
            buttonsDaily[i].SetActive(false);
        }
        this.money += money;
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
