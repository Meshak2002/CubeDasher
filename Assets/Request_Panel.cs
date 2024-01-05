using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request_Panel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<string> reqtempNames,followingtemp;
    public GameObject par, prefab;
    void OnEnable()
    {
        reqtempNames = Profile.instance.dataa.requests;
        followingtemp = Profile.instance.dataa.following;
        foreach (string s in reqtempNames)
        {
            GameObject g = Instantiate(prefab, par.transform);
            g.transform.GetChild(0).GetComponent<Text>().text = s;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Follow Back";
            g.transform.GetChild(3).gameObject.SetActive(true);
            if (!followingtemp.Contains(s))
            {
                g.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                g.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
