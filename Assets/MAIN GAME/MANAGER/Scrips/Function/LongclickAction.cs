using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class LongclickAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    enum Type
    {
        Continuous,
        Once,
    }
    public Action action;

    [SerializeField] float LongClickTime = 0.3f;
    [SerializeField] Type type;
    bool clickable;
    bool beginclick;

    float cooldown = 0f;

    private void Awake()
    {
        switch (type)
        {
            case Type.Continuous:
                clickable = false;
                cooldown = 0f;
                break;
            case Type.Once:
                clickable = true;
                cooldown = LongClickTime;
                break;
        }
    }

    private void Update()
    {
        switch (type)
        {
            case Type.Continuous:
                if (beginclick)
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown <= 0)
                    {
                        clickable = true;
                    }
                    if (clickable)
                    {
                        Button();
                        clickable = false;
                        cooldown = LongClickTime;
                    }
                }
                break;
            case Type.Once:
                if (beginclick)
                {
                    cooldown -= Time.deltaTime;
                    if (cooldown <= 0 && clickable)
                    {
                        Button();
                        clickable = false;
                    }
                }
                break;
        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        beginclick = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        beginclick = false;
        switch (type)
        {
            case Type.Continuous:
                cooldown = 0;
                break;
            case Type.Once:
                clickable = true;
                cooldown = LongClickTime;
                break;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        beginclick = false;
        switch (type)
        {
            case Type.Continuous:
                cooldown = 0;
                break;
            case Type.Once:
                clickable = true;
                cooldown = LongClickTime;
                break;
        }
    }
    public void Button()
    {
        Debug.Log("Long Click");
        action?.Invoke();
    }
}