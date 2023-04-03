using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectBarLogic : MonoBehaviour
{
    
    public GameObject ConfirmWindow;
    public GameObject settingWindow;
    public GameObject SelectBar;
    private bool hasSavedFile;

    // Start is called before the first frame update

    private void Awake()
    {
        hasSavedFile = FileOperator.IsSave();
    }
    void Start()
    {
        if(SelectBar == null)
        SelectBar = this.gameObject;
        AddListenerToAllButton();
        settingWindow.GetComponent<Window>().onCancel += delegate () { EnableAllButton(true); };
        Window confirmWindow = ConfirmWindow.GetComponent<Window>();
        confirmWindow.onCancel += delegate () { EnableAllButton(true);confirmWindow.gameObject.SetActive(false); };
        confirmWindow.onOk = CreateNewGame;
        
    }
    private void OnEnable()
    {
        
        SelectBar.transform.GetChild(0).gameObject.SetActive(hasSavedFile);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }


    private void EnableAllButton(bool reverse)
    {
        
        foreach (Transform t in SelectBar.transform)
        {
            SetButtonActive(t.gameObject, reverse);
        }
    }
    private void SetButtonActive(GameObject btn, bool state)
    {
        btn.GetComponent<Button>().interactable= state;
    }


    private void NewGame()
    {
        if (hasSavedFile)
        {
            ConfirmWindow.SetActive(true);
            EnableAllButton(false);
        }
        else
        {
            CreateNewGame();
        }
        
        //void exe()
    }

    private void CreateNewGame()
    {
        FileOperator.ClearAllData();
        GameEnterData.IsNewGame = true;
        GameSceneManager.Instance.ChangeToMainScene();

    }
    private void LoadGame()
    {
        GameEnterData.IsNewGame = false;
        GameSceneManager.Instance.ChangeToMainScene();
    }
    #region 引用外部
    private void CreateGame() 
    { 
    }
    public void SaveGame()
    {
    }
    #endregion 
    private void SettingGame()
    {
        settingWindow.SetActive(true);
        EnableAllButton(false);
        
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    
    private void AddListener(Transform tm, UnityAction an)
    {
        tm.GetComponent<Button>().onClick.AddListener(an);
    }
    private void AddListenerToAllButton()
    {
        AddListener(SelectBar.transform.GetChild(0),LoadGame);
        AddListener(SelectBar.transform.GetChild(1), NewGame);
        AddListener(SelectBar.transform.GetChild(2), SettingGame);
        AddListener(SelectBar.transform.GetChild(3), ExitGame);

    }
}
