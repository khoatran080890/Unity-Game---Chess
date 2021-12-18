using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearItem : Singleton<WearItem>
{
    void AddLimb(GameObject target, GameObject mainchar)
    {
        SkinnedMeshRenderer[] targetbones = target.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        // Get bone from target
        foreach (SkinnedMeshRenderer targetbone in targetbones)
        {
            SetupBone(targetbone, mainchar);
            Destroy(FindChildByName(targetbone.gameObject.name, mainchar.transform).gameObject);
        }
    }
    void SetupBone(SkinnedMeshRenderer targetbone, GameObject mainchar)
    {
        // new BoneObj
        GameObject newfakebone = new GameObject(targetbone.gameObject.name);
        newfakebone.transform.parent = mainchar.transform;
        //newfakebone.transform.parent = FindChildByName(targetbone.gameObject.name, mainchar.transform).parent;
        SkinnedMeshRenderer skinmesh = newfakebone.AddComponent<SkinnedMeshRenderer>();
        // get structure from mainchar
        Transform[] MyBones = new Transform[targetbone.bones.Length];
        for (var i = 0; i < targetbone.bones.Length; i++)
        {
            Transform transform = FindChildByName(targetbone.bones[i].name, mainchar.transform);
            MyBones[i] = transform;
        }
        /*      Assemble Renderer       */
        skinmesh.bones = MyBones;
        skinmesh.sharedMesh = targetbone.sharedMesh;
        skinmesh.materials = targetbone.materials;
    }
    Transform FindChildByName(string name, Transform maincharTranform)
    {
        Transform ReturnObj;
        if (maincharTranform.name == name)
        {
            return maincharTranform.transform;
        }
        foreach (Transform child in maincharTranform)
        {
            ReturnObj = FindChildByName(name, child);
            if (ReturnObj)
                return ReturnObj;
        }
        return null;
    }
}
