using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trig : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCollision pc;
    void Start()
    {
        pc = GameObject.Find("CubeHolder").GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            
            //pc.currentCube++;
            

        }
    }
    }
