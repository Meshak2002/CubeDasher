using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementManager : MonoBehaviour
{
    public Button[] Price;
    public int score = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Price.Length; i++)
        {
            Price[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Score_N", 0) >=1000)
        {
            Price[0].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 1500)
        {
            Price[1].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 2000)
        {
            Price[2].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 2500)
        {
            Price[3].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 3000)
        {
            Price[4].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 3500)
        {
            Price[5].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 4000)
        {
            Price[6].interactable = true;
        }
        if (PlayerPrefs.GetInt("Score_N", 0) >= 4500)
        {
            Price[7].interactable = true;
        }
       
       
    }
    public void IncreaseScore()
    {
        PlayerPrefs.SetInt("Score_N", PlayerPrefs.GetInt("Score_N", 0) + 50);
        score = PlayerPrefs.GetInt("Score_N", 0);
    }
    
}
