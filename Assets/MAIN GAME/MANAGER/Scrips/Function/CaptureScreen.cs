using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureScreen : MonoBehaviour
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
}
