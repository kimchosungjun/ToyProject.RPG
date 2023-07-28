using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);
            GameObject go = MasterManager.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent =null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
            return null;
        if (original.GetComponent<Poolable>() != null)
            return MasterManager.Pool.Pop(original, parent).gameObject;
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable!=null)
        {
            MasterManager.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go);
    }
}
