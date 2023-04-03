using Cinemachine;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CameraControl cameraControl;
    private PrefabManager prefabManager;
    [HideInInspector]
    public Transform firstTarget;
    private GameObject cfObj;
    [HideInInspector]
    public CinemachineFreeLook cf;
    private GameObject cvObj;
    [HideInInspector]
    public CinemachineVirtualCamera cv;
    private GameObject thirdFollow;

    private static CameraManager cameraManager;
    public static CameraManager Instance
    {
        get 
        {
            if (cameraManager == null) throw (new Exception("Please GenerateCamera"));
            return cameraManager; 
        }
    }
    public void GenerateCamera()
    {
        prefabManager = PrefabManager.GetPrefabManager();
        cameraManager = this;

        thirdFollow = Instantiate(prefabManager.GetOtherPrefabByName("Third Follow Target"));
        cfObj = Instantiate(prefabManager.GetOtherPrefabByName("Third Person Camera"));
        cvObj = Instantiate(prefabManager.GetOtherPrefabByName("First Person Camera"));
        cv = cvObj.GetComponent<CinemachineVirtualCamera>();
        cf = cfObj.GetComponent<CinemachineFreeLook>();

        cameraControl = Camera.main.GetComponent<CameraControl>();  
       
    }

    public void InitCamera(PlayerControl playerControl)
    {
 
        firstTarget = playerControl.PR.firstPersonTarget.transform;
        cv.LookAt = cv.Follow = firstTarget;
        TargetSimpleFollow tf = thirdFollow.GetComponent<TargetSimpleFollow>();
        playerControl.targetSimpleFollow = tf;
        
        cf.Follow = thirdFollow.transform;
        cf.LookAt = thirdFollow.transform.GetChild(0);
        
        tf.Init(playerControl, cf);
        cameraControl.CameraInit(playerControl, cf, cv);


    }


}
