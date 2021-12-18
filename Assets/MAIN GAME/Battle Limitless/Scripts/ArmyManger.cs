using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyManger : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] ArmyManger ArmyManger_Player;
    [SerializeField] ArmyManger ArmyManger_Enemy;

    [Header("Info")]
    [SerializeField] RosterSide rostertype;
    [SerializeField] List<RosterManager> allroster = new List<RosterManager>();
    private void Awake()
    {
        allroster = transform.GetComponentsInChildren<RosterManager>().ToList();
    }

    #region GET INFO
    public List<RosterManager> Get_AllRoster()
    {
        return allroster;
    }
    public RosterSide Get_Rostertype()
    {
        return rostertype;
    }
    #endregion
}
