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
       /* for (int i = 0; i < 5; i++)
        {
            MasterManager.Resource.Instantiate("Player");
        }*/
    }

    public override void Clear()
    {
       
    }

}
