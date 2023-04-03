using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerCharacterData 
{
    // characterName == prefab name
    public string characterName;
    public float health;
    public float maxHealth;

    public float shield;
    public float maxShield;

    public float mana;
    public float maxMana;

    public float temperature;

    public float physicalDamage;
    public float magicDamage;

    public float strength;
    public float maxStrength;

    public float armor;
    public float magicResistance;

    //%
    public float moveSpeed;
    public float handSpeed;
    public float magicalCrit;
    public float physicalCrit;
    public float recuperation;
    public float temperatureReduce;


    //Vector3
    public SerVector3 position;
    public SerVector3 velocity;

    //Quaternion
    public SerQuaternion rotation;

    public SerCharacterData(CharacterData cd)
    {
        position = SerVector3.Zero;
        velocity = SerVector3.Zero;
        rotation = SerQuaternion.Zero;
        characterName = cd.characterName;
        health = cd.health;
        maxHealth = cd.maxHealth;
        shield = cd.shield;
        maxShield = cd.maxShield;
        mana = cd.mana;
        maxMana = cd.maxMana;
        temperature = cd.temperature;
        physicalDamage = cd.physicalDamage;
        magicDamage = cd.magicDamage;
        strength = cd.strength;
        maxStrength = cd.maxStrength;
        armor = cd.armor;
        magicResistance = cd.magicResistance;

        moveSpeed = cd.moveSpeed;
        handSpeed = cd.handSpeed;

        magicalCrit = cd.magicalCrit;
        physicalCrit = cd.physicalCrit;
        recuperation = cd.recuperation;
        temperatureReduce = cd.temperatureReduce;

        position.Set(cd.position);
        rotation.Set(cd.rotation);
        velocity.Set(cd.velocity);
    }

}
