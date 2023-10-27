using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Scene.Game;
        gameObject.GetOrAddComponent<CursorController>();
        Dictionary<int, Data.Stat> dict = MasterManager.Data.statDict;
        gameObject.GetOrAddComponent<CursorController>();
     
        //GameObject player = MasterManager.Game.Spawn(WorldObject.Player, "Player");
        MasterManager.Game.Spawn(WorldObject.Monster, "Monster");
        //player.transform.position += Vector3.right * 20;
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        //MasterManager.Sound.Play("BGM/Default",Sound.Bgm,1,0.1f);
        //MasterManager.UI.ShowSceneUI<UIInven>();
        /* for (int i = 0; i < 5; i++)
         {
             MasterManager.Resource.Instantiate("Player");
         }*/
    }

    public override void Clear()
    {
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            MasterManager.Game.Spawn(WorldObject.Player, "Player");
    }
}
