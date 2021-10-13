using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TEST : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 firstpos;
    private Vector2 secondpos;
    public Text debugtext;
    public void OnPointerClick(PointerEventData eventData)
    {
        firstpos = eventData.position;
        secondpos = eventData.position;
    }
    private void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        secondpos = eventData.position;
        RotateCamera(secondpos - firstpos);
        debugtext.text = (secondpos - firstpos).ToString();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        secondpos = eventData.position;
    }
    void RotateCamera(Vector2 vector)
    {
        Camera.main.transform.eulerAngles = new Vector3(transform.eulerAngles.x + vector.y, transform.eulerAngles.x + vector.y, 0);
    }
}
