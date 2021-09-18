using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFunction : Singleton<GameFunction>
{

    public void CallAfter(float waittime, Action action)
    {
        StartCoroutine(Ienumerator_CallAfter(waittime, action));
    }
    IEnumerator Ienumerator_CallAfter(float waittime, Action action)
    {
        yield return new WaitForSeconds(waittime);
        action?.Invoke();
    }
}
