using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : ItemContainer
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        container[0].tag = "RightEye";
        container[1].tag = "LeftEye";
        container[2].tag = "Core";
        container[3].tag = "Shield";
    }

    public float[] GetEquipmentInformation()
    {
        float sahealth = 0;
        float samana = 0;
        float shield = 0;
        float sastrength = 0;
        float satemperature = 0;
;       foreach(ItemSpace itemSp in container)
        {
            if(itemSp != null)
            {
                Equipment eq = itemSp.item2D.GetComponent<Equipment>();
                sahealth += eq.sahealth;
                samana += eq.samana;
                shield += eq.shield; 
                sastrength +=  eq.sastrength;    
                satemperature += eq.satemperature;
            }
            
        }
        return new float[] {sahealth,samana,shield,sastrength,satemperature };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
