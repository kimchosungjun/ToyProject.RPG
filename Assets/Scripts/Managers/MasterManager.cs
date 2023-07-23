using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    static MasterManager instance;
    static MasterManager Instance { get { return instance; } }

    ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.resource; } }


    
    void Start()
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

}
