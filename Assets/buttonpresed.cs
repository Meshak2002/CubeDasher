using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonpresed : MonoBehaviour
{
    // Start is called before the first frame update

    //Recruit members
    public Button bu;
    private clan c;
    void Start()
    {
        c = GameObject.Find("Clan_Panel__").GetComponent<clan>();
        bu = this.GetComponent<Button>();
        bu.onClick.AddListener(delegate
        {
            clan.instance.read_od(this.transform.parent.gameObject.name);
            if (clan.instance.other.clan == null)
            {
                c.selfrnds.Add(this.transform.parent.gameObject.name);
                bu.enabled = false;
                bu.gameObject.GetComponent<Image>().color = Color.green;
                bu.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Added";
            }
            else
            {
                Debug.Log(clan.instance.other.clan);
                Debug.Log("Player already in a clan");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
