using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager 
{
    public Action playerMoveAction = null;
    public Action<MouseEvent> PlayerMouseMove = null;
    private bool pressed = false;

    public void EventUpdate()
    {
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


}
