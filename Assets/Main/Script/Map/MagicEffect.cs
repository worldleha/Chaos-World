using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagicEffect:Object
{
    public EffectState WaterBall(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj)
    {
        Character character = effectState.character;
        Skill skill = (Skill)effectState.item;
        GameObject effect = PrefabManager.GetPrefabManager().GetEffectByName("WaterBall");

        effect =Instantiate(effect, null);

        effect.transform.position = character.CI.AttackPos.position;
        BulletSelector bs = effect.AddComponent<BulletSelector>();
        bs.SetPower(effectState.power);
        PlayerControl pc = (PlayerControl)effectState.character;
        if (pc is not null)
            bs.SetSpeed(pc.AttackDirection * Mathf.Log(effectState.speed, 2));
        else
            bs.SetSpeed(character.AttackDirection * Mathf.Log(effectState.speed, 2));
        bs.action += delegate (GameObject obj)
        {
            Character character = obj.GetComponent<Character>(); 
            if (character is not null && (character != effectState.character))
            {
                character.CS.Damage(skill.magicDamage, skill.physicalDamage);
            }
        };
        return null;

    }

    public EffectState FireBall(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj)
    {

        return null;
    }

}
