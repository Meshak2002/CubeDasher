using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(fileName ="Theme", menuName ="ScriptableObjects/theme")]
public class colors :ScriptableObject
{
    // Start is called before the first frame update
    public string[] color;
}
[Serializable]
public class paint
{
    public Image[] img;
}
