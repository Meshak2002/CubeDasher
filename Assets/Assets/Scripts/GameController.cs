using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject GameOverPanel;
    public GameObject tapToStart;
    public GameObject scoreText;
    public bool menu = false;
    public Button Left;
    public Button Right;
    private void Start()
    {
        GameOverPanel.SetActive(false);
        tapToStart.SetActive(true);
        //scoreText.SetActive(false);
        PauseGame();
        tapToStart.SetActive(false);
        StarGame();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StarGame();
        }
    }
    public void GameOver()
    {
        Left.interactable = false;
        Right.interactable = false;
        scoreText.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Left.interactable = true;
        Right.interactable = true;
        SceneManager.LoadSceneAsync("Game");
    }

    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void StarGame()
    {
        scoreText.SetActive(true);
        tapToStart.SetActive(false);
        Time.timeScale = 1;
    }
}
