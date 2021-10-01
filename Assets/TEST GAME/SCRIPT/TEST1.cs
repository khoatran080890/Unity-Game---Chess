using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST1 : MonoBehaviour, IEventRegisterObject
{
    public void EventListener(string eventname, object data)
    {
        Debug.Log(eventname + " - " + data);
    }
    void Start()
    {
        EventManager.Instance.ListenEvent("abcd", this);
    }


}
