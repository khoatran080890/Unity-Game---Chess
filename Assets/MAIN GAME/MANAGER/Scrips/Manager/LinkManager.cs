using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkManager : Singleton<LinkManager>
{
    public string GetLink_Config(string filename)
    {
        return "Assets/Addressable Container/Config/" + filename;
    }

    public string GetLink_Icon_Race(string filename)
    {
        return "Assets/Addressable Container/Icons/Race/" + filename + ".jpg";
    }
}
