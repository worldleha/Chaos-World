using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance;
    public SceneLoading sceneLoading;
    float process = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoading = SceneLoading.Instance;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Awake()
    {
        instance = this;
    }
    public static GameSceneManager Instance
    {
        get { return instance; }
    }
    public void LoadStart()
    {
        sceneLoading.rotateTarget.gameObject.SetActive(true);
    }

    public void LoadEnd()
    {
        Invoke("SetTargetActive", 1f);
        
    }
    public void SetTargetActive()
    {
        sceneLoading.rotateTarget.gameObject.SetActive(false);
    }
    public void ChangeToMainScene()
    {
        StartCoroutine(LoadScene(1));
    }

    public void ChangeToUIScene()
    {
        StartCoroutine(LoadScene(0));
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        LoadStart();
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f) break;
            process = operation.progress;
            sceneLoading.ProcessTouch(process/2);
            yield return null;
        }
        process = 1f;
        sceneLoading.ProcessTouch(process/2);
        operation.allowSceneActivation = true;
        
    }
}
