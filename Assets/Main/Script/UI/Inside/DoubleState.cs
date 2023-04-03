
using TMPro;
using UnityEngine;

public class DoubleState : SingleState
{
    public States secondState;
    protected float maxValue;
    private TMP_Text text1;

    public new void ChangeValue(float value)
    {
        base.ChangeValue(value);
        
    }
    public void ChangeMaxValue(float _maxValue)
    {
        maxValue = _maxValue;
        if (text1 is null)
        {
            if(transform.childCount >= 3)
            {
                text1 = transform.GetChild(2).GetComponent<TMP_Text>();
            }
            else
            {
                return;
            }
        }
   
        text1.text = maxValue.ToString();
    }
    public void ChangeColor()
    {
        
        Color color = Color.Lerp(Color.red, ColorMap.whiteGreen, value / maxValue);
        image.color = color;
        text.color = color;
        if (text1 is null) return;
        text1.color = color;
    }
}
