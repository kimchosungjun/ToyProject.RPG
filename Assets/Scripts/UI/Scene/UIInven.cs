using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInven : UIScene
{

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
            MasterManager.Resource.Destroy(child.gameObject);
        for(int i=0; i< 8; i++)
        {
            GameObject item = MasterManager.UI.MakeSubItem<UIInvenItem>(gridPanel.transform).gameObject;
            UIInvenItem uiInvenItem = item.GetOrAddComponent<UIInvenItem>();
            uiInvenItem.SetItemInfo($"집행검 {i}번");
        }
    }
}
