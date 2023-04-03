using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画管理
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterKinematic))]


public class HumanoidAnimation : CharacterAnimation
{

    protected bool isHanded;
    protected bool isRun;

    protected bool isJump;

    protected bool leftHandUp;
    protected bool rightHandUp;

    protected float maxYJumpSpeed;

    protected float targetPercentMoveSpeed;
    protected float currentPercentMoveSpeed;

    protected float changeRoughness;
    protected float currentActionType;
    protected float targetActionType;

    // Start is called before the first frame update
    public new void Init()
    {
        base.Init();

        isRun = false;
        isJump = false;
        isHanded = false;

        //当前动作类型
        currentActionType = targetActionType = 0;
        //平滑过渡
        moveRoughness = 0.1f;
        changeRoughness = 0.1f;

        currentPercentMoveSpeed = targetPercentMoveSpeed = 0f;

        AddListener();
    }


    /// <summary>
    /// 对动画添加事件
    /// </summary>
    private void AddListener()
    {
        AddAnimationEvent("NormalAttack1", "CancelAttack", "80%");
        AddAnimationEvent("NormalAttack2", "CancelAttack", "80%");
        AddAnimationEvent("NormalAttack3", "CancelAttack", "80%");
    }

    public bool RightHandUp
    {
        get { return rightHandUp; }
        set { rightHandUp = value; animator.SetBool("RightHandUp", value); }
    }
    public bool LeftHandUp
    {
        get { return leftHandUp; }
        set { leftHandUp = value; animator.SetBool("LeftHandUp", value); }
    }
    public void HandControl(bool isUp)
    {
        if (isUp)
        {
            if (isHanded)
            {
                RightHandUp = true;
                LeftHandUp = false;
            }
            else
            {
                LeftHandUp = true;
                RightHandUp = false;
            }
        }
        else
        {
            RightHandUp = false;
            LeftHandUp = false;
        }

    }
    

    /// <summary>
    /// 是否手持
    /// </summary>
    public bool IsHanded
    {
        get
        {
            return isHanded;
        }
        set
        {
            isHanded = value;
            animator.SetBool("IsHanded", value);
        }
    }

    /// <summary>
    /// 改变角色行动
    /// </summary>
    public void ChangeCharacterAction()
    {
        
        currentActionType = Mathf.Lerp(currentActionType, targetActionType, changeRoughness);
        animator.SetFloat("ActionType", currentActionType);
    }

    // Update is called once per frame
    private new void FixedUpdate()
    {
        if (!isActive) return;
        Move();
        Jumping();
        ChangeCharacterAction();
    }

    public new bool IsAttack
    {
        get { return isAttack; }
    }

    public new void NormalAttack(int type)
    {

        isAttack = true;

        if (type == 1) animator.SetTrigger("Attack1");
        else if (type == 2) animator.SetTrigger("Attack2");
        else if (type == 3) animator.SetTrigger("Attack3");

    }
    public new void CancelAttack()
    {
        isAttack = false;
    }


    public void SetAnimationMoveSpeed(float speed)
    {
        animator.SetFloat("MoveSpeed", speed);
    }

    /// <summary>
    /// 开始跳跃
    /// </summary>
    public void Jump()
    {

        if (characterKinematic.isGround && !isJump)
        {
            targetActionType = 1;
            isJump = true;
            characterKinematic.Jump();
            moveRoughness = 0.12f;
            maxYJumpSpeed = 0;
        }
    }

    /// <summary>
    /// 跳跃中
    /// </summary>
    protected void Jumping()
    {
        if (isJump)
        {
            
            float velocityY = Vector3.Dot(characterKinematic.RB.velocity, transform.up);
            maxYJumpSpeed = Mathf.Max(maxYJumpSpeed, velocityY);
            animator.SetFloat("JumpState", velocityY / maxYJumpSpeed);
            
            if (velocityY < -0.1f && characterKinematic.IsGrounded())
            {
                isJump = false;
                moveRoughness = 0.05f;
             
                targetActionType = 0;
            }
        }
    }


    /// <summary>
    /// 是否跑
    /// </summary>
    /// <param name="_isRun"></param>
    public void Run(bool _isRun)
    {
        isRun = _isRun;
    }

    /// <summary>
    /// 移动中
    /// </summary>
    protected new void Move()
    {

        if (IsMove && !isJump)
            targetPercentMoveSpeed = isRun ? 1f : 0.5f;
        else
            targetPercentMoveSpeed = 0;

        v2CurrentSpeed = Vector2.Lerp(v2CurrentSpeed, v2TargetSpeed, moveRoughness);
        currentPercentMoveSpeed = Mathf.Lerp(currentPercentMoveSpeed, targetPercentMoveSpeed, moveRoughness);
        animator.SetFloat("EmptyHandedSpeed", currentPercentMoveSpeed);
        Vector2 speed = v2CurrentSpeed * currentPercentMoveSpeed;
        animator.SetFloat("HandedXSpeed", speed.x);
        animator.SetFloat("HandedYSpeed", speed.y);

    }



    /// <summary>
    /// 角色移动控制权转到 characterKinematic
    /// </summary>
    private void OnAnimatorMove()
    {
        if (!isActive) return;
        Vector3 velocity = animator.velocity;
        //保证角色空中移动
        if (!characterKinematic.IsGrounded())
            characterKinematic.SetAnimationVelocity(Operator.IgnoreSmallVelocityValue(velocity));
    }

}
