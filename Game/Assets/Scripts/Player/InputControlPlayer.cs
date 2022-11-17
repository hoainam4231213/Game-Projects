using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class InputControlPlayer : BYSingleton<InputControlPlayer>
{
    public static Vector2 move;
    public static Vector2 look;
    public static bool aim;
    public PlayerInput playerInput;
    public bool uiControl;
    public bool isCurrentDeviceMouse
    {
        get
        {
          #if ENABLE_INPUT_SYSTEM
            return playerInput.currentControlScheme == "KeyBoardvsMouse";
          #else
           return false;
          #endif

        }
    }

    public void OnMove(InputValue inputValue)
    {
        Vector2 val = inputValue.Get<Vector2>();
        OnMoveInput(val);
    }

    public void OnAim(InputValue inputValue)
    {
        OnAimInput(inputValue.isPressed);
    }

    public void OnLook(InputValue inputValue)
    {
        Vector2 val = inputValue.Get<Vector2>();
        OnLookInput(val);
    }
    public void OnMoveInput(Vector2 value)
    {
        move = value;
    }
    public void OnLookInput(Vector2 value)
    {
        look = value;
    }
    public void OnAimInput(bool value)
    {
        aim = value;
    }

    public void OnApplicationFocus(bool focus)
    {
        if(!uiControl)
        Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
