using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> dicObjects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    protected void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object // enum�� �޾ƿ��� ���� Type�� ����Ѵ�.
    {
        // ����Ƽ�� ���õ� ��� ������Ʈ���� UnityEngine.Object�� �޾ƿ� �� �ִ�.
        string[] names = Enum.GetNames(type); // �ش� Enum�� ���ڿ����� �迭�� �̾ƿ´�. 
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        dicObjects.Add(typeof(T), objects);
        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        if (dicObjects.TryGetValue(typeof(T), out UnityEngine.Object[] objects) == false)
            return null;
        return objects[idx] as T;
    }

    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected GameObject GetGameObejct(int idx) { return Get<GameObject>(idx); }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(go);
        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }       
    }
}
