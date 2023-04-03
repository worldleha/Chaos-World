using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPanel : PanelSetting
{
    public ValueControl bright;
    public ValueControl sound;
    public ValueControl autoSave;
    // Start is called before the first frame update
    void Start()
    {
        ApplyButton.onClick.AddListener(SaveData);
        bright.onValueChange = OnValueChange;
        sound.onValueChange = OnValueChange;
        autoSave.onValueChange = OnValueChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SaveData()
    {

        ButtonOff();
    }
    void OnValueChange()
    {
        ButtonOn();
    }
}
