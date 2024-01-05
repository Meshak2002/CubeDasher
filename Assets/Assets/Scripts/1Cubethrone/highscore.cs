using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class highscore : MonoBehaviour
{
    public GameObject player;
    public int score;
    public Text highscore_text;
    public Text score_text;
    public GameObject gameoverpannel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)(transform.position.z);
        highscore_text.text = "HIGHSCORE = " + PlayerPrefs.GetInt("Score_N", 0).ToString();
        score_text.text = "SCORE = " + score;
        if (gameoverpannel)
        {
            //Debug.Log("game over");

            if (score>PlayerPrefs.GetInt("Score_N", 0))
            {
                Debug.Log("ITS highscore");
                PlayerPrefs.SetInt("Score_N", score);
            }

        }

    }

}
