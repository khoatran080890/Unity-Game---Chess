using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : Singleton<GUIManager>
{
    [Header("Special GUI")] // Seperated Manager
    public GUI Notificatiion_GUI;
    public void ShowGUI(GUI gui)
    {
        if (!gui.CheckExistance())
        {
            AddressableManager.Instance.LoadAsset<GameObject>(gui.Get_Prefabname(), (result, op) =>
            {
                GameObject obj = Instantiate(result, gui.transform);
                gui.Init();
            });
        }
    }
    public void HideGUI(GUI gui)
    {
        gui.gameObject.SetActive(false);
    }
}

