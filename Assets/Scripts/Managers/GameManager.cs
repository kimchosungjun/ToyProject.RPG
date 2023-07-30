using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    GameObject player;
    HashSet<GameObject> monsters = new HashSet<GameObject>();

    public GameObject GetPlayer() { return player; }

    public GameObject Spawn(WorldObject _type, string path, Transform parent = null)
    {
        GameObject go = MasterManager.Resource.Instantiate(path, parent);
        switch (_type)
        {
            case WorldObject.Player:
                player = go;
                break;
            case WorldObject.Monster:
                monsters.Add(go);
                break;
        }
        return go;
    }
    public WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return WorldObject.UnKnown;
        return bc.WorldObjectType;
    } 

    public void DeSpawn(GameObject go)
    {
        WorldObject type = GetWorldObjectType(go);
        switch (type)
        {
            case WorldObject.Player:
                if (player == go)
                    player = null;
                break;
            case WorldObject.Monster:
                if (monsters.Contains(go))
                    monsters.Remove(go);
                break;
        }
        MasterManager.Resource.Destroy(go);
    }
}
