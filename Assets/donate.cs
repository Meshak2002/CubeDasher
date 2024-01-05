using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class donate : MonoBehaviour
{
    // Start is called before the first frame update
    public Button bu;
    void Start()
    {
        bu = this.GetComponent<Button>();
        clan.instance.read_d();
        clan.instance.cubes.text = clan.instance.d.cubes.ToString();
        bu.onClick.AddListener(delegate
        {
            clan.instance.read_od(this.transform.parent.gameObject.name);
            if (clan.instance.d.cubes >= 50)
            {
                for (int i = 0; i < Profile.instance.pref.record; i++)
                {
                    string savename = Profile.instance.pref.nane[i];
                    if (savename == Profile.NameInputFieldd.text)
                    {
                        Profile.instance.pref.cubes[i] -= 50;
                        clan.instance.d.cubes -= 50;
                        clan.instance.other.cubes += 50;
                        Debug.Log("Donated0");
                    }
                    if (savename == this.transform.parent.gameObject.name)
                    {
                        Profile.instance.pref.cubes[i] += 50;
                        Debug.Log("Donated");
                    }
                    Profile.instance.uploadd();
                    clan.instance.uploaddata(Profile.NameInputFieldd.text,1);
                    clan.instance.uploaddata(this.transform.parent.gameObject.name,0);
                    clan.instance.cubes.text = clan.instance.d.cubes.ToString();
                }
            }
        });
        if (this.transform.parent.gameObject.name == Profile.NameInputFieldd.text)
        {
            bu.enabled = false;
            bu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
