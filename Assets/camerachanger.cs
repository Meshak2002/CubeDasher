using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerachanger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject c1, c2;
    int n;
    void Start()
    {
        n = 0;
        camerachange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void camerachange()
    {
        if (n == 0)
        {
            c1.SetActive(true);
            c2.SetActive(false);
        }else if (n == 1)
        {
            c1.SetActive(false);
            c2.SetActive(true);
        }
        else
        {
            n = 0;
        }
        n++;
    }
}
