using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        Stack<Poolable> poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}Root";
            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;
            poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;
            if (poolStack.Count > 0)
                poolable = poolStack.Pop();
            else
                poolable = Create();
            poolable.gameObject.SetActive(true);
            if (parent == null) // Dondestroyonload ����
                poolable.transform.parent = MasterManager.Scene.CurrentScene.transform;
            poolable.transform.parent = parent;
            poolable.isUsing = true;
            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> pool = new Dictionary<string, Pool>();
    Transform root;
    public void Init()
    {
        if (root == null)
        {
            root = new GameObject { name = "PoolRoot" }.transform;
            Object.DontDestroyOnLoad(root);
           
        }
    }

    public void Push(Poolable poolable) // Pool�� �־��
    {
        string name = poolable.gameObject.name;
        if(pool.ContainsKey(name)==false)
        {
            GameObject.Destroy(poolable.gameObject);
            return; 
        }
        pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (pool.ContainsKey(original.name) == false)
            CreatePool(original);
        return pool[original.name].Pop(parent);
    }

    public void CreatePool(GameObject original, int count =5)
    {
        Pool _pool = new Pool();
        _pool.Init(original, count);
        _pool.Root.parent = root;
        pool.Add(original.name, _pool);
    }

    public GameObject GetOriginal(string name)
    {
        if (pool.ContainsKey(name) == false)
            return null;
        return pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in root)
            GameObject.Destroy(child.gameObject);
        pool.Clear();
    }
}
