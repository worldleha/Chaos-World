using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInstrument : MonoBehaviour
{

    protected Character character;
    public Transform hand;
    public Transform emptyHandedAttackPos;

    protected EffectMap effectMap;
    protected EffectState effectState;
    public List<HitState> hitStates;
    protected Instrument _targetInstrument;
    public UnityAction<bool> onHandStateChange;
    protected bool isHanded;

    public void Init()
    {
        character = GetComponent<Character>();
        effectMap = EffectMap.GetInstance();
        hitStates = new List<HitState>();
    }
    public Transform AttackPos
    {
        get
        {
            if (IsHanded) { return TargetInstrument.item3D.attackPos; }
            else return emptyHandedAttackPos;
        }
    }
    public Instrument TargetInstrument
    {
        get { return _targetInstrument; }
        set 
        {
            HideHandleObj();
            if (value is not null) 
            {
                _targetInstrument = value;
                ShowHandleObj();
            }
            else
            {
                _targetInstrument = null;
            }
            
            
        
        }   
    }

    public bool HasInstrument
    {
        get
        {
            return TargetInstrument is not null;
        }
    }
    public bool IsHanded
    {
        get
        {
            return isHanded;
        }
        set
        {
            isHanded = value;
            onHandStateChange(value);
        }
    }
    public GameObject HandleObj
    {
        get 
        {
            if (HasInstrument)
                return TargetInstrument.item3D.gameObject;
            else
                return null;
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="normal">决定普通还是特殊</param>
    public void Action(bool normal)
    {
        if (!IsHanded) return;
        if (character.CA.IsAttack) return;
        string action = normal ? TargetInstrument.naction : TargetInstrument.saction;
        EffectState nextEffectState;
        if (effectState is null)
            nextEffectState = new EffectState(character, TargetInstrument, 1, 1, 1);
        else
            nextEffectState = new EffectState(character, TargetInstrument, effectState, 1, 1);
        HitState hitState = new HitState();
        hitStates.Add(hitState);
        effectState = effectMap.GetEffect(action)(nextEffectState, hitStates, hitState.EffectHit(action));

    }


    public void ChangeHandedState()
    {
        if (isHanded) HideHandleObj();
        else ShowHandleObj();
    }


    /// <summary>
    ///  收起手持工具
    /// </summary>
    protected void HideHandleObj()
    {
        
        if (IsHanded)
        {
            hand.DetachChildren();
            PhysicsBase pb = HandleObj.GetComponent<PhysicsBase>();
            if(!pb.IsDrop)
            {
                HandleObj.SetActive(false);
                HandleObj.transform.position = Vector3.zero;
                HandleObj.transform.rotation = Quaternion.identity;
            }
            IsHanded = false;
        }
    }
    protected void ShowHandleObj()
    {
        if (!IsHanded && HasInstrument)
        {
            HandleObj.transform.SetParent(hand);
            HandleObj.transform.rotation = hand.rotation;
            HandleObj.transform.position = hand.position - TargetInstrument.item3D.handPos.position;
            HandleObj.SetActive(true);
            IsHanded = true;
        }
    }

}
