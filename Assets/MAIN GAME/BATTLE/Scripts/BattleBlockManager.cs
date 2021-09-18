using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BattleBlockManager : MonoBehaviour
{
    public static BattleBlockManager Instance;

    [Header("Init")]
    [SerializeField] Vector2 Mapsize;
    [SerializeField] Vector2 Blocksize;
    [SerializeField] float Blockheight_3D;

    [Header("Info")]
    [SerializeField] int currentblock_mouseisover = -1;

    [Header("Prefabs")]
    [SerializeField] GameObject BattleBlock;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Instance = this;
        if(transform.childCount < (Mapsize.x * Mapsize.y))
        {
            Instantiate(BattleBlock, transform);
        }
    }

    #region GET
    public Vector2 Get_Mapsize()
    {
        return Mapsize;
    }
    public Vector2 Get_Blocksize()
    {
        return Blocksize;
    }
    public float Get_Blockheight_3D()
    {
        return Blockheight_3D;
    }

    public int Get_CurrentBlock_mouseisover()
    {
        return currentblock_mouseisover;
    }
    #endregion
    #region Function
    public void Set_CurrentBlock_mouseisover(int index)
    {
        currentblock_mouseisover = index;
    }
    #endregion
}
