using Cinemachine;
using UnityEngine;


public class PlayerRotation : MonoBehaviour
{

    public CinemachineFreeLook thirdPersonCamera;
    private CharacterKinematic characterKinematic;
    public GameObject firstPersonTarget;
    public GameObject firstPersonBody;
    private Ray ray;
    private Vector3 cameraRotation;

    public bool autoRotate;

    public bool isThirdPerson;
    // Start is called before the first frame update
    public void Init()
    {

        CameraManager cameraManager = CameraManager.Instance;
        thirdPersonCamera = cameraManager.cf;
        firstPersonTarget = transform.Find("chaos_character_hand/FirstCameraTarget").gameObject;
        firstPersonBody = firstPersonTarget.transform.parent.gameObject;
        cameraRotation = firstPersonTarget.transform.eulerAngles;
        characterKinematic = GetComponent<CharacterKinematic>();
        autoRotate = false;
        isThirdPerson = true;
    }

    public void RotatePlayerDirectly(Vector2 axisDelta)
    {

        
        if (!isThirdPerson)
        {
            
            axisDelta.x /= 10;
            axisDelta.y = -axisDelta.y / 20;
            cameraRotation.x += axisDelta.x;
            cameraRotation.y += axisDelta.y;
            cameraRotation.y = Mathf.Clamp(cameraRotation.y, -80, 80);
            characterKinematic.targetSelfYRotation *= Quaternion.Euler(0, axisDelta.x, 0);
            firstPersonTarget.transform.rotation = transform.rotation*Quaternion.Euler(cameraRotation.y*9/10, 0, 0);
            firstPersonBody.transform.rotation = transform.rotation  * Quaternion.Euler(cameraRotation.y*1/10, 0, 0);
        }
    }

    /// <summary>
    /// 键盘方向控制
    /// </summary>
    /// <param name="moveDirection"></param>
    public void RotatePlayer(Vector2 moveDirection)
    {
        if (isThirdPerson&&!autoRotate)
        {
            Vector3 direction = Operator.Vector2ToVector3XZ(moveDirection);
            float cameraAngleY = thirdPersonCamera.m_XAxis.Value;
            direction = Quaternion.Euler(0, cameraAngleY, 0) * direction;
            characterKinematic.LookAt(direction);
        }
    }

    /// <summary>
    /// 鼠标自动方向控制
    /// </summary>
    public void RotatePlayerBaseRay(Vector2 position)
    {
        if (isThirdPerson&&autoRotate)
        {
            
            ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider)
            {
                Vector3 direction = hit.point - transform.position;
                direction =characterKinematic.InverseXZRotation * direction;
                direction.y = 0;
                characterKinematic.LookAt(direction);
            }
        }
    }
    public void PlayerRotateControl(bool isHanded)
    {
        autoRotate = isHanded;

    }
    public void PlayerRotateType(bool isThird)
    {
        isThirdPerson = isThird;    
    }
}
