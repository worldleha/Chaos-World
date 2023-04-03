using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitState 
{

    public GameObject hitObject;
    public float hitTime;
    public string hitEffectName;
    public bool isHit;

    public HitState()
    {
        isHit = false;
    }

    public UnityAction<GameObject> EffectHit(string effectName) {
        
        void OnHitObject(GameObject obj)
        {
            this.hitObject = obj;
            this.hitTime = Time.time;
            this.hitEffectName = effectName;
        }
        return OnHitObject;
    }
    


}
