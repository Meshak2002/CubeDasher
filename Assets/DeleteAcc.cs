using UnityEngine;
using UnityEngine.UI;
using System;

public class DeleteAcc : MonoBehaviour
{
    private const string LastCallTimeKey = "LastCallTime";
    private const float DebugDelay = 604800.0f; // 7 Weeks in seconds

    private DateTime lastCallTime;
    private bool isTimerRunning = false;

    public Text timerText;
    public Button startButton;
    public GameObject warning,del,recove;

    private void Start()
    {
        startButton.onClick.AddListener(StartTimer);

        // Retrieve the last call time from PlayerPrefs
        if (PlayerPrefs.HasKey(LastCallTimeKey))
        {
            del.SetActive(false);
            recove.SetActive(true);
            string lastCallTimeString = PlayerPrefs.GetString(LastCallTimeKey);
            lastCallTime = DateTime.Parse(lastCallTimeString);
            isTimerRunning = true;
        }
    }
    public void recover()
    {
        isTimerRunning = false;
        PlayerPrefs.DeleteKey(LastCallTimeKey);
        timerText.text = "Account";
        warning.SetActive(false);   
    }
    private void Update()
    {
        if (isTimerRunning)
        {
            // Calculate the remaining time
            TimeSpan elapsedTime = DateTime.Now - lastCallTime;
            float remainingTime = DebugDelay - (float)elapsedTime.TotalSeconds;

            // Check if enough time has passed since the last call
            if (elapsedTime.TotalSeconds >= DebugDelay)
            {
                Debug.Log("Debug message after 7 Week!");

                // Update the last call time
                PlayerPrefs.DeleteKey(LastCallTimeKey);
                Profile.instance.RemoveDATa();
            }

            // Update the timer display
            UpdateTimerDisplay(remainingTime);
        }
    }

    private void UpdateTimerDisplay(float remainingTime)
    {
        int days = Mathf.FloorToInt(remainingTime / 86400);
        int hours = Mathf.FloorToInt((remainingTime % 86400) / 3600);
        int minutes = Mathf.FloorToInt((remainingTime % 3600) / 60);
        string timerString = string.Format("{0:00}:{1:00}:{2:00}", days, hours, minutes);
        timerText.text = timerString;
    }

    private void StartTimer()
    {
        if (!isTimerRunning)
        {
            // Start the timer
            lastCallTime = DateTime.Now;
            PlayerPrefs.SetString(LastCallTimeKey, lastCallTime.ToString());
            Debug.Log(lastCallTime.ToString());
            isTimerRunning = true;
        }
    }
}
