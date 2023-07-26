using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScene : UIBase
{

    public override void Init()
    {
        MasterManager.UI.SetCanvas(gameObject, false);
    }
}
