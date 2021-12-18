using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TEST : MonoBehaviour
{

    private void Awake()
    {
        GameObject loadingbar = Instantiate(Resources.Load<GameObject>("Loading Bar"));
        loadingbar.transform.parent = transform;
    }


}


