using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] Text title;
    [SerializeField] Text content;
    public void Show()
    {
        GUIManager.Instance.ShowGUI(GUIManager.Instance.Notificatiion_GUI);
    }
}
