using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 角色更新的最高级
/// </summary>
/// 
[Serializable]
public class CharacterData : MonoBehaviour
{
    // characterName == prefab name
    public string characterName = "None";
    public float health = 100;
    public float maxHealth = 100;

    public float shield = 1000;
    public float maxShield = 1000;

    public float mana = 100;
    public float maxMana = 100;

    public float temperature = 37;

    public float physicalDamage = 10;
    public float magicDamage = 10;

    public float strength = 100;
    public float maxStrength = 100;

    public float armor = 10;
    public float magicResistance = 10;

    //%
    public float moveSpeed = 1;
    public float handSpeed = 1;
    public float magicalCrit = 1.5f;
    public float physicalCrit = 1.5f;
    public float recuperation = 0.5f;
    public float temperatureReduce = 10f;


    //Vector3
    public Vector3 position = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    //Quaternion
    public Quaternion rotation = Quaternion.identity;

    public void Init(SerCharacterData data)
    {
        
        health = data.health;
        maxHealth = data.maxHealth;
        shield = data.shield;
        maxShield = data.maxShield;
        mana = data.mana;
        maxMana = data.maxMana;
        temperature = data.temperature;
        physicalDamage = data.physicalDamage;
        magicDamage = data.magicDamage;
        strength = data.strength;
        maxStrength = data.maxStrength;
        armor = data.armor;
        magicResistance = data.magicResistance;
        physicalCrit = data.physicalCrit;
        magicalCrit = data.magicalCrit;
        recuperation = data.recuperation;
        temperatureReduce = data.temperatureReduce;
        moveSpeed = data.moveSpeed;
        handSpeed = data.handSpeed;
        position = data.position.Vector_3;
        velocity = data.velocity.Vector_3;
        rotation = data.rotation.Quaternion_4;

    }

    public void UpdateData()
    {
        Character character = GetComponent<Character>();

        health = character.CS.Health;
        maxHealth = character.CS.MaxHealth;
        shield = character.CS.Shield;
        maxShield = character.CS.MaxShield;
        mana = character.CS.Mana;
        maxMana = character.CS.MaxMana;
        temperature = character.CS.Temperature;
        physicalDamage = character.CS.PhysicalDamage;
        magicDamage = character.CS.MagicDamage;
        strength = character.CS.Strength;
        maxStrength = character.CS.MaxStrength;
        armor = character.CS.Armor;
        magicResistance = character.CS.MagicResistance;
        physicalCrit = character.CS.PhysicalCrit;
        magicalCrit = character.CS.MagicalCrit; 
        recuperation = character.CS.Recuperation;
        temperatureReduce = character.CS.TemperatureReduce;

        moveSpeed = character.CS.MoveSpeed;
        handSpeed = character.CS.HandSpeed;

        position = character.CK.RB.position;
        velocity = character.CK.RB.velocity;
        rotation = character.CK.targetSelfYRotation;
    }
}
