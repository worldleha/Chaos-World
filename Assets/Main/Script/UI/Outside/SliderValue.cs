using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderValue : ValueControl
{
    public Slider slider;
    // Start is called before the first frame update

    private void Start()
    {
        slider.onValueChanged.AddListener(delegate (float value) {onValueChange(); });
    }
    public float GetValue()
    {

        return slider.value;    
    }
    public override string GetStringValue()
    {
        return slider.value.ToString();
    }
}
