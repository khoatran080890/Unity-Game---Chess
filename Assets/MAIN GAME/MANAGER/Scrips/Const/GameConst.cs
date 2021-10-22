using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConst
{
    public static class Tag
    {
        public static string block = "Battle Block";
    }


    public static class Scene
    {
        public static string Home = "Home";
        public static string Loading = "Loading";
        public static string MainHall = "Main Hall";
        public static string Battle_Limitless = "Battle Limitless";
    }
    public static class Addressable_Label
    {
        public static string Roster_100001 = "100001";
        public static string Roster_100002 = "100002";
        public static string Roster_100003 = "100003";
    }
    public enum GameEvent
    {
        Loading_nextloadingbar,
        Home_Login,

        Config,
    }
}
public enum ResultCode
{
    OK = 200,
    NOT_FOUND = 404,
    SERVER_ERROR = 403,
    RESPONSE_NULL = 0
}

public enum RosterSide
{
    PLAYER,
    ENEMY,
}


#region Easy Config
public enum ERoster
{
    Minator = 100001,
    Bee = 100002,
    Wraith = 100003,
}
public enum ERosterType
{
    Infantry_meele = 110001,
    Infantry_missle = 110002,
    Flyer_meele = 110011,
    Flyer_missle = 110012,
}
#endregion
