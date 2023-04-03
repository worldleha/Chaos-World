using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : Item
{
    private float health;
    private float shealth;
    private float sahealth;
    private float rhealth;
    private float smana;
    private float samana;
    private float rmana;
    private float sshield;
    private float sashield;
    private float sastrength;
    private float sstrength;
    private float rstrength;
    private float satemperature;
    public float physicalDamage;
    public float magicDamage;

    public string naction;
    public string saction;



    public Instrument(int _id, string[] information) : base(_id, information)
    {
        health = GetFloatFromInformation(ItemInformation.Health);
        shealth = GetFloatFromInformation(ItemInformation.SHealth);
        sahealth = GetFloatFromInformation(ItemInformation.SAHealth);
        rhealth = GetFloatFromInformation(ItemInformation.RHealth);
        smana = GetFloatFromInformation(ItemInformation.SMana);
        samana = GetFloatFromInformation(ItemInformation.SAMana); 
        rmana = GetFloatFromInformation(ItemInformation.RMana);
        sshield = GetFloatFromInformation(ItemInformation.SShield);
        sashield = GetFloatFromInformation(ItemInformation.SAShield);
        sastrength = GetFloatFromInformation(ItemInformation.SAStrength);
        sstrength = GetFloatFromInformation(ItemInformation.SStrength);
        rstrength = GetFloatFromInformation(ItemInformation.RStrength);

        physicalDamage = GetFloatFromInformation(ItemInformation.PhysicalDamage);
        magicDamage = GetFloatFromInformation(ItemInformation.MagicDamage);

        naction = GetStringFromInformation(ItemInformation.NAction);
        saction = GetStringFromInformation(ItemInformation.SAction);
    }



    // Start is called before the first frame update

    // Update is called once per frame

}
