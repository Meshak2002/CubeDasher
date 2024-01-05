using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class captcha_sys : MonoBehaviour
{
    // Start is called before the first frame update
    public SO captch;
    public Image img;
    public InputField inut;
    public string ans;
    public Text wron;
    private int n;
    void Start()
    {
        for(int i = 0; i < captch.captchas.Length; i++) {
            captch.captchas[i].value = captch.captchas[i].cimg.name;
        }
        n = Random.Range(0, captch.captchas.Length-1);
        img.sprite = captch.captchas[n].cimg;
        ans = captch.captchas[n].value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resett()
    {
        wron.enabled = false;
        n = Random.Range(0, captch.captchas.Length - 1);
        img.sprite = captch.captchas[n].cimg;
        ans = captch.captchas[n].value;
    }
    public void submit()
    {
        wron.enabled = false;
        if (inut.text == ans)
        {
            Debug.Log("CRT");
            StartCoroutine(fin());
        }
        else
        {
            wron.enabled = true;
        }
    }
    IEnumerator fin(){
        float g = GameObject.Find("Fade").GetComponent<fade>().BeginFade(1);
        yield return new WaitForSeconds(g);
        SceneManager.LoadScene(2);
    }
}
