
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public Cinemachine.CinemachineInputProvider cameraInput;
    public InputSystemUIInputModule canvasInput;
    // Start is called before the first frame update

    public void InputManagerInit(PlayerControl playerControl)
    {
        playerInput = playerControl.GetComponent<PlayerInput>();
        CameraManager cm = CameraManager.Instance;
        cameraInput = cm.cf.GetComponent<CinemachineInputProvider>();
    }

    public void UIEnable()
    {
        playerInput.enabled = false;
        cameraInput.enabled = false;
        canvasInput.enabled = true;
    }
    public void GameEnable()
    {
        cameraInput.enabled = true;
        playerInput.enabled = true;
        canvasInput.enabled = false;
    }

}
