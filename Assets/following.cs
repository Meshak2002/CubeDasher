using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class following : MonoBehaviour
{
 
    [SerializeField] private List<string> followingg;
   [SerializeField] private GameObject par, prefab;
    public GameObject story;
    public static following instance;
    private void OnEnable()
    {
        instance = this;
        Profile.instance.retj();
        followingg = Profile.instance.dataa.following;
        foreach(string s in followingg)
        {
            GameObject g = Instantiate(prefab, par.transform);
            g.transform.GetChild(0).GetComponent<Text>().text = s;
        }
    }

}
