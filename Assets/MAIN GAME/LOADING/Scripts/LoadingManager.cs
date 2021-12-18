using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour, IEventRegisterObject
{
    [SerializeField] Transform LoadingBar_parent;

    List<string> lst_label;
    int currentloadingbar_index;
    long totalfilesize;

    private void Awake()
    {
        // Addressable
        lst_label = new List<string>
        {
            GameConst.Addressable_Label.Roster_100001,
            GameConst.Addressable_Label.Roster_100002,
            GameConst.Addressable_Label.Roster_100003,
            GameConst.Addressable_Label.Config,
            GameConst.Addressable_Label.UI,
        };
        currentloadingbar_index = 0;

        totalfilesize = AddressableManager.Instance.Get_filesize(lst_label);
        Debug.Log(totalfilesize);
        Create_LoadingBar_Addressable(totalfilesize);

    }
    private void OnEnable()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Loading_nextloadingbar_addressable.ToString(), this);
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Loading_finishconfig_all.ToString(), this);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent(this);
    }
    #region ADDRESSABLE
    void Create_LoadingBar_Addressable(long totalfilesize)
    {
        if (currentloadingbar_index >= lst_label.Count)
        {
            // finish download
            Debug.Log("FINISH DOWNLOAD ADDRESSABLE");
            // Config
            Create_LoadingBar_Config(ConfigManager.Instance.Get_configcount());
            return;
        }
        AddressableManager.Instance.DownloadAddressable_Remote(lst_label[currentloadingbar_index], (download) =>
        {
            GameObject loadingbar = Instantiate(Resources.Load<GameObject>("Loading Bar Addressable"));
            loadingbar.transform.SetParent(LoadingBar_parent);

            RectTransform rect = loadingbar.GetComponent<RectTransform>();
            GameFunction.Instance.SetAnchorPreset(rect, AnchorPreset.Fit);
            GameFunction.Instance.Set_Left_Right_Top_Bottom(rect, 0, 0, 0, 0);

            loadingbar.GetComponent<LoadingBarManagerAddressable>().Init(download, totalfilesize);
        });
    }
    #endregion
    #region CONFIG
    void Create_LoadingBar_Config(int totalconfig)
    {
        AddressableManager.Instance.LoadAsset<GameObject>(LinkManager.Instance.GetLink_Config("Loading Bar Config.prefab"), (result, op) =>
        {
            GameObject loadingbar = Instantiate(result);
            loadingbar.transform.SetParent(LoadingBar_parent);

            RectTransform rect = loadingbar.GetComponent<RectTransform>();
            GameFunction.Instance.SetAnchorPreset(rect, AnchorPreset.Fit);
            GameFunction.Instance.Set_Left_Right_Top_Bottom(rect, 0, 0, 0, 0);

            loadingbar.GetComponent<LoadingBarManagerConfig>().Init(totalconfig);
            ParseConfig();
        });
    }
    void ParseConfig()
    {
        AddressableManager.Instance.LoadAsset<TextAsset>(LinkManager.Instance.GetLink_Config("Roster.txt"), (result, op) => 
        {
            string textData = result.text;
            object[] data_config = new object[2];
            data_config[0] = CSVParser.Deserialize<Roster>(textData).ToList();
            data_config[1] = (Action)(() => 
            {
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), null);
            });
            EventManager.Instance.SendEvent(GameConst.GameEvent.Config_roster.ToString(), data_config);
        });
        AddressableManager.Instance.LoadAsset<TextAsset>(LinkManager.Instance.GetLink_Config("Rostertype.txt"), (result, op) =>
        {
            string textData = result.text;
            object[] data_config = new object[2];
            data_config[0] = CSVParser.Deserialize<RosterType>(textData).ToList();
            data_config[1] = (Action)(() =>
            {
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), null);
            });
            EventManager.Instance.SendEvent(GameConst.GameEvent.Config_rostertype.ToString(), data_config);
        });
        AddressableManager.Instance.LoadAsset<TextAsset>(LinkManager.Instance.GetLink_Config("Missle.txt"), (result, op) =>
        {
            string textData = result.text;
            object[] data_config = new object[2];
            data_config[0] = CSVParser.Deserialize<Missle>(textData).ToList();
            data_config[1] = (Action)(() =>
            {
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), null);
            });
            EventManager.Instance.SendEvent(GameConst.GameEvent.Config_missle.ToString(), data_config);
        });
        AddressableManager.Instance.LoadAsset<TextAsset>(LinkManager.Instance.GetLink_Config("Formation.txt"), (result, op) =>
        {
            string textData = result.text;
            object[] data_config = new object[2];
            data_config[0] = CSVParser.Deserialize<Formation>(textData).ToList();
            data_config[1] = (Action)(() =>
            {
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), null);
            });
            EventManager.Instance.SendEvent(GameConst.GameEvent.Config_formation.ToString(), data_config);
        });
        AddressableManager.Instance.LoadAsset<TextAsset>(LinkManager.Instance.GetLink_Config("Race.txt"), (result, op) =>
        {
            string textData = result.text;
            object[] data_config = new object[2];
            data_config[0] = CSVParser.Deserialize<Race>(textData).ToList();
            data_config[1] = (Action)(() =>
            {
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), null);
            });
            EventManager.Instance.SendEvent(GameConst.GameEvent.Config_race.ToString(), data_config);
        });
    }
    #endregion
    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Loading_nextloadingbar_addressable:
                currentloadingbar_index++;
                Create_LoadingBar_Addressable(totalfilesize);
                break;
            case GameConst.GameEvent.Loading_finishconfig_all:
                Debug.Log("FINISH DOWNLOAD CONFIG");
                SceneManager.LoadScene(GameConst.Scene.ChooseRace);
                break;
        }
    }
}
