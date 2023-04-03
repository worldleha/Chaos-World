using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 相机目标跟随玩家位置，不旋转（相对）
/// 指针相对移动
/// </summary>
public class TargetSimpleFollow : MonoBehaviour
{
    private CharacterKinematic characterKinematic;
    private CinemachineFreeLook cinemachineFreeLook;

    [Range(0,10)]
    public float screenMoveDistance;
    [Range(0,1)]
    public float maxScreenMoveAngle;

    private Vector3 addition;
    // Update is called once per frame
    public bool isActive = false;

    public void Init(PlayerControl playerControl, CinemachineFreeLook cf)
    {
        addition = Vector3.zero;
        cinemachineFreeLook = cf;
        characterKinematic = playerControl.CK;
        isActive = true;
     
    }
    private void Update()
    {
        if (!isActive) return;
        transform.rotation = characterKinematic.gravityRotation;
        transform.position = characterKinematic.transform.position + addition;
    }

    public void MouseMoveTarget(Vector2 mousePosition)
    {
        
        if (cinemachineFreeLook.m_YAxis.Value > maxScreenMoveAngle)
        {
            Vector3 newAddition;
            mousePosition -= (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            mousePosition /= Screen.width;
            mousePosition *= cinemachineFreeLook.m_YAxis.Value * screenMoveDistance;
            newAddition = Operator.Vector2ToVector3XZ(mousePosition);
            addition = characterKinematic.gravityRotation * Quaternion.Euler(0, cinemachineFreeLook.m_XAxis.Value, 0) * newAddition;
        }
        else
        {
            addition = Vector3.zero;
        }
    }

}
