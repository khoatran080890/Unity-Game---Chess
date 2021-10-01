using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFunction : Singleton<GameFunction>
{

    /// <summary>
    /// ScreenShot + delete old assign image
    /// </summary>
    IEnumerator TakeScreenshot(Image image)
    {
        yield return new WaitForEndOfFrame();
        try
        {
            Destroy(image.sprite.texture);
            Destroy(image.sprite);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.LoadRawTextureData(texture.GetRawTextureData());
        texture.Apply();

        image.sprite = Sprite.Create(texture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
    }

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
