using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>, IEventRegisterObject
{

    PlayerInfo playerinfo;
    private void Start()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Home_Login.ToString(), this);
    }

    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Home_Login:
                object[] data_parsed = data as object[];
                playerinfo = data_parsed[0] as PlayerInfo;
                Action action = (Action)data_parsed[1];
                action?.Invoke();
                break;
        }
    }

    #region GET
    public PlayerInfo Get_PlayerInfo()
    {
        return playerinfo;
    }
    #endregion
}


public class PlayerInfo
{
    public List<RosterInfo> allroster = new List<RosterInfo>();
}

public class RosterInfo
{
    int id;
    int number;

    public RosterInfo(int id, int number)
    {
        this.id = id;
        this.number = number;
    }
}
