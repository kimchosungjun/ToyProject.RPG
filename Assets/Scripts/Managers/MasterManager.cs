using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    static MasterManager instance;
    static MasterManager Instance { get { return instance; } }

    ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.resource; } }

    EventManager _event = new EventManager();
    public static EventManager Event { get {return Instance._event; } }
    UIManager ui = new UIManager();
    public static UIManager UI {  get { return Instance.ui; } }

    SceneLoadManager scene = new SceneLoadManager();
    public static SceneLoadManager Scene { get { return Instance.scene; } }
    
    void Awake()
    {
        if (instance == null)
        {
            GameObject masterManager = GameObject.Find("MasterManager");
            if (masterManager == null)
            {
                masterManager = new GameObject { name = "MasterManager" };
                masterManager.AddComponent<MasterManager>();
            }
            DontDestroyOnLoad(masterManager);
            instance = masterManager.GetComponent<MasterManager>();
        }    
    }

    void Update()
    {
        _event.EventUpdate();
    }
}
