using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色动画管理
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterKinematic))]


public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;
    protected CharacterKinematic characterKinematic;
    protected bool isAttack;


    protected Vector2 v2TargetSpeed;
    protected Vector2 v2CurrentSpeed;

    protected float moveRoughness;
    protected float moveSpeed = 1;

    public bool isActive;
    // Start is called before the first frame update
    public void Init()
    {
        moveRoughness = 0.1f;
        animator = GetComponent<Animator>();
        characterKinematic = GetComponent<CharacterKinematic>();
        isActive = true;

    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set 
        { 
            moveSpeed = value;
            animator.SetFloat("MoveSpeed", moveSpeed);
        }
    }
    public float CurrentMoveSpeed
    {
        get { return v2CurrentSpeed.magnitude; }
    }
    public float TargetMoveSpeed
    {
        get { return v2TargetSpeed.magnitude; }
    }
    public bool IsMove{
        get { return v2CurrentSpeed.magnitude > 0.1f; }
    }
    protected void FixedUpdate()
    {
        if (!isActive) return;
        Move();
    }
    /// <summary>
    /// 添加动画事件
    /// </summary>
    /// <param name="clipName">动画名称</param>
    /// <param name="eventFunctionName">事件方法名称</param>
    /// <param name="time">添加事件时间。百分比</param>
    protected void AddAnimationEvent(string clipName, string eventFunctionName, string time)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == clipName)
            {
                AnimationEvent _event = new AnimationEvent();
                _event.functionName = eventFunctionName;
                _event.time = (float.Parse(time.TrimEnd('%')) / 100) * clips[i].length;
                clips[i].AddEvent(_event);
                break;
            }
        }
        animator.Rebind();
    }
 

 
  
    public bool IsAttack
    {
        get { return isAttack; }
        set
        {
            isAttack = value;
        }
    }

    public void NormalAttack(int type)
    {
        IsAttack =true;
    }
    public void CancelAttack()
    {
       IsAttack=false;
    }



    protected void Move()
    {
        v2CurrentSpeed = Vector2.Lerp(v2CurrentSpeed, v2TargetSpeed, moveRoughness);
    }



    public void CharacterMove(Vector2 speed)
    {
        v2TargetSpeed = speed;
    }





}
