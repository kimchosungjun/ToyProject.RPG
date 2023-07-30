using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    int order = 10;
    Stack<UIPopup> popupStack = new Stack<UIPopup>();
    UIScene uiScene = null;

    public GameObject Root { 
        get 
        {
            GameObject root = GameObject.Find("UIRoot");
            if (root == null)
                root = new GameObject { name = "UIRoot" }; 
            return root; 
        } 
    }

    public void SetCanvas(GameObject go, bool sort =true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        if (sort)
            canvas.sortingOrder = order++;
        else
            canvas.sortingOrder = 0;
    }
    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UIBase
    {

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = MasterManager.Resource.Instantiate($"UI/WorldSpace/{name}");
        if (parent != null)
            go.transform.SetParent(parent);
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
        return Util.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name =null)where T : UIBase
    {
     
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = MasterManager.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);
        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UIScene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = MasterManager.Resource.Instantiate($"UI/Scene/{name}");
        T scene = Util.GetOrAddComponent<T>(go);
        uiScene = scene;  
        go.transform.SetParent(Root.transform);
        return scene;
    }

    public T ShowPopupUI<T>(string name = null) where T : UIPopup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject go = MasterManager.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        popupStack.Push(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

    public void ClosePopupUI(UIPopup popup)
    {
        if (popupStack.Count == 0)
            return;
        if (popupStack.Peek() != popup)
            return;
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;
        UIPopup popup = popupStack.Pop();
        MasterManager.Resource.Destroy(popup.gameObject);
        popup = null;
        order -= 1;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        uiScene = null;
    }
}
