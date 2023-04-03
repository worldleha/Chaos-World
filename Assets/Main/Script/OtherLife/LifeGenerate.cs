
using System;
using Unity.VisualScripting;
using UnityEngine;

public class LifeGenerate: MonoBehaviour 
{
    public GameObject lifeAssets;

    private PrefabManager prefabManager;

    Transform parent;

    private void Start()
    {
        prefabManager = PrefabManager.GetPrefabManager();
        parent = lifeAssets.transform;
    }
    public T RandomGenerateLife <T>(Planet planet,string name) where T : Character
    {

        
        GameObject obj =prefabManager.GetLivesByName(name);     
        T character = Generate<T>(obj, parent);
        character.GetComponent<CharacterData>().position = planet.GetRandomPos();
        return character;

    }

    public Character GenerateLife(SerCharacterData data)
    {
        string name = data.characterName;    
        GameObject obj = prefabManager.GetLivesByName(name);
        Character character = Generate<Character>(obj, parent);
        character.GetComponent<CharacterData>().Init(data);
        return character;
    }

    public static T Generate<T>(GameObject obj, Transform parent)
    {
        GameObject life = Instantiate(obj, parent);
        T character = life.GetComponent<T>();
        return character;
    }


}
