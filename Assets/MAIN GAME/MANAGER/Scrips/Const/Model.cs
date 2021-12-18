using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
   
}

[Serializable]
public class LoginInfo
{
    public int level;
    public float experience;

    public int gold;
    public int ruby;

    public Dictionary<int, LoginInfo_RaceInfo> lstRaceinfos;
}
[Serializable]
public class LoginInfo_RaceInfo
{
    public List<Roster> lstRosters;

}
