using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    Dictionary<string, List<IEventRegisterObject>> dic = new Dictionary<string, List<IEventRegisterObject>>();
    public void ListenEvent(string eventname, IEventRegisterObject eventregisterobject)
    {
        List<IEventRegisterObject> lstregisterobject = null;
        // Check KEY FIRST
        // Check if eventname exits
        if (!dic.TryGetValue(eventname, out lstregisterobject))
        {
            // Init first time
            lstregisterobject = new List<IEventRegisterObject>();
            dic.Add(eventname, lstregisterobject);
        }
        // CHECK VALUE
        if (!lstregisterobject.Contains(eventregisterobject))
        {
            lstregisterobject.Add(eventregisterobject);
        }
    }
    public void SendEvent(string eventname, object data)
    {
        List<IEventRegisterObject> lstregisterobject = null;
        if (!dic.TryGetValue(eventname, out lstregisterobject))
        {
            Debug.Log("EVENT : " + eventname + " is not register yet !!!");
        }
        else
        {
            foreach(IEventRegisterObject obj in lstregisterobject)
            {
                if (obj != null)
                {
                    obj.EventListener(eventname, data);
                }
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var i in dic)
            {
                Debug.Log(i.Key + " --- " + i.Value.Count + Environment.NewLine);
            }
        }
    }
}
public interface IEventRegisterObject
{
    void EventListener(string eventname, object data);
}
