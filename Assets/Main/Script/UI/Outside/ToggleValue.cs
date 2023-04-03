
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleValue : ValueControl,IPointerClickHandler
{
    // Start is called before the first frame update
    public GameObject activeObject;
    void Start()
    {
       
    }

    // Update is called once per frame
    public void  OnPointerClick( PointerEventData pointerEnentData)
    {
        onValueChange();
        activeObject.SetActive(!activeObject.activeSelf);
    }
    public bool GetValue()
    {
        return activeObject.activeSelf;
    }
    public override string GetStringValue()
    {
        return activeObject.activeSelf.ToString();
    }
}
