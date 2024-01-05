using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;
    public GameObject HoverText;
    // Start is called before the first frame update
    void Start()
    {
        HoverText.SetActive(false);
        slider.value = PlayerPrefs.GetFloat("GlideValue", 5);
        slider.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("GlideValue", v);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnMouseOver()
    {
        HoverText.SetActive(true);
    }
    public void OnMouseExit()
    {
        HoverText.SetActive(false);
    }

}
