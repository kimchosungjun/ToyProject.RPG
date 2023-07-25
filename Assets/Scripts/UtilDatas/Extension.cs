using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        UIBase.AddUIEvent(go, action, type);
    }
}