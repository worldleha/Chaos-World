using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectState
{ 
    public Item item;
    public Character character; 
    public int level;
    public float power;
    public float speed;
    public float time;
    public string tag;
    public bool complete;
    public EffectState(Character _character, Item _item, int level = 1, float power = 1, float speed = 1)
    {
        this.item = _item;
        this.character = _character;
        this.level = level;
        this.power = power;
        this.speed = speed;
        this.time = Time.time;
    }
    public EffectState(Character _character, Item _item, EffectState es, float power =1, float speed = 1)
    {
        this.item = _item;
        this.character = _character;
        this.power = power;
        this.speed = speed;
        FromESData(es);
        
    }

    public void FromESData(EffectState es)
    {
       
        this.level = es.level;
        this.tag = es.tag;
        this.time = es.time ;
        this.power *= es.power;
        this.speed *=es.speed;
    }
}
