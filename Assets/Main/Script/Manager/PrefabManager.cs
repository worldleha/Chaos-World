using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public List<GameObject> UIPrefabs;
    public List<GameObject> itemPrefabs;
    public List<GameObject> effectPrefabs;
    public List<GameObject> lives;
    public List<GameObject> planetItem;
    public List<GameObject> otherPrefabs;
    private static PrefabManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public static PrefabManager GetPrefabManager()
    {
        if (instance == null) throw new UnityException("enable layer should lower than awake");
        return instance;
    }

    public GameObject GetUIPrefabByName(string name)
    {
        foreach(var prefab in UIPrefabs)
        {
            if(prefab.name == name)
            {
                return prefab;
            }
        }
        return null;
    }

    public GameObject GetItemByName(string _name)
    {
        foreach (var prefab in itemPrefabs)
        {
            if (prefab.name == _name)
            {
                return prefab;
            }
        }
        return null;
    }
    public GameObject GetOtherPrefabByName(string _name)
    {
        foreach (var prefab in otherPrefabs)
        {
            if (prefab.name == _name)
            {
                return prefab;
            }
        }
        return null;
    }
    public GameObject GetItemById(int id)
    {
        if(id<= itemPrefabs.Count)
            return itemPrefabs[id-1];

        throw new System.Exception("id out of bound");
        
    }
    public GameObject GetEffectByName(string _name)
    {
        foreach (var prefab in effectPrefabs)
        {
            if (prefab.name == _name)
            {
               
                return prefab;
            }
        }
        return null;
    }
    public GameObject GetPlanetItemByName(string _name)
    {
        foreach (var prefab in planetItem)
        {
            if (prefab.name == _name)
            {

                return prefab;
            }
        }
        return null;
    }
    public GameObject GetLivesByName(string _name)
    {
        foreach (var prefab in lives)
        {
            if (prefab.name == _name)
            {

                return prefab;
            }
        }
        return null;
    }
}
