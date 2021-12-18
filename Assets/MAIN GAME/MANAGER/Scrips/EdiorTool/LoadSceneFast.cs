#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class LoadSceneFast : MonoBehaviour
{
    // % = ctrl/cmd
    // # = shift
    // & = alt
    [MenuItem("Open Scene/Home &1")]
    public static void OpenScene_Home()
    {
        OpenScene("Home");
    }
    [MenuItem("Open Scene/Loading &2")]
    public static void OpenScene_Loading()
    {
        OpenScene("Loading");
    }
    [MenuItem("Open Scene/Choose Race &3")]
    public static void OpenScene_ChooseRace()
    {
        OpenScene("Choose Race");
    }
    [MenuItem("Open Scene/Main Hall &4")]
    public static void OpenScene_MainHall()
    {
        OpenScene("Main Hall");
    }
    [MenuItem("Open Scene/Battle &5")]
    public static void OpenScene_Battle()
    {
        OpenScene("Battle Limitless");
    }




    [MenuItem("Open Scene/Test &0")]
    public static void OpenScene_Test()
    {
        OpenScene("TEST", "Assets/TEST GAME/SCENE/");
    }

    public static void OpenScene(string sceneName, string prepath = "Assets/Scenes/")
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(prepath + sceneName + ".unity");
        }
    }
}
#endif
