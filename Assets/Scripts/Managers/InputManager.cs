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
    private float pressedTime = 0f;

    public void EventUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // UI�� Ŭ���Ǹ� ������ �ȵ�
            return;
        if (Input.anyKey && playerMoveAction != null)
            playerMoveAction.Invoke();
        if(PlayerMouseMove != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!pressed)
                {
                    PlayerMouseMove.Invoke(MouseEvent.PointerDown);
                    pressedTime = Time.time;
                }
                PlayerMouseMove.Invoke(MouseEvent.Press);
                pressed = true;
            }
            else
            {
                if (pressed)
                {
                    if(Time.time<pressedTime+0.2f)
                        PlayerMouseMove.Invoke(MouseEvent.Click);
                    PlayerMouseMove.Invoke(MouseEvent.PointerUp);
                }
                pressedTime = 0f;
                pressed = false;
            }
        }
    }

    public void Clear()
    {
        PlayerMouseMove = null;
    }
}
