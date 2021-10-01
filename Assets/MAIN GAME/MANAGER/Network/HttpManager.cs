using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpManager : MonoBehaviour
{
    //public void SendAPI()
    //{
    //    Dictionary<string, object> requestParams = new Dictionary<string, object>();
    //    requestParams.Add("user_id", "unknown");
    //    requestParams.Add("device_name", SystemInfo.deviceName);
    //    requestParams.Add("device_model", SystemInfo.deviceModel);
    //    requestParams.Add("device_os", SystemInfo.operatingSystem);

    //    DoConnect("api/logs/insert_device", requestParams, (json) =>
    //    {
    //        JsonReader reader = new JsonReader(json);
    //        BaseHTTPResponse loginRes = JsonMapper.ToObject<BaseHTTPResponse>(reader);
    //        callback(loginRes);
    //    });
    //}

    //public void DoConnect(string path, Dictionary<string, object> requestParams, Action<string> callback = null, Action<string> onError = null)
    //{
    //    string fullURL = "http://testmilu2.milu.jp" + ":" + 8081 + "/" + path;
    //    var request = new HTTPRequest(new Uri(fullURL), (req, res) => { onRequestCallBack(req, res, callback, onError); });
    //    //add Header 
    //    string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("milu2_admin" + ":" + "mlml246"));
    //    request.AddHeader("Authorization", "Basic " + svcCredentials);

    //    //add body
    //    //string stringParams = Json.Encode(requestParams);
    //    //request.RawData = new UTF8Encoding().GetBytes(stringParams);

    //    // add field
    //    Debug.LogWarning("Start Request HTTP :" + fullURL);
    //    foreach (var param in requestParams)
    //    {
    //        if (param.Value == null)
    //        {
    //            Debug.LogError("param null : " + param.Key);
    //            return;
    //        } // if

    //        Debug.Log("param : " + param.Key + "===" + param.Value.ToString());
    //        request.AddField(param.Key, param.Value.ToString());
    //    } // foreach

    //    request.MethodType = HTTPMethods.Post;
    //    request.Send();
    //}
}
