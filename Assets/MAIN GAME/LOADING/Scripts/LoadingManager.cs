using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour, IEventRegisterObject
{
    [SerializeField] Transform LoadingBar_parent;

    List<string> lst_label;
    int currentloadingbar_index;
    long totalfilesize;

    private void Awake()
    {
        lst_label = new List<string>
        {
            GameConst.Addressable_Label.Roster_100001,
            GameConst.Addressable_Label.Roster_100002,
            GameConst.Addressable_Label.Roster_100003
        };
        currentloadingbar_index = 0;
        
        for (int i = 0; i < lst_label.Count; i++)
        {
            Create_LoadingBar(totalfilesize);
            currentloadingbar_index++;
        }
        totalfilesize = AddressableManager.Instance.Get_filesize(lst_label);
        Debug.Log(totalfilesize);
    }
    private void OnEnable()
    {
        EventManager.Instance.ListenEvent(GameConst.GameEvent.Loading_nextloadingbar.ToString(), this);
    }
    private void OnDisable()
    {
        //EventManager.Instance.RemoveEvent(this);
    }
    void Create_LoadingBar(long totalfilesize)
    {
        //if (currentloadingbar_index >= lst_label.Count)
        //{
        //    // finish download
        //    Debug.Log("FINISH DOWNLOAD ADDRESSABLE");
        //    return;x
        //}
        AddressableManager.Instance.DownloadAddressable_Remote(lst_label[currentloadingbar_index], (download) =>
        {
            GameObject loadingbar = Instantiate(Resources.Load<GameObject>("Loading Bar"));
            loadingbar.transform.SetParent(LoadingBar_parent);

            RectTransform rect = loadingbar.GetComponent<RectTransform>();
            GameFunction.Instance.SetAnchorPreset(rect, AnchorPreset.Fit);
            GameFunction.Instance.Set_Left_Right_Top_Bottom(rect, 0, 0, 0, 0);

            loadingbar.GetComponent<LoadingBarManager>().Init(download, totalfilesize);
        });
    }

    public void EventListener(string eventname, object data)
    {
        GameConst.GameEvent event_enum = GameFunction.Instance.Parse_StringEnum<GameConst.GameEvent>(eventname);
        switch (event_enum)
        {
            case GameConst.GameEvent.Loading_nextloadingbar:
                //currentloadingbar_index++;
                //Create_LoadingBar(totalfilesize);
                break;
        }
    }
}
