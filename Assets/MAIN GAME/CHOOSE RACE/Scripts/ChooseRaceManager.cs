using BestHTTP.JSON.LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseRaceManager : MonoBehaviour, IEventRegisterObject
{
    [Header("Prefabs")]
    [SerializeField] GameObject prefab_RaceOption;
    [Header("Reference")]
    [SerializeField] Transform parent_RaceOption;
    [SerializeField] Image Icon_mainimage;
    [SerializeField] TextMeshProUGUI Description_title;
    [SerializeField] TextMeshProUGUI Description_description;
    [Header("Info")]
    List<RaceOptionDetail> allrace = new List<RaceOptionDetail>();
    [SerializeField] Race currentrace = null;
    private void Awake()
    {
        List<Race> lst_race = ConfigManager.Instance.Get_config_race();
        for (int i = 0; i < lst_race.Count; i++)
        {
            GameObject raceoption = Instantiate(prefab_RaceOption);
            raceoption.transform.SetParent(parent_RaceOption);
            RaceOptionDetail detail = raceoption.GetComponent<RaceOptionDetail>();
            allrace.Add(detail);
            detail.Init(lst_race[i]);
        }
        SetupRaceInfo();
    }
    private void OnEnable()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.ChooseRace_pick.ToString(), this);
    }
    private void OnDisable()
    {
        EventManager.Instance.RemoveEvent(this);
    }
    private void Update()
    {
        GameFunction.Instance.InputKeycode(KeyCode.A, () =>
        {
            NotificationManager.Instance.Show();
        });
    }

    void SetupRaceInfo()
    {
        if(currentrace != null)
        {
            AddressableManager.Instance.LoadAsset<Sprite>(LinkManager.Instance.GetLink_Icon_Race(currentrace.id.ToString()), (result, op) =>
            {
                Icon_mainimage.sprite = result;
            });
            Description_title.text = currentrace.name;
            Description_description.text = currentrace.description;
        }
        else
        {
            Icon_mainimage.sprite = null;
            Description_title.text = "";
            Description_description.text = "";
        }
        
    }

    public void StartGame()
    {
        if (currentrace != null)
        {
            SaveLoadManager.Instance.Load("folder", "filename.txt", (json) =>
            {
                if (json != "")
                {
                    JsonReader parsedata = new JsonReader(json);
                    LoginInfo logininfoResponse = JsonMapper.ToObject<LoginInfo>(parsedata);
                }
                else
                {
                    Debug.Log("First Login");
                }
            });
        }
        else
        {
            Debug.Log("Please choose Race");
        }
    }

    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.ChooseRace_pick:
                object[] data_parsed = data as object[];
                currentrace = data_parsed[0] as Race;
                Action action = (Action)data_parsed[1];

                if (currentrace != null)
                {
                    foreach (var raceoption in allrace)
                    {
                        if (raceoption.Get_raceinfo().id != currentrace.id)
                        {
                            raceoption.SetupBorder(false);
                        }
                    }
                }
                

                SetupRaceInfo();

                action?.Invoke();
                break;
        }
    }
}
