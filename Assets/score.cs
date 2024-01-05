using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    // Start is called before the first frame update
    public float scor;
    private float conver;
    public Text sc;
    public GameObject over,overr;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (over.activeSelf || overr.activeSelf)
        {

        }
        else
        {
            scor += 12.1f * Time.deltaTime;
            conver = (int)scor;
            sc.text = conver.ToString();
        }
        
    }
}
