using BestHTTP;
using BestHTTP.JSON.LitJson;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class HttpManager : MonoBehaviour
{
    /// <summary>
    /// Example
    /// </summary>
    public void SendAPI(Action<HTTPResponse_1> callback)
    {
        Dictionary<string, object> requestParams = new Dictionary<string, object>();
        requestParams.Add("user_id", "unknown");
        requestParams.Add("device_name", SystemInfo.deviceName);
        requestParams.Add("device_model", SystemInfo.deviceModel);
        requestParams.Add("device_os", SystemInfo.operatingSystem);

        DoConnect("api/logs/insert_device", requestParams, (json) =>
        {
            JsonReader reader = new JsonReader(json);
            HTTPResponse_1 loginRes = JsonMapper.ToObject<HTTPResponse_1>(reader);
            callback(loginRes);
        });
    }


    /// <summary>
    /// Connect
    /// </summary>
    public void DoConnect(string path, Dictionary<string, object> requestParams, Action<string> callback = null, Action<string> onError = null)
    {
        string fullURL = "http://testmilu2.milu.jp" + ":" + 8081 + "/" + path;
        var request = new HTTPRequest(new Uri(fullURL), (req, res) => { onRequestCallBack(req, res, callback, onError); });
        //add Header 
        string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("milu2_admin" + ":" + "mlml246"));
        request.AddHeader("Authorization", "Basic " + svcCredentials);
        //add body
        //add field
        Debug.LogWarning("Start Request HTTP :" + fullURL);
        foreach (var param in requestParams)
        {
            if (param.Value == null)
            {
                Debug.LogError("param null : " + param.Key);
                return;
            }
            Debug.Log("param : " + param.Key + "===" + param.Value.ToString());
            request.AddField(param.Key, param.Value.ToString());
        }
        request.MethodType = HTTPMethods.Post;
        request.Send();
    }
    private void onRequestCallBack(HTTPRequest request, HTTPResponse response, Action<string> callback, Action<string> onError = null)
    {
        if (response == null)
        {
            Debug.LogError($"Request to {request} --> status code : {MLResultCode.RESPONSE_NULL} --> error: {response.DataAsText}" + " -- Link: " + request.CurrentUri.ToString());
            return;
        }
        MLResultCode resultCode = (MLResultCode)response.StatusCode;
        if (resultCode == MLResultCode.SERVER_ERROR)
        {
            onError?.Invoke(response.DataAsText);
            return;
        }
        if (resultCode != MLResultCode.OK)
        {
            Debug.LogError($"Request to {request} --> status code : {resultCode} --> error: {response.DataAsText}" + " -- Link: " + request.CurrentUri.ToString());
            callback?.Invoke(response.DataAsText);
            Debug.LogWarning($"End Request HTTP OK url --> {request.CurrentUri}");
            Debug.LogWarning($"http result NOT OK - res: {response.DataAsText}");
        }
        else
        {
            Debug.Log($"End Request HTTP OK url --> {request.CurrentUri}");
            Debug.Log($"http result OK - res: {response.DataAsText}");
            callback?.Invoke(response.DataAsText);
        }
    }
}

public class BaseHTTPResponse
{
    public int status;
    public string error;
}

[Serializable]
public class HTTPResponse_1: BaseHTTPResponse
{
    public string data;
}
