using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class SceneLoadManager 
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); }  } 
    

    public void LoadScene(Scene scene)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(scene));
    }

    string GetSceneName(Scene scene)
    {
        string name = Enum.GetName(typeof(Scene),scene);
        return name;
    }
}
