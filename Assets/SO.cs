using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="captcha" , menuName = "ScriptableObjects/captcha")]
public class SO : ScriptableObject
{
    public capt[] captchas;
}
[Serializable]
public class capt
{
    public Sprite cimg;
    public string value;
}
