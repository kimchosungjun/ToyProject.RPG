using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager 
{
    public Action playerMoveAction = null;
    public Action<MouseEvent> PlayerMouseMove = null;
    private bool pressed = false;

    public void EventUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // UI가 클릭되면 실행이 안됨
            return;
        if (Input.anyKey && playerMoveAction != null)
            playerMoveAction.Invoke();
        if(PlayerMouseMove != null)
        {
            if (Input.GetMouseButton(0))
            {
                PlayerMouseMove.Invoke(MouseEvent.Press);
                pressed = true;
            }
            else
            {
                if(pressed)
                    PlayerMouseMove.Invoke(MouseEvent.Click);
                pressed = false;
            }
        }
    }

    public void Clear()
    {
        PlayerMouseMove = null;
    }
}
