using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIButton : UIBase
{
    int score = 0;
    void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        GetButton((int)Buttons.PointBtn).gameObject.AddUIEvent(ClickBtn);

        GameObject go = GetImage((int)Images.Image).gameObject;
        AddUIEvent(go, (PointerEventData data)=> { go.transform.position = data.position; }, UIEvent.Drag);
       
    }

    public void ClickBtn(PointerEventData data)
    {
        Debug.Log("클릭!");
        score+=1;
        Get<Text>((int)Texts.ScoreText).text = $"점수{score}";
    }
}
