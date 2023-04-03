using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalEffect : Object
{
    public EffectState HumanoidWeaponNormalAttack(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj)
    {
        if (effectState.character.CA.IsAttack) return null;
        Character character = effectState.character;
        Instrument instrument = (Instrument)effectState.item;
        HumanoidAnimation ha = (HumanoidAnimation)effectState.character.CA;
        if (effectState.tag != "NormalAttack") effectState.level = 1;
        if (Time.time - effectState.time > 2) effectState.level = 1;
        /*instrument.item3D.GetComponent<Selector>().action = delegate (GameObject obj)
        {
            if (obj != effectState.character.gameObject)
            {
                obj.GetComponent<Character>().CS.Damage(0, instrument.physicalDamage);
                instrument.item3D.GetComponent<Selector>().action = delegate (GameObject obj) { };

            }
                
        };
        */
        RaycastHit hit;
        bool isHit = Physics.SphereCast(character.transform.position , 0.5f,character.transform.forward, out hit, LayerMask.GetMask("Character"));
        if (isHit)
            hit.collider.gameObject.GetComponent<Character>().CS.Damage(0, instrument.physicalDamage * (1 + effectState.level / 3));
        ha.NormalAttack(effectState.level);
        effectState.time = Time.time;
        effectState.level++;
        if (effectState.level > 3) effectState.level = 1;
        effectState.power = 0;
        effectState.speed = 0;
        effectState.tag = "NormalAttack";
        return effectState;
    }
    public EffectState KnockAttack(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj)
    {

        Character self = effectState.character;
        SphereSelector sphereSelector = self.GetComponent<SphereSelector>();
        if (sphereSelector is null)
            sphereSelector = self.gameObject.AddComponent<SphereSelector>();

        sphereSelector.action = delegate(GameObject obj)
        {
            Character character = obj.gameObject.GetComponent<Character>();
            
            if (character is not null && (character != effectState.character))
            {
                Skill skill = (Skill)effectState.item;
                character.CS.Damage(0, skill.physicalDamage);
                Vector3 force = character.GetComponent<Collider>().bounds.center - effectState.character.GetComponent<Collider>().bounds.center;
                character.CK.RB.AddForce( force.normalized * character.CK.RB.mass, ForceMode.Impulse);
            }
            
        };
        return null;
    }
}
