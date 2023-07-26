using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Scene SceneType { get; protected set; } = Scene.Default;
    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            MasterManager.Resource.Instantiate("UI/EventSystem");
    }
    public abstract void Clear();

}
