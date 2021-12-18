using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Homemanger : MonoBehaviour
{
    public void Button_Start()
    {
        // fake char
        PlayerInfo playerinfo = new PlayerInfo();
        playerinfo.allroster.Add(new RosterInfo((int)ERoster.Minator, 3));
        playerinfo.allroster.Add(new RosterInfo((int)ERoster.Wraith, 2));
        playerinfo.allroster.Add(new RosterInfo((int)ERoster.Bee, 1));

        object[] data = new object[2];
        data[0] = playerinfo;
        data[1] = (Action)(() =>
        {
            SceneManager.LoadScene(GameConst.Scene.Loading);
        });
        EventManager.Instance.SendEvent(GameConst.GameEvent.Home_Login.ToString(), data);
    }
    private void Update()
    {
        
    }
}
