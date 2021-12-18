using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarManagerConfig : MonoBehaviour, IEventRegisterObject
{
    Slider slider;

    int currentfinishedconfig;
    int totalconfig;

    [SerializeField] TextMeshProUGUI nametxt;
    [Header("Code support")]
    bool beginload;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0.03f;

        EventManager.Instance.ListenEvent(GameConst.GameEvent.Loading_finishconfig_one.ToString(), this);
    }
    public void Init(int totalconfig)
    {
        nametxt.text = "Parsing Config";
        this.totalconfig = totalconfig;
        beginload = true;
    }
    private void Update()
    {
        if (beginload)
        {
            slider.value = (float)((float)currentfinishedconfig / (float)totalconfig);
            if (slider.value >= 1)
            {
                beginload = false;
                EventManager.Instance.SendEvent(GameConst.GameEvent.Loading_finishconfig_all.ToString(), null);
            }
        }
    }

    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Loading_finishconfig_one:
                currentfinishedconfig++;
                break;
        }
    }
}
