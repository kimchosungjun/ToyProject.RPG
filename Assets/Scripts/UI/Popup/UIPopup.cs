using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : UIBase
{
    public override void Init()
    {
        MasterManager.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        MasterManager.UI.ClosePopupUI(this);
    }
}
