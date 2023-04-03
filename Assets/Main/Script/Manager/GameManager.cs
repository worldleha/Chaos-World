using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager: MonoBehaviour
{


    private PlayerControl playerControl;
    public CameraManager cameraManager; 
    public PlanetManager planetManager;
    public LifeManager lifeManager;
    public PanelManager panelManager;
    public InputManager inputManager;
    public GameSceneManager gameSceneManager;

    public Command command;

   // private Thread threadGameEnter;


    private void Start()
    {
        

        gameSceneManager = GameSceneManager.Instance;
        //threadGameEnter = new Thread(() => { StartCoroutine(ProcessControl()); });
        //threadGameEnter.Start();
        StartCoroutine(ProcessControl());
    }
    IEnumerator ProcessControl()
    {
        IEnumerator<float> main = Main();
        while (main.MoveNext())
        {
           
            gameSceneManager.sceneLoading.ProcessTouch(0.5f+main.Current/2);
            
            yield return new WaitForFixedUpdate();
        }
        GameSceneManager.Instance.LoadEnd();
    }
    IEnumerator<float> Main()
    {

        if (GameEnterData.IsNewGame)
        {
            IEnumerator<float> itor = NewGame();
            while (itor.MoveNext())
            {
                yield return itor.Current;
            }
        }

        else
        {
            IEnumerator<float> planetLoad = planetManager.Load();
            while (planetLoad.MoveNext())
            {
                yield return planetLoad.Current/5;
            }

            lifeManager.Load();
            playerControl = lifeManager.player;
            yield return 0.4f;
        }

        
        
        yield return 0.5f;
        //cameraManager.InitCamera(playerControl);
        panelManager.inputManager = inputManager;
        panelManager.AllPanelGenerate();
        yield return 0.6f;
        panelManager.ExitPanelInit(ExitGame, ReturnMainMenu);
        cameraManager.GenerateCamera();
        playerControl.Init();
        yield return 0.7f;
        cameraManager.InitCamera(playerControl);
        inputManager.InputManagerInit(playerControl);
        yield return 0.8f;
        panelManager.AllPanelInit(inputManager);
        panelManager.PlayerPanelInit(playerControl);
        yield return 0.9f;
        playerControl.GetComponent<PlayerInput>().enabled = true;
        command.container = playerControl.backpackContainer;
        command.playerAnimation = playerControl.PA;
        command.dropPoint = playerControl.transform;

        

        /*
        // 先获取所有组件才能加载角色数据面板
        playerControl.GetAllComponent();
        playerControl.CK.Init();
        //生成相机
        cameraManager.GenerateCamera();
        //初始化相机
        cameraManager.InitCamera(playerControl);
        playerControl.PR.Init(cameraManager.firstTarget.gameObject, cameraManager.cf);
        //初始化输入
        inputManager.InputManagerInit(playerControl, cameraManager.cf);
        //对面板绑定输入
        panelManager.inputManager = inputManager;
        //生成所有面板
        panelManager.AllPanelGenerate();
        //设置退出面板
        panelManager.ExitPanelInit(ExitGame, ReturnMainMenu);
        //初始化角色面板
        playerControl.CS.Init();
        
        panelManager.PlayerPanelInit(playerControl);
        //初始化角色
        playerControl.PI.Init();
        playerControl.PS.Init();
        playerControl.Init();

        command.container = playerControl.backpackContainer;
        command.playerAnimation = playerControl.PA;
        command.dropPoint = playerControl.transform;
        */

    }
    IEnumerator<float> NewGame()
    { 
        //生成星球
        IEnumerator<float> generatePlanet = planetManager.GeneratePlanet();
        while (generatePlanet.MoveNext())
        {
            yield return generatePlanet.Current/5;
        }
        
  
        //获取初始星球
        Planet earth = PlanetManager.GetPlanetFromType(PlanetType.Earth);
        yield return 0.3f;
        //生成其他角色
        lifeManager.RandomGenerateLifeBasePlanet(earth);
        //生成玩家
        PlayerControl player = lifeManager.GeneratePlayer(earth);
        playerControl = player;
        yield return 0.4f;
    }
    

    private void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }

    private void SaveGame()
    {
        planetManager.Save();
        lifeManager.Save();
        FileOperator.Save();
    }
    private void ReturnMainMenu()
    {
        SaveGame();
        gameSceneManager.ChangeToUIScene();
    }
}
