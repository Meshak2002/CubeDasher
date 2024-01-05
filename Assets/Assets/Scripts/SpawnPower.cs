using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPower : MonoBehaviour
{
    public GameObject magnetpowers;
    public GameObject sprint;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 3);
        if(r == 0)
        {
            Instantiate(sprint, transform.position, transform.rotation);
        }
        else if (r == 1)
        {
            Instantiate(magnetpowers, transform.position, transform.rotation);
        }
        else
        {
            //dont
        }
         
    }
}
