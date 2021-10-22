using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHallManager : MonoBehaviour
{
    public void Button_LimitlessMode()
    {
        SceneManager.LoadScene(GameConst.Scene.Battle_Limitless);
    }
}
