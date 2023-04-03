using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 角色的技能释放类
/// </summary>
public class CharacterSkill : MonoBehaviour
{
    protected Transform selfAttackPos;
    public SkillBoard skillBoard;
    protected EffectMap effectMap;

    protected EffectState effectState;
    protected Character character;
    protected float power;
    protected float speed;
    public Skill targetSkill;

    protected bool isActive = false;

 

    public void Init()
    {
        effectMap = EffectMap.GetInstance();
        character = GetComponent<Character>();
        skillBoard.onTargetSkillChange += OnTargetSkillChange;
        isActive = true;
    }
   
    public void Emission()
    {
        EffectState _effectState;
        if (effectState == null)
            _effectState = new EffectState(character, targetSkill, 1, power, speed);
        else
            _effectState = new EffectState(character, targetSkill, effectState, power, speed);
        effectState = effectMap.GetEffect(targetSkill.saction)(_effectState, null,null);

    }
    /// <summary>
    /// 当目标技能更换时
    /// </summary>
    public void OnTargetSkillChange()
    {
        targetSkill = skillBoard.TargetSkill;
    }
}
