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
