using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceOptionDetail : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] Race raceinfo;

    [Header("Reference")]
    [SerializeField] GameObject Border;
    [SerializeField] Image mainimage;

    public void Init(Race raceinfo)
    {
        this.raceinfo = raceinfo;
        AddressableManager.Instance.LoadAsset<Sprite>(LinkManager.Instance.GetLink_Icon_Race(raceinfo.id.ToString()), (result, op) =>
        {
            mainimage.sprite = result;
        });
        if (raceinfo.valid != 1)
        {
            GetComponent<Button>().interactable = false;
        }
    }
    public void Click() 
    {
        object[] data = new object[2];
        if (!CheckBorder())
        {
            data[0] = raceinfo;
            SetupBorder(true);
        }
        else 
        {
            data[0] = null;
            SetupBorder(false);
        }
        data[1] = (Action)(() =>
        {

        });
        EventManager.Instance.SendEvent(GameConst.GameEvent.ChooseRace_pick.ToString(), data);

    }
    public void SetupBorder(bool click)
    {
        Border.SetActive(click);
    }
    public bool CheckBorder()
    {
        return Border.activeSelf;
    }

    #region GET
    public Race Get_raceinfo()
    {
        return raceinfo;
    }
    #endregion
}
