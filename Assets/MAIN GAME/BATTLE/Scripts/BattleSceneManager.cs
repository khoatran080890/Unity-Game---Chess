using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100000))
        {
            if (hit.collider.tag == GameConst.Tag.block)
            {
                int index = hit.collider.GetComponent<BattleBlock>().index;
                BattleBlockManager.Instance.Set_CurrentBlock_mouseisover(index);
            }
        }
        else
        {
            BattleBlockManager.Instance.Set_CurrentBlock_mouseisover(-1);
        }
    }
}
