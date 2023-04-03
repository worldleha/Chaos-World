using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    public Transform rotateTarget;
    // Update is called once per frame
    public float mulSpeed;
    public float roughness = 0.1f;
    private float targetSpeed = 1;
    private float currentSpeed = 1;
    
    private static SceneLoading instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        //rotateTarget = this.transform.GetChild(0);

    }
    public static SceneLoading Instance
    {
        get
        {
            return instance;
        }
    }
    public void ProcessTouch(float process)
    {
        //Debug.Log(process);
        targetSpeed = 1-process;
    }
    void Update()
    {
        if (rotateTarget.gameObject.activeSelf)
            TargetRotate();
    }

        
          
  

     void TargetRotate()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, roughness);
        rotateTarget.rotation = Quaternion.Euler(0, 0, currentSpeed *mulSpeed*Time.deltaTime)* rotateTarget.rotation;
    }
}
