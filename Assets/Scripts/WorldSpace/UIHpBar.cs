using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : UIBase
{
    enum GameObjectType
    {
        HpBar
    }
    Stat stat;
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjectType));
        stat = transform.parent.GetComponent<Stat>();
    }

    void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
        float ratio = stat.Hp / (float)stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObejct((int)GameObjectType.HpBar).GetComponent<Slider>().value = ratio;
    }
}
