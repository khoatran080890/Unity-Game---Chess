using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : Singleton<AddressableManager>
{
    

    /// <summary>
    /// CALL FIRST TO CHECK DOWNLOAD
    /// </summary>
    public void DownloadAddressable_Remote(string label)
    {
        Addressables.ClearDependencyCacheAsync(label);
        AsyncOperationHandle<long> downloadsize = Addressables.GetDownloadSizeAsync(label);
        if (downloadsize.Result > 0)
        {
            AddressableDownLoader download = new AddressableDownLoader(label);
        }
    }


    /// <summary>
    /// LOAD BY ADDRESS
    /// </summary>
    void LoadAddressable<T>(string address, Action<T, AsyncOperationHandle<T>> action) where T: UnityEngine.Object
    {
        AsyncOperationHandle<T> download = Addressables.LoadAssetAsync<T>(address);
        download.Completed += (AsyncOperationHandle<T> result) =>
        {
            if (result.Status == AsyncOperationStatus.Succeeded)
            {
                action(result.Result, download);
            }
            else
            {
                Debug.LogError("FAIL LOADING AT PATH " + address);
            }
        };
    }
    void Realease<T>(AsyncOperationHandle<T> download) where T : UnityEngine.Object
    {
        Addressables.Release(download);
    }
}

public class AddressableDownLoader: MonoBehaviour
{
    string label;
    AsyncOperationHandle download_remote;
    long filesize;
    float progress;

    public AddressableDownLoader(string label)
    {
        this.label = label;
        download_remote = Addressables.DownloadDependenciesAsync(label);
        filesize = Addressables.GetDownloadSizeAsync(label).Result;
    }
    private void Update()
    {
        progress = download_remote.PercentComplete;
        Debug.Log(progress * filesize + " / " + filesize);
        if (download_remote.IsValid() && download_remote.IsDone)
        {
            if (download_remote.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("ERROR LOADING ADDRESSABLE " + label);
            }
            else
            {
                Debug.Log("SUCCESS LOADING ADDRESSABLE " + label);
            }
        }
    }
}