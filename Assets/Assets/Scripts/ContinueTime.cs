using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ContinueTime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float continueTime;
    [SerializeField] private Text timer;
    private int time = 5;

    //animator
    void Start()
    {
        StartCoroutine("continueTimer");
        timeCal();
    }
    

    // Update is called once per frame
    private IEnumerator continueTimer()
    {
        //Anim.play
        yield return new WaitForSeconds(continueTime);
        SceneManager.LoadScene(2);
    }

    void timeCal()
    {
        if (time >= 0)
        {
            StartCoroutine("timerAnim");
        }
        timer.text = time.ToString();
    }


    private IEnumerator timerAnim()
    {
        yield return new WaitForSeconds(1);
        time--;
        if(time>=0)
        {
            timeCal();
        }
    }
}
