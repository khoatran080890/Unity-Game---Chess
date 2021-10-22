using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFunction : Singleton<GameFunction>
{

    /// <summary>
    /// Parse Enum
    /// </summary>
    public T Parse_StringEnum<T>(string name) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), name);
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

    public string Parse_Byte_Filesizestring(long bytes)
    {
        string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
        int i;
        double dblSByte = bytes;
        for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
        {
            dblSByte = bytes / 1024.0;
        }
        return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
    }

    #region RECTSRANSFOM
    public void SetAnchorPreset(RectTransform rt, AnchorPreset type, float left = 0f, float right = 1f, float bot = 0f, float top = 1f)
    {
        switch (type)
        {
            case AnchorPreset.Fit:
                rt.anchorMin = new Vector2(0, 0);
                rt.anchorMax = new Vector2(1, 1);
                break;
            case AnchorPreset.Dynamic:
                rt.anchorMin = new Vector2(left, bot);
                rt.anchorMax = new Vector2(right, top);
                break;
            case AnchorPreset.Center:
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                break;
        }
    }
    public void SetLeft(RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
    public void SetRight(RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
    public void SetTop(RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
    public void SetBottom(RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
    public void Set_Left_Right_Top_Bottom(RectTransform rt, float left, float right, float top, float bottom)
    {
        SetLeft(rt, left);
        SetRight(rt, right);
        SetTop(rt, top);
        SetBottom(rt, bottom);
    }
    #endregion

}
public enum AnchorPreset
{
    Fit,
    Center,
    Dynamic,
}
