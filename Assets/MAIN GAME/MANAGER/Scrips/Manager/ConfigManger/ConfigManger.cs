using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManger : Singleton<ConfigManger>, IEventRegisterObject
{
    RosterConfig config_roster = new RosterConfig();

    private void Start()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config.ToString(), this);
    }
    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Config:
                Config(data);
                break;
        }
    }
    #region HANDLE EVENT LISTENER
    void Config(object data)
    {
        object[] data_parsed = data as object[];
        config_roster.rosterconfig = data_parsed[0] as List<Roster>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    #endregion

    #region GET

    #endregion
}
// Roster.txt
[System.Serializable]
public class RosterConfig
{
    public List<Roster> rosterconfig;
}
[System.Serializable]
public class Roster
{
    public int id;
    public string name;
    public float hp;
    public float attack;
    public float moventspeed;
    public float attackcooldown;
    public float attackrange;
    public int formation;
    public int rostertype;
    public int missletype;


}
// Formation.txt
