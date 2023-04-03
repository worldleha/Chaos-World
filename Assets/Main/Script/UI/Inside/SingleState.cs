
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 可优化
/// </summary>

public class SingleState : MonoBehaviour
{
    public States firstState;
    protected float value;
    protected Image image;
    protected TMP_Text text;
    // Start is called before the first frame update

    protected void LoadComponents()
    {
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TMP_Text>();
    }

    public void ChangeValue(float _value)
    {
        //可优化
        if(image is null) LoadComponents();
        text.text = _value.ToString("N1");
        value = _value;
    }
    public void ChangeColor(Color color)
    {
        image.color = color;    
        text.color = color;
    }


}
