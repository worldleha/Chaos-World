using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public GameObject cancelButton;
    public GameObject okButton; 
    public Action onCancel;
    public Action onOk;
    // Start is called before the first frame update
    protected void Start()
    {
        cancelButton.GetComponent<Button>().onClick.AddListener(Cancel);
        if(okButton != null)
        okButton.GetComponent<Button>().onClick.AddListener(Ok);
    }
    void Cancel()
    {
        if(onCancel != null)
            onCancel();
    }
    void Ok()
    {
        if(onOk != null)
            onOk();
    }
    // Update is called once per frame

}
