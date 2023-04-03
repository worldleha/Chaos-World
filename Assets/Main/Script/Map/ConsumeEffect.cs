
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConsumeEffect :Object
{

    public EffectState LearnSkill(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj)
    {
        PlayerControl character = (PlayerControl)effectState.character;
        Skill skill = (Skill)effectState.item;

        character.CSK.skillBoard.AddSkill(skill);
        skill.SetSkillImage();
        return null;

    }
}
