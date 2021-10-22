using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Canvas canvas;

    [Header("Input Value")]
    Transform firstPos;
    Transform target;

    [Header("Info")]
    public float speed = 100f;
    public float launchHeight = 100;

    [Header("Code Support")]
    Vector3 movePosition;
    float firstPos_fix;
    float target_fix;
    float distant;
    float baseX;
    float baseY;
    float height;

    bool beginfly;
    private void Awake()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
    }
    void Update()
    {
        // set up layer oder
        canvas.sortingOrder = Mathf.RoundToInt((transform.position.y - height) * 10f * -1);

        // Animation
        if (beginfly)
        {
            firstPos_fix = firstPos.position.x;
            target_fix = target.position.x;
            distant = target_fix - firstPos_fix;
            baseX = Mathf.MoveTowards(transform.position.x, target_fix, speed * Time.deltaTime);
            baseY = Mathf.Lerp(firstPos.position.y, target.position.y, (baseX - firstPos_fix) / distant);
            height = launchHeight * (baseX - firstPos_fix) * (baseX - target_fix) / (-0.25f * distant * distant);

            movePosition = new Vector3(baseX, baseY + height, transform.position.z);

            transform.rotation = LookAtTarget(movePosition - transform.position);
            transform.position = movePosition;

            if (movePosition == target.position)
            {
                gameObject.SetActive(false);
            }
        }
    }
    Quaternion LookAtTarget(Vector2 r)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(r.y, r.x) * Mathf.Rad2Deg);
    }
    public void SetupMissle(Transform firstPos, Transform target)
    {
        this.firstPos = firstPos;
        this.target = target;
        beginfly = true;
    }
}