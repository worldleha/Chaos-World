using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PanelManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public InputManager inputManager;
    public SkillBoard skillBoard;
    public EquipmentContainer equipmentContainer;
    public BackpackContainer backpackContainer; 
    private GameObject playerPanelObj;
    private PlayerPanel playerPanel;
    private GameObject exitPanelObj;
    private ExitPanel exitPanel;
    private GameObject stateObj;
    private PlayerBasicState state;
    private PlayerInformation playerInformation;

    // Start is called before the first frame update
    private Vector3 activePosition;

    private GameObject activePanel = null;

    private static PanelManager panelManager;

    public static PanelManager Instance
    {
        get 
        {
            if (panelManager == null) throw new Exception("Please GeneratePanel");
            return panelManager;
        }    
    }
    public void AllPanelGenerate()
    {
        GameObject empty = new GameObject("Empty");
        panelManager = this;
        PrefabManager pm = PrefabManager.GetPrefabManager();
        playerPanelObj = Instantiate(pm.GetUIPrefabByName("PlayerPanel"),mainCanvas.transform);
        exitPanelObj = Instantiate(pm.GetUIPrefabByName("ExitPanel"), mainCanvas.transform);
        stateObj = Instantiate(pm.GetUIPrefabByName("State"), mainCanvas.transform);
        Instantiate(empty, mainCanvas.transform).name = "Public";


        playerPanel = playerPanelObj.GetComponent<PlayerPanel>();
        playerPanel.onCancel += () => { SetActive(playerPanelObj, false); };
        playerInformation = playerPanelObj.GetComponentInChildren<PlayerInformation>();
        skillBoard = playerPanel.GetComponentInChildren<SkillBoard>();
        equipmentContainer = playerPanel.GetComponentInChildren<EquipmentContainer>();
        backpackContainer = playerPanel.GetComponentInChildren<BackpackContainer>();
        state = stateObj.GetComponent<PlayerBasicState>();
    }

    public void AllPanelInit(InputManager im)
    {
        inputManager = im;
        SetActive(playerPanelObj, false);
        SetActive(exitPanelObj, false);
        activePosition.x = Screen.width / 2;
        activePosition.y = Screen.height / 2;
    }

    public void ExitPanelInit( Action onOk, Action onMainMenu)
    {
        exitPanel = exitPanelObj.GetComponent<ExitPanel>();
        exitPanel.onCancel += () => { SetActive(exitPanelObj, false); };
        exitPanel.onOk += onOk;
        exitPanel.onMainMenu += onMainMenu;

    }
    public void PlayerPanelInit(PlayerControl pc)
    {
          
        playerInformation.Init(pc);
        state.Init(pc);   

    }

    // Update is called once per frame
    public void OpenCaharacterPanel(ActivePanels activePanels)
    {
     
        SetActive(playerPanelObj, true);

        playerPanel.SetActivePanel(activePanels);

    }

    private void SetActive(GameObject gt, bool active)
    {  

        if (active)
        {
            if(activePanel != null)
                SetActive(activePanel, false);
            activePanel = gt;
            gt.transform.position = activePosition;
            
            inputManager.UIEnable();
        }
        else
        {
            
            gt.transform.position = new Vector3(-1000, -1000, 0);
            activePanel = null;
            inputManager.GameEnable();
    
        }
    }

    public void Exit()
    {
        if(activePanel == null)
            SetActive(exitPanelObj, true);
        else
            SetActive(activePanel, false);
    }
    public void InputEsc(CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
            if (callbackContext.ReadValue<float>() > 0)
                Exit();

    }
    public void InputOpenPanel(CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            float value = callbackContext.ReadValue<float>();
            if (value > 0)
            {
                switch (callbackContext.control.name)
                {
                    case "u": OpenCaharacterPanel(ActivePanels.Backpack); break;
                    case "i": OpenCaharacterPanel(ActivePanels.Information); break;
                    case "k": OpenCaharacterPanel(ActivePanels.Skill); break;
                }
            }
        }
    }
}
