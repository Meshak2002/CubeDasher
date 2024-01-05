using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class leaderBoardcontrollerScores : MonoBehaviour
{
    public int ID;
    public int maxScores = 9;
    public TextMeshProUGUI[] Entries;
    public TextMeshProUGUI Entry;
    string playerName;
    string playerRank;
    int highScore;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Name") || !PlayerPrefs.HasKey("PlayerCube")) return;
        playerName = PlayerPrefs.GetString("Name");
        highScore = PlayerPrefs.GetInt("PlayerCube");
    }
    private void Update()
    {
    }
    public void GetPlayerRank()
    {
        LootLockerSDKManager.GetScoreListMain(ID, 1000, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Score Pulled");

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    highScore = members[i].score / 1000;
                    playerName = PlayerPrefs.GetString("Name");
                    playerRank = members[i].rank.ToString();
                    Entry.text += playerRank + ". " + playerName;
                }
            }
        });
    }
    private void Start()
    {
        LootLockerSDKManager.StartSession(playerName, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Success");
                SubmitScore();
                ShowScores();
                GetPlayerRank();
            }
            else
            {
                Debug.Log(response);
            }
        });
    }
    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    Entries[i].text = (scores[i].rank + ".   " + scores[i].metadata + playerName + " " + scores[i].score);
                }
                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        Entries[i].text = (i + 1).ToString() + ".   none";
                    }
                }
            }
            else
            {
                Debug.Log(response);
            }
        });
    }
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(playerName, int.Parse(highScore.ToString()), ID.ToString(), playerName, (response) =>
        {
            if (response.success) Debug.Log("Success");
            else Debug.Log("Failed");
        });
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}