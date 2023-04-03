
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public delegate EffectState Effect(EffectState effectState, List<HitState> hitStates, UnityAction<GameObject> hitObj) ;
public class EffectMap : MonoBehaviour
{
    //这里可改为可选参数
    private NormalEffect ne;
    private SpecialEffect se;
    private MagicEffect me;
    private ConsumeEffect ce;
    private static EffectMap instance;
    
    private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();

    private void Awake()
    {
        ne = new NormalEffect();
        se = new SpecialEffect();
        me = new MagicEffect();
        ce = new ConsumeEffect();
        instance = this;
        LoadEffect(typeof(NormalEffect), ne);
        LoadEffect(typeof(MagicEffect),me);
        LoadEffect(typeof(SpecialEffect),se) ;
        LoadEffect(typeof(ConsumeEffect),ce);
    }
    public Effect GetEffect(string effectName)
    {
        return effects[effectName];
    }
    public static EffectMap GetInstance()
    {
        return instance;
    }
    private void LoadEffect(System.Type type, Object obj)
    {
        MethodInfo[] methodsInfo =
           type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (MethodInfo method in methodsInfo)
        {

            Effect effect = (Effect)method.CreateDelegate(typeof(Effect), obj);
   
            if (effect != null)
                effects[method.Name] = effect;
        }
    }


    
}
