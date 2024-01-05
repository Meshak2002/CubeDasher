using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class clandata 
{
    public List<frnds> clans;
}
[Serializable]
public class frnds
{
    public string clan,admin;
    public List<string> cfrndnames;
    public List<bans> kicks;
}
[Serializable]
public class bans
{
    public string nam;
    public string duration;
}
