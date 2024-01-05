using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Musics",menuName ="ScriptableObjects/Musics") ]
public class Music : ScriptableObject
{
    // Start is called before the first frame update
    public List<AudioClip> Audios;
}
