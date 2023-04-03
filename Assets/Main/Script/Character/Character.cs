using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterKinematic))]
[RequireComponent(typeof(CharacterState))]
[RequireComponent (typeof(CharacterData))]
/// <summary>
/// 所有角色的基类
/// </summary>

public class Character : MonoBehaviour
{

    //public MeshFilter filter;

    protected CharacterSkill characterSkill;
    protected CharacterKinematic characterKinematic;
    protected CharacterAnimation characterAnimation;
    protected CharacterInstrument characterInstrument;
    protected CharacterState characterState;
    protected CharacterData characterData;
    protected OtherCharacterState otherCharacterState;
    protected float playerSpeed = 1;
    protected bool isMove = false;
    protected bool isRun = false;

    protected bool isActive = false;

    public void InitComponent()
    {
        CS.Init();
        CK.Init();
        CI.Init();
        CA.Init();
        CSK.Init();
        otherCharacterState.Init();
    }
    public void GetAllComponent()
    {
        characterKinematic = GetComponent<CharacterKinematic>();
        characterAnimation = GetComponent<CharacterAnimation>();
        characterInstrument = GetComponent<CharacterInstrument>();
        characterState = GetComponent<CharacterState>();
        characterSkill = GetComponent<CharacterSkill>();
        otherCharacterState = GetComponent<OtherCharacterState>();
        characterData = GetComponent<CharacterData>();  
        
    }


    public void Init()
    {
        GetAllComponent();
        InitComponent();
        AddAllListener();
        isActive = true;
    }

    protected void AddAllListener()
    {
        characterState.onStateChange[(int)States.health] += otherCharacterState.UpdateStateBar;
        characterState.onStateChange[(int)States.maxHealth] += otherCharacterState.UpdateStateMax;
        otherCharacterState.UpdateStateMax(characterState.MaxHealth);

    }
    public Vector3 AttackDirection{
        get{ 
         
            return transform.forward; 
        }
     }
    public CharacterData CD
    {
        get
        {
            return characterData;
        }
    }
    public CharacterAnimation CA
    {
        get { return characterAnimation; }
    }
    public CharacterKinematic CK
    {
        get { return characterKinematic; }
    }
    public CharacterInstrument CI
    {
        get { return characterInstrument; }
    }
    public CharacterState CS
    {
        get { return characterState; }
    }
    public CharacterSkill CSK
    {
        get { return characterSkill; }
    }

    public void ChangeHandedState()
    {
        characterInstrument.ChangeHandedState();
    }
}
