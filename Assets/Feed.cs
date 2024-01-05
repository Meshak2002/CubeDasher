using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<string> tempNames;
    public GameObject par, prefab;
    private void OnEnable()
    {
        tempNames = Profile.instance.pref.nane;
        tempNames.Remove(Profile.instance.dataa.nane);
        foreach (string s in Profile.instance.dataa.following)
        {
            if (!tempNames.Remove(s))
            {
                continue;
            }
        }
        foreach(string s in tempNames)
        {
            GameObject g= Instantiate(prefab, par.transform);
            g.transform.GetChild(0).GetComponent<Text>().text = s;
        }
    }
}
