using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ValueControl : MonoBehaviour
{
    
    // Start is called before the first frame update
    public UnityAction onValueChange;
    public abstract string GetStringValue();


}
