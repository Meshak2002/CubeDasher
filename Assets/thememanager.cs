using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thememanager : MonoBehaviour
{
    // Start is called before the first frame update
    public colors colorr;
    public paint paint;
    public Music music;
    public int n,m;
    public Button changeMusi,theme;
    public static thememanager instance;
    void Start()
    {
        if (instance == null)
            instance = this;
        this.gameObject.GetComponent<AudioSource>().enabled = false;
        n = PlayerPrefs.GetInt("ColorSav");
        m = PlayerPrefs.GetInt("MusicSav");
        changecol();
        this.gameObject.GetComponent<AudioSource>().clip = music.Audios[m];
        this.gameObject.GetComponent<AudioSource>().enabled = true;
        changeMusi.onClick.AddListener(delegate
        {
            changeMusi.gameObject.GetComponent<Animator>().Play("change themeclk", 0);
            mchange();
        });
    }
    public void changecol()
    {
        foreach (Image ig in paint.img)
        {
            Color col;
            ColorUtility.TryParseHtmlString(colorr.color[n], out col);
            ig.color = col;
        }
    }
    public void clk()
    {
        theme.gameObject.GetComponent<Animator>().Play("changetheme", 0);
        if (n < colorr.color.Length-1)
        {
            n++;
        }
        else
        {
            n = 0;
        }
        PlayerPrefs.SetInt("ColorSav", n);
        changecol();
    }
    public void mchange()
    {
        this.gameObject.GetComponent<AudioSource>().enabled = false;
        if (m < music.Audios.Count - 1)
        {
            m++;
        }
        else
        {
            m = 0;
        }
        this.gameObject.GetComponent<AudioSource>().clip = music.Audios[m];
        this.gameObject.GetComponent<AudioSource>().enabled = true;
        PlayerPrefs.SetInt("MusicSav", m);
    }
}
