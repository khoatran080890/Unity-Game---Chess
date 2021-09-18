using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class BattleBlock : MonoBehaviour
{
    public int index;

    [Header("UI")]
    [SerializeField] TextMeshPro txt;
    [SerializeField] Color normalcolor = Color.white;
    [SerializeField] Color onmousovercolor = Color.green;

    private void Awake()
    {
        
    }
    private void Update()
    {
        UpdatePos();
        OnMouserOver();
    }
    #region Init
    void UpdatePos()
    {
        float map_width = BattleBlockManager.Instance.Get_Mapsize().y;
        float map_height = BattleBlockManager.Instance.Get_Mapsize().x;

        float block_width = BattleBlockManager.Instance.Get_Blocksize().y;
        float block_height = BattleBlockManager.Instance.Get_Blocksize().x;


        index = transform.GetSiblingIndex();
        if (index > (map_width * map_height - 1))
        {
            DestroyImmediate(gameObject);
        }
        name = index.ToString();
        txt.text = index.ToString();

        float x = block_width * (index % map_width);
        float y = block_height * (index - (index % map_width)) / map_width;

        transform.position = new Vector3(x - (map_width * block_width - 1) / 2, 0, y - (map_height * block_height - 1) / 2);
        transform.localScale = new Vector3(block_width * 0.9f, BattleBlockManager.Instance.Get_Blockheight_3D(), block_height * 0.9f);
    }
    #endregion

    #region Function
    #region Private
    void OnMouserOver()
    {
        if (BattleBlockManager.Instance.Get_CurrentBlock_mouseisover() == index)
        {
            SetColor(onmousovercolor);
        }
        else
        {
            SetColor(normalcolor);
        }
    }
    
    #endregion

    #region Public
    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }

    #endregion
    #endregion
}
