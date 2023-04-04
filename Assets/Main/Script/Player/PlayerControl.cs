
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerSkill))]
[RequireComponent(typeof(PlayerInstrument))]
/// <summary>
/// ��ɫ������ ������ƽ�ɫ�ű�
/// </summary>
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerControl : Character
{

    private CameraControl cameraControl;
    public TargetSimpleFollow targetSimpleFollow;
    protected PlayerRotation playerRotation;

    protected PlayerInstrument playerInstrument;
    protected PlayerSkill playerSkill;
    protected PlayerAnimation playerAnimation;

    public BackpackContainer backpackContainer;
    public EquipmentContainer equipmentContainer;
    public HandpickContainer handpickContainer;

    private bool isThirdPerson;


    public new void GetAllComponent()
    {
        base.GetAllComponent();

        playerRotation = GetComponent<PlayerRotation>();
        playerInstrument = GetComponent<PlayerInstrument>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerSkill = GetComponent<PlayerSkill>();
        handpickContainer = GameObject.Find("MainCanvas").GetComponentInChildren<HandpickContainer>();
        cameraControl = Camera.main.GetComponent<CameraControl>();  

    }

    public new void InitComponent()
    {
        PS.Init();
        PR.Init();
        PI.Init();
        CK.Init();
        CS.Init();
        PA.Init();
        CSK.Init();
        otherCharacterState.Init();
    }

    public new void Init()
    {

        GetAllComponent();
        InitComponent();
        isThirdPerson = true;
        AddAllListener();

        handpickContainer = GameObject.Find("MainCanvas").GetComponentInChildren<HandpickContainer>();
        equipmentContainer = PanelManager.Instance.equipmentContainer;
        backpackContainer = PanelManager.Instance.backpackContainer;
        backpackContainer.SetAllItemSpaceCharacter(this);
        equipmentContainer.SetAllItemSpaceCharacter(this);
        handpickContainer.SetAllItemSpaceCharacter(this);
        isActive = true;
        
    }

    public PlayerAnimation PA { get { return playerAnimation; } }
    public PlayerInstrument PI { get { return playerInstrument; } }
    public PlayerSkill PS { get { return playerSkill; } }
   
    public PlayerRotation PR { get { return playerRotation; } }
    public new Vector3 AttackDirection
    {
        get
        {
            if (isThirdPerson)
            {
                return transform.forward;
            }
            else
            {
                return Camera.main.ScreenPointToRay(Operator.ScreenCenter).direction;
            }
        }
    }

    public float PlayerSpeed
    {
        get 
        {
            if (isMove)
            {
                if (isRun)
                {
                    cameraControl.FirstWalkShake(ShakeState.run);
                    return playerSpeed * 3.6f;
                }
                else
                {
                    cameraControl.FirstWalkShake(ShakeState.walk);
                    return playerSpeed*1.4f;
                }
            }
            else
            {
                cameraControl.FirstWalkShake(ShakeState.idle);
                return 0;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }

    protected new void AddAllListener()
    {
        base.AddAllListener();
        //��ɫ������  ���л��ֳ�״̬ʱ 
        PI.onHandStateChange += delegate (bool state)
        {
            // ���� ����״̬ ���� ��ת״̬
            PA.IsHanded = state;
            playerRotation.PlayerRotateControl(state);
        };

        

        CK.onSelfRotate += delegate (Quaternion q)
        {
            // ����ɫ�����Զ���ת״̬
            if (playerRotation.autoRotate && isThirdPerson)
            {
                //���� ������ת״̬
                float angle = Operator.isClockwise(characterKinematic.selfYRotation * Vector3.forward,
                    characterKinematic.targetSelfYRotation * Vector3.forward, transform.up);
                PA.RotateAngle = angle;
            }
        };

        CK.onSelfRotateComplete += delegate (Quaternion q)
        {
            PA.RotateAngle = 0;
        };


        CS.onStateChange[(int)States.moveSpeed] += delegate (float speed)
        {
            CA.MoveSpeed = speed;
        };

        CI.onHandStateChange += delegate (bool isHand)
        {
            if (!isHand)
                CS.MoveSpeed *= 1.5f;
            else
                CS.MoveSpeed /= 1.5f;
        };

        //PlayerInput pi = GetComponent<PlayerInput>();
        //pi.actions["MouseDelta"].started += InputMousePositionDelta;
    }
    
    //����
    public void PressStick(CallbackContext callbackContext)
    {
    
        // �û� C# �ű�����ע�ᰴ�� ʱ ɾ�� if(!isActive) return;
        if (!isActive) return;
        bool isStick = callbackContext.ReadValue<float>() > 0;
        PA.Run(isStick);
        if (isStick)
        {
            isRun = true;
        }
        else {
            isRun = false;
        }
        if(!isThirdPerson)
            CK.SetVelocityBaseSelfYRotate(Operator.Vector2ToVector3XZ(v2Speed * PlayerSpeed));

    }
    private Vector2 v2Speed = Vector2.zero;
    // �ƶ���ɫ
    public void InputMove(CallbackContext callbackContext)
    {
      
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Canceled)
        {
            isMove = false;
            PA.CharacterMove(Vector2.zero);
            CK.SetVelocityBaseSelfYRotate(Operator.Vector2ToVector3XZ(v2Speed * PlayerSpeed));
        }
        else
        {
            isMove = true;
            v2Speed = callbackContext.ReadValue<Vector2>();
            if (isThirdPerson)
            {
                playerRotation.RotatePlayer(v2Speed);
                PA.CharacterMove(v2Speed);
            }
            else
            {
                CK.SetVelocityBaseSelfYRotate(Operator.Vector2ToVector3XZ(v2Speed*PlayerSpeed));
            }
        }

    }

   
    public void InputJump(CallbackContext callbackContext)
    {
       
        if (!isActive) return;
        if (callbackContext.ReadValue<float>() > 0)
        {
            PA.Jump();
        }
    }

    public void InputSwitch(CallbackContext callbackContext)
    {
       
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
            {
                float value = callbackContext.ReadValue<float>();
                if (value != 0)
                {
                    PI.ChangeIntrument(value < 0);
                    
            }
            }
    }

    // �������λ��
   public void InputTargetPosition(CallbackContext callbackContext)
    {
        Debug.Log("Hello1");
        if (!isActive) return;
        Vector2 targetPosition = callbackContext.ReadValue<Vector2>();
        playerRotation.RotatePlayerBaseRay(targetPosition);
        targetSimpleFollow.MouseMoveTarget(targetPosition);
    }
    
    public void InputMousePositionDelta(CallbackContext callbackContext)
    {
        Debug.Log("Hello2s");
        if (!isActive) return;
        Vector2 delta = callbackContext.ReadValue<Vector2>();
        playerRotation.RotatePlayerDirectly(delta);
    }
    // c �� �л��ֳ� �� ����
    public void InputSwitchHandState(CallbackContext callbackContext) 
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
        {
            if (callbackContext.ReadValue<float>() > 0)
                PI.ChangeHandedState();
        }
    }
    
    // ������
    public void InputNormalAction(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
            if (callbackContext.ReadValue<float>() > 0)
                PI.Action(true);
    }

    // ����Ҽ�
    public void InputSpecialAction(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
            if (callbackContext.ReadValue<float>() > 0)
                PI.Action(false);
    }

    public void InputSkill(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
        {
            if (callbackContext.ReadValue<float>() > 0)
            {

                switch (callbackContext.control.name) {
                    case "1":
                        PS.skillBoard.SetTargetSkill(1);
                        break;
                    case "2":
                        PS.skillBoard.SetTargetSkill(2);
                        break;
                    case "3":
                        PS.skillBoard.SetTargetSkill(3);
                        break;
                    case "4":
                        PS.skillBoard.SetTargetSkill(4);
                        break;
                    case "5":
                        PS.skillBoard.SetTargetSkill(5);
                        break;
                }
            }
        }
    }

    public void InputSkillEmissionQ(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
        {
            if(callbackContext.control.name == "q")
            {
                Debug.Log("QS");
                playerSkill.Q(true);
                playerSkill.E(false);
            }

        }
        else if(callbackContext.phase == InputActionPhase.Canceled)
        {
            if(callbackContext.control.name == "q")
            {
                playerSkill.Q(false);
                Debug.Log("QE");
            }

        }
    }
    public void InputChangeCamera(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
        {
            isThirdPerson = !isThirdPerson;
            cameraControl.ChangeCamera(isThirdPerson);



            playerRotation.isThirdPerson = isThirdPerson;

            

        }

    }

    public void InputSkillEmissionE(CallbackContext callbackContext)
    {
        if (!isActive) return;
        if (callbackContext.phase == InputActionPhase.Started)
        {
            if (callbackContext.control.name == "e")
            {
                Debug.Log("ES");
                playerSkill.E(true);
                playerSkill.Q(false);
            }
        }
        else if (callbackContext.phase == InputActionPhase.Canceled)
        {

            if (callbackContext.control.name == "e")
            {
                playerSkill.E(false);
                Debug.Log("EE");
            }
        }
    }
}
