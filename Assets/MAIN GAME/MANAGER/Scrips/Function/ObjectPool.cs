using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [Serializable]
    public class ObjectInfo
    {
        public string id;
        public GameObject mainObj;
        public Transform Parent_mainObj;
        public int firstObjInstantiated;

        public List<GameObject> lstmainObj = new List<GameObject>();
    }

    [SerializeField] ObjectInfo[] objs;
    // private
    
    private void Awake()
    {
        Init();
    }
    void Init()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < objs[i].firstObjInstantiated; j++)
            {
                CreateObj(objs[i], false);
            }
        }
    }
    void CreateObj(ObjectInfo objectinfo, bool active, Action<GameObject> action = null)
    {
        GameObject obj = Instantiate(objectinfo.mainObj, objectinfo.Parent_mainObj);
        obj.transform.localPosition = Vector3.zero;
        objectinfo.lstmainObj.Add(obj);
        obj.SetActive(active);
        action?.Invoke(obj);
    }
    public void SetupObjectPool(ObjectInfo[] objectinfo_array)
    {
        objs = objectinfo_array;
        Init();
    }
    public GameObject GetfromPool(string id)
    {
        ObjectInfo objinfo = null;
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].id == id)
            {
                objinfo = objs[i];
            }
        }
        if (objinfo != null)
        {
            GameObject obj = null;
            for (int i = 0; i < objinfo.lstmainObj.Count; i++)
            {
                if (!objinfo.lstmainObj[i].activeSelf)
                {
                    objinfo.lstmainObj[i].SetActive(true);
                    obj = objinfo.lstmainObj[i];
                    return obj;
                }
            }
            if (obj == null)
            {
                CreateObj(objinfo, true, (newobj) =>
                {
                    obj = newobj;
                });
                return obj;
            }
            else
            {
                Debug.LogError("UNKNOWN ERROR");
                return null;
            }
        }
        else
        {
            Debug.LogError(id + " DOES NOT EXITS");
            return null;
        }
    }
}
