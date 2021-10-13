using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform player;
    // Version 1
    [Tooltip("Area to touch for moving camera - if null defauld 1/2 right screen")]
    [SerializeField] RectTransform camera_rotate_area;
    [SerializeField] float camera_sensity;
    [SerializeField] float max_angle_up;
    [SerializeField] float max_angle_down;

    Vector2 vector_moving;
    float camera_rotation_x;

    int R_finger_id;
    // Changedistance
    [SerializeField] float distance;
    [SerializeField] float height;

    private void Start()
    {
        // set up camera pos
        camera.transform.localPosition = new Vector3(0, height, -distance);

        // set up left right pos
        R_finger_id = -1;
    }
    private void Update()
    {
        camera.transform.localPosition = new Vector3(0, height, -distance);

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (camera_rotate_area != null)
                    {
                        if (Mathf.Abs(touch.position.x - camera_rotate_area.localPosition.x - Screen.width / 2) < (camera_rotate_area.rect.width / 2)
                        && Mathf.Abs(touch.position.y - camera_rotate_area.localPosition.y - Screen.height / 2) < (camera_rotate_area.rect.height / 2)
                        && R_finger_id == -1)
                        {
                            R_finger_id = touch.fingerId;
                        }
                    }
                    else
                    {
                        if (touch.position.x > Screen.width / 2 && R_finger_id == -1)
                        {
                            R_finger_id = touch.fingerId;
                        }
                    }
                    
                    break;
                case TouchPhase.Moved:
                    if (touch.fingerId == R_finger_id)
                    {
                        vector_moving = touch.deltaPosition * camera_sensity * Time.deltaTime;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (touch.fingerId == R_finger_id)
                    {
                        vector_moving = Vector2.zero;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == R_finger_id)
                    {
                        R_finger_id = -1;
                    }
                    break;
            }
        }

        if (R_finger_id != -1)
        {
            camera_rotation_x = Mathf.Clamp(camera_rotation_x - vector_moving.y, -max_angle_up, max_angle_down);
            camera.localRotation = Quaternion.Euler(camera_rotation_x, 0, 0);

            player.Rotate(player.up, vector_moving.x);
        }
    }







}
