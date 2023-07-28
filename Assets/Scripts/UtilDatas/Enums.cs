using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Layer
{
    Player=3,
    Ground=6,
    Monster=7,
    Block=8,
}

public enum CameraMode
{
    QuarterView
}

public enum MouseEvent
{
    Press,
    Click
}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Skill,
    Hit,
    Dead
}

public enum Buttons
{
    PointBtn,
}

public enum Texts
{
    PointText,
    ScoreText
}

public enum Images
{
    Image,
}

public enum GameObjects
{
    TestObject,
    GridPanel
}

public enum UIEvent
{
    Click,
    Drag,
}

public enum InvenItem
{
    ItemIcon,
    ItemNameText,
}

public enum Scene
{
    Default,
    Login,
    Lobby,
    Game
}

public enum Sound
{
    Bgm,
    Effect,
    MaxCound,
}