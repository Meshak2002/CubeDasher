using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarrerManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPText, targetXPText, levelText;
    public int currentXP, targetXP, level;

    public static CarrerManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        currentXPText.text = currentXP.ToString();
        targetXPText.text = targetXP.ToString();
        levelText.text = level.ToString();
    }
    public void AddXP(int xp)
    {
        currentXP += xp;
        PlayerPrefs.SetInt("XPAdded", PlayerPrefs.GetInt("XPAdded", 0) + xp);


        while (currentXP >= targetXP)
        {
            currentXP = currentXP - targetXP;
            level++;
            targetXP += targetXP / 20;

            levelText.text = level.ToString();
            targetXPText.text = targetXP.ToString();
        }

        currentXPText.text = currentXP.ToString();
    }
}
