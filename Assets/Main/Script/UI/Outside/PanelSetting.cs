
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : MonoBehaviour
{
    public Button ApplyButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void ButtonOff()
    {
        ApplyButton.interactable = false;
    }
    protected void ButtonOn()
    {
        ApplyButton.interactable= true;  
    }
}
