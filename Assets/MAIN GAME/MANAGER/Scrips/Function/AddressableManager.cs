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
    public void DownloadAddressable_Remote(string label, Action<AddressableDownLoader> action)
    {
        Caching.ClearCache();
        Addressables.ClearDependencyCacheAsync(label);
        AddressableDownLoader download = new AddressableDownLoader(label);
        action(download);
    }
    public long Get_filesize(List<string> lst_label)
    {
        long totalfilesize = 0;
        for (int i = 0; i < lst_label.Count; i++)
        {
            totalfilesize += Addressables.GetDownloadSizeAsync(lst_label[i]).Result;
        }
        return totalfilesize;
    }
    // --------------------------------------------------------------------
    /// <summary>
    /// LOAD Async - Ok: load one time to memory when call many time - Bad: Not auto reasle when load new scene without release first
    /// </summary>
    public void LoadAsset<T>(string address, Action<T, AsyncOperationHandle<T>> action) where T: UnityEngine.Object
    {
        AsyncOperationHandle<T> download = Addressables.LoadAssetAsync<T>(address);
        download.Completed += (AsyncOperationHandle<T> result) =>
        {
            if (result.Status == AsyncOperationStatus.Succeeded)
            {
                action(result.Result, download);
                Addressables.Release(download);
            }
            else
            {
                Debug.LogError("FAIL LOADING AT PATH " + address);
            }
        };
    }
    //----------------------------------------------------------------------
    [SerializeField] AssetReference assetref;
    int index_Instantiate_GameObject = 0;
    Dictionary<int, GameObject> dict_Instantiate_GameObject = new Dictionary<int, GameObject>(); 
        
    /// <summary>
    /// Instantiate - Bad: load many time to memory if instantiate many time - OK: Release when load new scene
    /// action return: gameobject created, index for realeasing
    /// </summary>
    public void Instantiate_GameObject(string address, Action<GameObject, int> action)
    {
        AsyncOperationHandle<GameObject> download = Addressables.InstantiateAsync(assetref);
        download.Completed += (AsyncOperationHandle<GameObject> result) =>
        {
            if (result.Status == AsyncOperationStatus.Succeeded)
            {
                dict_Instantiate_GameObject.Add(index_Instantiate_GameObject, result.Result);
                action(result.Result, index_Instantiate_GameObject);
                index_Instantiate_GameObject++;
            }
            else
            {
                Debug.LogError("FAIL LOADING AT PATH " + address);
            }
        };
    }
    public void Realease_Instantiate_GameObject(int idx)
    {
        dict_Instantiate_GameObject.TryGetValue(idx, out GameObject obj);
        Destroy(obj);
        Addressables.ReleaseInstance(obj);
    }

    
}

public class AddressableDownLoader
{
    public string label;
    public AsyncOperationHandle download_remote;
    public long filesize;
    public float progress;

    public AddressableDownLoader(string label)
    {
        this.label = label;
        download_remote = Addressables.DownloadDependenciesAsync(label);
        filesize = Addressables.GetDownloadSizeAsync(label).Result;
    }
    
    public void Update_Progress()
    {
        progress = download_remote.PercentComplete;
        //Debug.Log(progress * filesize + " / " + filesize);
        if (download_remote.IsValid() && download_remote.IsDone)
        {
            if (download_remote.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("ERROR LOADING ADDRESSABLE " + label);
            }
        }
    }
}