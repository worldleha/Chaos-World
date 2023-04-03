
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingWindowLogic : Window
{

    public Color image1Color = new Color(1f, 1f, 1f, 0f);
    public Color image2Color = new Color(1f, 1f, 1f, 1f);
    public Color image3Color = new Color(0f, 0f, 0f, 1f);

    public GameObject BasicSetting;
    public GameObject DisplaySetting;
    public GameObject KeyboardSetting;

    public GameObject BasicPanel;
    public GameObject DisplayPanel;
    public GameObject KeyboardPanel;

    private GameObject activePanel;
    private GameObject activeButton;

    // Start is called before the first frame update
    protected new void Start()
    {
        AddListener(BasicSetting, BasicPanel);
        AddListener(DisplaySetting, DisplayPanel);
        AddListener(KeyboardSetting, KeyboardPanel);

        base.Start();

        onCancel += delegate () { this.gameObject.SetActive(false); };
    }

    private void OnEnable()
    {
        ActivePanel(BasicPanel, BasicSetting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ActivePanel(GameObject _activePanel, GameObject _activeButton)
    {

        if (activePanel != null) {
            activeButton.GetComponent<Image>().color = image1Color;
            activeButton.transform.GetChild(0).GetComponent<Image>().color = image2Color;
            activePanel.SetActive(false);
        }

        _activeButton.GetComponent<Image>().color = image2Color;
        _activeButton.transform.GetChild(0).GetComponent<Image>().color = image3Color;
        _activePanel.SetActive(true);

        
        activeButton = _activeButton;
        activePanel = _activePanel; 
    }

    UnityAction ButtonBind(GameObject active, GameObject activeBtn)
    {
        GameObject _active = active;
        GameObject _activeButton = activeBtn;

        void _ActivePanel()
        {
            ActivePanel(_active, _activeButton);
        }
        return _ActivePanel;
    }
    void AddListener(GameObject gt, GameObject ua)
    {
        gt.GetComponent<Button>().onClick.AddListener(ButtonBind(ua,gt));
    }
 
}
