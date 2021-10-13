using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAction : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    public Action OnSwipeUp_Evt;
    public Action OnSwipeDown_Evt;
    public Action OnSwipeRight_Evt;
    public Action OnSwipeLeft_Evt;

    public float SWIPE_THRESHOLD = 20f;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }
            ////Detects Swipe while finger is still moving
            //if (touch.phase == TouchPhase.Moved)
            //{
            //    if (!detectSwipeOnlyAfterRelease)
            //    {
            //        fingerDown = touch.position;
            //        checkSwipe();
            //    }
            //}
            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }
        else if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }
        else
        {
            Debug.Log("No Swipe!");
        }
    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
        OnSwipeUp_Evt.Invoke();
    }

    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
        OnSwipeDown_Evt.Invoke();
    }

    void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
        OnSwipeLeft_Evt.Invoke();
    }

    void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
        OnSwipeRight_Evt.Invoke();
    }
}