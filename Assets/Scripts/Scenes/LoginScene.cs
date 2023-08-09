using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = Scene.Login;
        List<GameObject> list = new List<GameObject>();
        for(int i=0; i<5; i++)
            list.Add(MasterManager.Resource.Instantiate("Player"));
        foreach (GameObject obj in list)
            MasterManager.Resource.Destroy(obj);
        Debug.Log(MasterManager.Data.itemDict[100].Name);
    }
    public override void Clear()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            MasterManager.Scene.LoadScene(Scene.Game);
    }
}
