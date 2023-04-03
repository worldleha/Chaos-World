using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    private PrefabManager prefabManager;    
    // Start is called before the first frame update
    void Start()
    {
        if (GameSceneManager.Instance == null)
        {
            prefabManager = PrefabManager.GetPrefabManager();
            Instantiate(prefabManager.GetUIPrefabByName("GameMainSceneManager"));
            Instantiate(prefabManager.GetUIPrefabByName("SceneLoadingCanvas"));
        }
        else
        {
            GameSceneManager.Instance.LoadEnd();
        }
        //Debug.Log(GameSceneManager.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
