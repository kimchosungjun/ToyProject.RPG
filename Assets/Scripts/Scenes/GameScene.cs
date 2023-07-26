using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        MasterManager.UI.ShowSceneUI<UIInven>();
        SceneType = Scene.Game;
    }

    public override void Clear()
    {
       
    }

}
