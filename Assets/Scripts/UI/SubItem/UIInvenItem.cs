using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInvenItem : UIBase
{
    string itemName;

    public override void Init()
    { 
        Bind<GameObject>(typeof(InvenItem));
        Get<GameObject>((int)InvenItem.ItemNameText).GetComponent<Text>().text = itemName;
        Get<GameObject>((int)InvenItem.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"{itemName}Å¬¸¯"); });
    }

    public void SetItemInfo(string name)
    {
        itemName = name;
    }
}
