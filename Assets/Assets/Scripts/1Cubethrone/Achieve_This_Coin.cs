using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Achieve_This_Coin : MonoBehaviour
{
    public string coinValue;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PlayerPrefs.GetInt("PlayerCube", 0));
    }
    public void get_Text()
    {
        coinValue = this.GetComponentInChildren<Text>().text;
       // Debug.Log(coinValue);
        PlayerPrefs.SetInt("PlayerCube", PlayerPrefs.GetInt("PlayerCube",0)+int.Parse(coinValue));
      
    }
}
