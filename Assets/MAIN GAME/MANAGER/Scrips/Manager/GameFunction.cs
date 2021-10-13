using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFunction : Singleton<GameFunction>
{

   

    /// <summary>
    /// Execute Action after time
    /// </summary>
    public void CallAfter(float waittime, Action action)
    {
        StartCoroutine(Ienumerator_CallAfter(waittime, action));
    }
    IEnumerator Ienumerator_CallAfter(float waittime, Action action)
    {
        yield return new WaitForSeconds(waittime);
        action?.Invoke();
    }

    /// <summary>
    /// Execute Action after time
    /// </summary>
    public void CallAfter_loop(float waittime, int times, Action action)
    {
        StartCoroutine(Ienumerator_CallAfter_loop(waittime, times, action));
    }
    IEnumerator Ienumerator_CallAfter_loop(float waittime, int times, Action action)
    {
        int i = 0;
        while (true)
        {
            if (i == times)
            {
                break;
            }
            yield return new WaitForSeconds(waittime);
            action?.Invoke();
            i++;
        }
    }

}
