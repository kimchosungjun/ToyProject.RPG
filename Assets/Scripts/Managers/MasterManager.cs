using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    #region Core
    static MasterManager instance;
    static MasterManager Instance { get { return instance; } }
    ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.resource; } }
    InputManager _input = new InputManager();
    public static InputManager Input { get {return Instance._input; } }
    UIManager ui = new UIManager();
    public static UIManager UI {  get { return Instance.ui; } }
    SceneLoadManager scene = new SceneLoadManager();
    public static SceneLoadManager Scene { get { return Instance.scene; } }
    SoundManager sound = new SoundManager();
    public static SoundManager Sound { get { return Instance.sound; } }
    PoolManager pool = new PoolManager();

    DataManager data = new DataManager();
    public static DataManager Data { get { return Instance.data; } }
    public static PoolManager Pool {  get { return Instance.pool;  } }

    GameManager game = new GameManager();
    public static GameManager Game { get { return Instance.game; } }
    #endregion

    void Awake()
    {
        if (instance == null)
        {
            GameObject masterManager = GameObject.Find("MasterManager");
            if (masterManager == null)
            {
                masterManager = new GameObject { name = "MasterManager" };
                masterManager.AddComponent<MasterManager>();
            }
            DontDestroyOnLoad(masterManager);
            instance = masterManager.GetComponent<MasterManager>();
            //instance.sound.Init();
            instance.data.Init();
            instance.pool.Init();
        }    
    }

    void Update()
    {
        _input.EventUpdate();
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
