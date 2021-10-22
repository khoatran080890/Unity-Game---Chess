using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] List<RosterManager> allroster = new List<RosterManager>();
    [SerializeField] int number;

    [Header("Code Support")]
    [SerializeField] Vector3 centerpositon;
    [SerializeField] List<Vector3> lstPos = new List<Vector3>();
    bool beginsetinline;
    private void Awake()
    {
        allroster = transform.GetComponentsInChildren<RosterManager>().ToList();
        number = allroster.Count;
        GameFunction.Instance.CallAfter(2, () => 
        { 
            BackInLine();
            beginsetinline = true;
        });
    }
    private void Update()
    {
        if (beginsetinline)
        {
            allroster[0].transform.position = Vector3.MoveTowards(allroster[0].transform.position, lstPos[0], allroster[0].Get_moventspeed() * Time.deltaTime);
            allroster[1].transform.position = Vector3.MoveTowards(allroster[1].transform.position, lstPos[1], allroster[0].Get_moventspeed() * Time.deltaTime);
            allroster[2].transform.position = Vector3.MoveTowards(allroster[2].transform.position, lstPos[2], allroster[0].Get_moventspeed() * Time.deltaTime);
            allroster[3].transform.position = Vector3.MoveTowards(allroster[3].transform.position, lstPos[3], allroster[0].Get_moventspeed() * Time.deltaTime);
            allroster[4].transform.position = Vector3.MoveTowards(allroster[4].transform.position, lstPos[4], allroster[0].Get_moventspeed() * Time.deltaTime);
            allroster[5].transform.position = Vector3.MoveTowards(allroster[5].transform.position, lstPos[5], allroster[0].Get_moventspeed() * Time.deltaTime);
        }
    }
    void BackInLine()
    {
        // Get centerpoint
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i < allroster.Count; i++)
        {
            x += allroster[i].transform.position.x;
            y += allroster[i].transform.position.y;
            z += allroster[i].transform.position.z;
        }
        centerpositon = new Vector3(x / allroster.Count, y / allroster.Count, z / allroster.Count);


        // set position
        lstPos.Clear();
        Vector3 pos_1 = centerpositon + new Vector3(5, 5, 0);
        Vector3 pos_2 = centerpositon + new Vector3(5, 0, 0);
        Vector3 pos_3 = centerpositon + new Vector3(5, -5, 0);
        Vector3 pos_4 = centerpositon + new Vector3(-5, 5, 0);
        Vector3 pos_5 = centerpositon + new Vector3(-5, 0, 0);
        Vector3 pos_6 = centerpositon + new Vector3(-5, -5, 0);
        lstPos.Add(pos_1);
        lstPos.Add(pos_2);
        lstPos.Add(pos_3);
        lstPos.Add(pos_4);
        lstPos.Add(pos_5);
        lstPos.Add(pos_6);
    }

    
}
