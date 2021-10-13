using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Input Manager")]
    [SerializeField] Text txt;
    [SerializeField] float moveSpeed = 2.0f;

    [Header("Action")]
    public Action<InputAction.CallbackContext> _OnMovement;
    public Action<InputAction.CallbackContext> _OnJump;
    public Action<InputAction.CallbackContext> _OnAttack;


    Vector3 rawinputmovent;

    # region Event Function
    private void Awake()
    {
        _OnJump += OnJump;
        _OnAttack += OnAttack;
    }
    void Update()
    {
        transform.Translate(rawinputmovent);
        if (Input.GetKeyDown(KeyCode.M))
        {

        }
    }
    #endregion


    #region CALLBACK
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 input = value.ReadValue<Vector2>();
        rawinputmovent = new Vector3(input.x, 0, input.y) * moveSpeed * Time.deltaTime;
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            txt.text += "Jump-";
            Debug.Log("Jump");
        }
    }
    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            txt.text += "Attack-";
            Debug.Log("Attack");
        }
    }
    #endregion

    


}
