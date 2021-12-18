using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>, IEventRegisterObject
{
    [Header("Config")]
    RosterConfig config_roster = new RosterConfig();
    [Header("Config")]
    RosterTypeConfig config_rostertype = new RosterTypeConfig();
    [Header("Config")]
    MissleConfig config_missle = new MissleConfig();
    [Header("Config")]
    FormationConfig config_formation = new FormationConfig();
    [Header("Config")]
    RaceConfig config_race = new RaceConfig();

    private void Start()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config_roster.ToString(), this);
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config_rostertype.ToString(), this);
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config_missle.ToString(), this);
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config_formation.ToString(), this);
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Config_race.ToString(), this);
    }
    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Config_roster:
                Config_roster(data);
                break;
            case GameConst.GameEvent.Config_rostertype:
                Config_rostertype(data);
                break;
            case GameConst.GameEvent.Config_missle:
                Config_missle(data);
                break;
            case GameConst.GameEvent.Config_formation:
                Config_formation(data);
                break;
            case GameConst.GameEvent.Config_race:
                Config_race(data);
                break;
        }
    }
    #region HANDLE EVENT LISTENER
    void Config_roster(object data)
    {
        object[] data_parsed = data as object[];
        config_roster.lst = data_parsed[0] as List<Roster>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    void Config_rostertype(object data)
    {
        object[] data_parsed = data as object[];
        config_rostertype.lst = data_parsed[0] as List<RosterType>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    void Config_missle(object data)
    {
        object[] data_parsed = data as object[];
        config_missle.lst = data_parsed[0] as List<Missle>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    void Config_formation(object data)
    {
        object[] data_parsed = data as object[];
        config_formation.lst = data_parsed[0] as List<Formation>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    void Config_race(object data)
    {
        object[] data_parsed = data as object[];
        config_race.lst = data_parsed[0] as List<Race>;
        Action action = (Action)data_parsed[1];
        action?.Invoke();
    }
    #endregion

    #region GET
    public int Get_configcount()
    {
        //return typeof(ConfigManger).GetProperties().Length;
        int count = 0;
        FieldInfo[] allfields = typeof(ConfigManager).GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (FieldInfo field in allfields)
        {
            HeaderAttribute header = field.GetCustomAttributes(typeof(HeaderAttribute), true).FirstOrDefault() as HeaderAttribute;
            if (header.header == "Config")
            {
                count++;
            }
        }
        return count;
    }
    public List<Race> Get_config_race()
    {
        return config_race.lst;
    }
    #endregion
}
// Roster.txt
[System.Serializable]
public class RosterConfig
{
    public List<Roster> lst;
}
[System.Serializable]
public class Roster
{
    public int id;
    public string name;
    public int unitsize;
    public float hp;
    public float leadership;
    public float attackrange;
    public float meleebase;
    public float meleepierce;
    public float misslebase;
    public float misslepierce;
    public float ammunition;
    public float charge;
    public float armour;
    public float attack;
    public float defend;
    public float moventspeed;
    public float attackcooldown;
    public int formation;
    public int rostertype;
    public int missletype;
    public int race;
    public int active;
    public int passive;
    public int shield;
    public float antilarge;
    public int chargedefend;
    public int causefear;
}
// Formation.txt
[System.Serializable]
public class FormationConfig
{
    public List<Formation> lst;
}
[System.Serializable]
public class Formation
{
    public int id;
    public int row;
    public int column;
    public int rowdistant;
    public int columndistant;
}
// RosterType.txt
public class RosterTypeConfig
{
    public List<RosterType> lst;
}
[System.Serializable]
public class RosterType
{
    public int id;
    public string name;
    public float flydistant;
}
// Missle
public class MissleConfig
{
    public List<Missle> lst;
}
[System.Serializable]
public class Missle
{
    public int id;
    public string speed;
    public float height;
}
// Race
public class RaceConfig
{
    public List<Race> lst;
}
[System.Serializable]
public class Race
{
    public int id;
    public string name;
    public int valid;
    public string description;
}