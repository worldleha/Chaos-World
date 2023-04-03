using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ɫ��������
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(CharacterKinematic))]


public class PlayerAnimation : HumanoidAnimation
{
  
  



    private float rotateAngle;

    // Start is called before the first frame update
    public new void Init()
    {
        base.Init();
        rotateAngle = 0;
        AddListener();
        
    }


    /// <summary>
    /// �Զ�������¼�
    /// </summary>
    private void AddListener()
    {
        AddAnimationEvent("NormalAttack1", "CancelAttack", "80%");
        AddAnimationEvent("NormalAttack2", "CancelAttack", "80%");
        AddAnimationEvent("NormalAttack3", "CancelAttack", "80%");
    }

   
    public float RotateAngle
    {
        get { return rotateAngle; }
        set
        {
            rotateAngle = value;
            if (isHanded && !IsMove && !isJump && !isAttack)
            {
                if (rotateAngle == 0)
                {
                    animator.SetFloat("RotateSpeed", 1);
                    animator.SetInteger("RotateAngle", 0);
                }
                else
                {
                    animator.SetFloat("RotateSpeed", Mathf.Min(Mathf.Abs(rotateAngle) /20,1.2f));
                    animator.SetInteger("RotateAngle", rotateAngle > 0 ? 1 : -1);
                }

            }
            else
            {
                rotateAngle = 0;
                animator.SetInteger("RotateAngle", 0);
                animator.SetFloat("RotateSpeed", 1);
            }
 
        }
    }

   
    // Update is called once per frame
    private new void FixedUpdate()
    {
        if (!isActive) return;
        //����
        ChangeCharacterAction();
        Move();
        Jumping();
        
    }


  
    



    /// <summary>
    /// ��ɫ�ƶ�����Ȩת�� characterKinematic
    /// </summary>
    private void OnAnimatorMove()
    {
        if (!isActive) return;
        Vector3 velocity = animator.velocity;
        //��֤��ɫ�����ƶ�
        if(!isJump)
            characterKinematic.SetAnimationVelocity(Operator.IgnoreSmallVelocityValue(velocity));
    }

}
