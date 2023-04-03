using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ɫ��������
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(CharacterKinematic))]


public class SlamAnimation : CharacterAnimation
{
 
    protected bool isJump;

    private float maxYJumpSpeed;



    // Start is called before the first frame update
    protected new void Init()
    {
        base.Init();
        isJump = false;

        v2CurrentSpeed = v2TargetSpeed = Vector2.zero;
        AddListener();
    }

    /// <summary>
    /// �Զ�������¼�
    /// </summary>
    private void AddListener()
    {
   
    }

   
   private bool IsJump
    {
        get { return isJump; }
        set { 
            isJump = value;
            animator.SetBool("IsJump", value);
        }
    }
    


   
    // Update is called once per frame
    private new void FixedUpdate()
    {
        if (!isActive) return;
        //����
        Move();
        MoveToward();
    }



    public new void NormalAttack(int type)
    {


    }

 
    /// <summary>
    /// �ƶ���
    /// </summary>
    private void MoveToward()
    {
        if (CurrentMoveSpeed < 0.1f) IsJump = false;
        else
        {

            IsJump = true;
            characterKinematic.LookAt(Operator.Vector2ToVector3XZ(v2CurrentSpeed));
            characterKinematic.SetVelocityBaseSelfYRotate(CurrentMoveSpeed * Vector3.forward);
        }

    }




    /// <summary>
    /// ��ɫ�ƶ�����Ȩת�� characterKinematic
    /// </summary>
    private void OnAnimatorMove()
    {
        //characterKinematic.SetAnimationVelocity(animator.velocity);
    }

}
