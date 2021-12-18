using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    bool created;
    [SerializeField] string Prefabname;

    public void Init()
    {
        created = true;
    }
    public bool CheckExistance()
    {
        return created;
    }

    public string Get_Prefabname()
    {
        return Prefabname;
    }
    
}
