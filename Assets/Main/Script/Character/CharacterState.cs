
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum States
{
    health = 0,
    maxHealth,
    shield,
    maxShield,
    mana,
    maxMana,
    temperature,
    physicalDamage,
    magicDamage,
    strength,
    maxStrength,
    armor,
    magicResistance,
    moveSpeed,
    handSpeed,
    magicalCrit,
    physicalCrit,
    recuperation,
    temperatureReduce
}
public class CharacterState : MonoBehaviour
{

    public float[] states;

    public CharacterData cd; 
    public UnityAction<float>[] onStateChange;
    public UnityAction onAnyStateChange;

    private bool isApplyAllState = false;
    private bool isLive = false;
    private bool isRecuperation = false;


    public const float temperatureNormal = 36.5f;
    public const float temperatureRange = 2;

    public UnityAction onDie;

    #region  Ù–‘
    public float Health{
        get { return states[(int)States.health]; }
        set
        {
            value = Mathf.Min(value, MaxHealth);
            states[(int)States.health] = value;
            onStateChange[(int)States.health](states[(int)States.health]);

        }
    }

    public float MaxHealth
    {
        get { return states[(int)States.maxHealth]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.maxHealth] = value;
            onStateChange[(int)States.maxHealth](states[(int)States.maxHealth]);

        }
    }
    public float Shield
    {
        get { return states[(int)States.shield]; }
        set
        {
            value = Mathf.Min(MaxShield, value);
            states[(int)States.shield] = value;
            foreach(var i in onStateChange) Debug.Log(i);
            onStateChange[(int)States.shield](states[(int)States.shield]);
        }
    }

    public float MaxShield
    {
        get { return states[(int)States.maxShield]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.maxShield] = value;
            onStateChange[(int)States.maxShield](states[(int)States.maxShield]);
        }
    }

    public float Temperature
    {
        get { return states[(int)States.temperature]; }
        set
        {
         
            states[(int)States.temperature] = value;
            onStateChange[(int)States.temperature](states[(int)States.temperature]);
        }
    }

    public float Strength
    {
        get { return states[(int)States.strength]; }
        set
        {

            value = Mathf.Min(value, MaxStrength);
            states[(int)States.strength] = value;
            onStateChange[(int)States.strength](states[(int)States.strength]);
        }
    }
    public float MaxStrength
    {
        get { return states[(int)States.maxStrength]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.maxStrength] = value;
            onStateChange[(int)States.maxStrength](states[(int)States.maxStrength]);
        }
    }

    public float Mana
    {
        get { return states[(int)States.mana]; }
        set
        {
            value = Mathf.Min(value, MaxMana);
            states[(int)States.mana] = value;
            onStateChange[(int)States.mana](states[(int)States.mana]); 
        }

    }
    public float MaxMana
    {
        get { return states[(int)States.maxMana]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.maxMana] = value;
            onStateChange[(int)States.maxMana](states[(int)States.maxMana]);
        }
    }
    public float PhysicalDamage
    {
        get { return states[(int)States.physicalCrit]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.physicalCrit] = value;
            onStateChange[(int)States.physicalDamage](states[(int)States.physicalCrit]);
        }
    }

    public float MagicDamage
    {
        get { return states[(int)States.magicDamage]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.magicDamage] = value;
            onStateChange [(int)States.magicDamage](states[(int)States.magicDamage]);
        }
    }

    public float Armor
    {
        get { return states[(int)States.armor]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.armor] = value;
            onStateChange[(int)(int)States.armor](states[(int)States.armor]);   
        }
    }

    public float MagicResistance
    {
        get { return states[(int)States.magicResistance]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.magicResistance] = value;
            onStateChange[(int)States.magicResistance](states[(int)States.magicResistance]);
        }
    }

    public float MoveSpeed
    {
        get { return states[(int)States.moveSpeed]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.moveSpeed] = value;
            onStateChange[(int)States.moveSpeed](states[(int)States.moveSpeed]);
        }
    }

    public float HandSpeed
    {
        get { return states[(int)States.handSpeed]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.handSpeed] = value;
            onStateChange[(int)States.handSpeed](states[(int)States.handSpeed]);
        }
    }

    public float MagicalCrit
    {
        get { return states[(int)States.magicalCrit]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.magicalCrit] = value;
            onStateChange[(int)States.magicalCrit](states[(int)States.magicalCrit]);
        }
    }
    public float PhysicalCrit
    {
        get { return states[(int)States.physicalCrit]; }
        set 
        {
            value = Mathf.Max(0, value);
            states[(int)States.physicalCrit] = value;
            onStateChange [(int)States.physicalCrit](states[(int)States.physicalCrit]);   
        }
    }

    public float Recuperation
    {
        get { return states[(int)States.recuperation]; }
        set 
        {

            states[(int)States.recuperation] = value;
            onStateChange[(int)States.recuperation](states[(int)States.recuperation]);
        }
    }

    public float TemperatureReduce
    {
        get { return states[(int)States.temperatureReduce]; }
        set
        {
            value = Mathf.Max(0, value);
            states[(int)States.temperatureReduce] = value;
            onStateChange[(int)States.temperatureReduce](states[(int)States.temperatureReduce]);
        }
    }

    #endregion
    public void Init()
    {
        this.enabled = true;
        states = new float[32];
        onStateChange = new UnityAction<float>[32];
        cd = GetComponent<CharacterData>();

        UnityAction<float> uaNull = (float u) => { };
        for(int i = 0; i < 32; i++) onStateChange[i] = uaNull;
        onDie = () => { };
        if (!isApplyAllState) ApplyAllState();
        //StartCoroutine(Ex());
        isLive = true;
        
    }

    private void Awake()
    {
        this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ApplyAllState()
    {
        ApplySingleState(States.health, cd.health);
        ApplySingleState(States.temperature, cd.temperature);
        ApplySingleState(States.temperatureReduce, cd.temperatureReduce);
        ApplySingleState(States.recuperation, cd.recuperation);
        ApplySingleState(States.maxHealth, cd.maxHealth);
        ApplySingleState(States.mana, cd.mana);
        ApplySingleState(States.moveSpeed, cd.moveSpeed);
        ApplySingleState(States.handSpeed, cd.handSpeed);
        ApplySingleState(States.recuperation, cd.recuperation);
        ApplySingleState(States.strength, cd.strength);
        ApplySingleState(States.maxStrength, cd.maxStrength);
        ApplySingleState(States.physicalDamage, cd.physicalDamage);    
        ApplySingleState(States.physicalCrit, cd.physicalCrit);
        ApplySingleState(States.shield, cd.shield);    
        ApplySingleState(States.maxShield, cd.maxShield);
        ApplySingleState(States.armor, cd.armor);
        ApplySingleState(States.magicalCrit, cd.magicalCrit);
        ApplySingleState(States.magicDamage, cd.magicDamage);
        ApplySingleState(States.magicResistance,cd.magicResistance);


    }
    public void ApplySingleState(States stateSign, float state)
    {
        states[(int)stateSign] = state;
    }
    public void touch()
    {
        if (!isApplyAllState) ApplyAllState();
        for(int i = 0; i < states.Length; i++)
        {
            if (onStateChange[i] is not null)
            {
                onStateChange[i](states[i]);
            }
        }
       
    }
    void PerSecond()
    {

        if (isRecuperation)
        {
            Health += MaxHealth * Recuperation;
            Health = Mathf.Min(Health, MaxHealth);
        }


    }
    public void Die() {
        isLive = false;
        Destroy(gameObject);  
        onDie();
    }

    public void Damage(float _magicDamage, float _physicalDamage)
    {
        float pDamageReduce = Armor * 0.08f / (1 + 0.08f * Armor);
        float mDamageReduce = MagicResistance * 0.07f / (1 + 0.07f * MagicResistance);
        float sub = _magicDamage * (1 - mDamageReduce) + _physicalDamage * (1 - pDamageReduce);
        Health -= sub;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }
    IEnumerator Ex()
    {
        yield return new WaitForSeconds(1);
        while (isLive)
        {
            
            PerSecond();
            yield return new WaitForSeconds(1);
        }
    }
}
