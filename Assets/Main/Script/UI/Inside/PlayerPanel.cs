
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ActivePanels
{
    Information,
    Backpack,
    Skill
}
public class PlayerPanel : Window {
    // Start is called before the first frame update
    public Color activeColor;
    public Color disabledColor;


    public GameObject informationButton;
    public GameObject skillsButton;
    public GameObject equipmentsButton;

    public GameObject information;
    public GameObject equipments;
    public GameObject skills;

    private GameObject activeButton;
    private GameObject activePanel;


    private Vector2 activePosition;
    // Update is called once per frame

    private void Awake()
    {
        activePosition = new Vector2(0, -32);
        SetActive(equipments,false);
        SetActive(information, false);
        SetActive(skills, false);
    }
    private new void  Start()
    {
        base.Start();
        AddListener(informationButton, information);
        AddListener(skillsButton, skills);
        AddListener(equipmentsButton, equipments);
        SetActivePanel(ActivePanels.Backpack);
        
    }
    public void SetActivePanel(ActivePanels name)
    {
        switch (name)
        {
            case ActivePanels.Information: ActivePanel(information, informationButton);break;
            case ActivePanels.Backpack: ActivePanel(equipments, equipmentsButton);break;
            case ActivePanels.Skill: ActivePanel(skills , skillsButton);break;
        }
        
    }

    private void SetActive(GameObject gt, bool active)
    {
        if (active)
        {
            gt.GetComponent<RectTransform>().anchoredPosition = activePosition;
        }else
            gt.GetComponent<RectTransform>().anchoredPosition = new Vector3(-1000, -1000, 0); 
    }
    void Update()
    {
        
    }
    void ActivePanel(GameObject _activePanel, GameObject _activeButton)
    {

        if (activeButton)
        {
            activeButton.GetComponent<Image>().color = disabledColor;
            SetActive(activePanel, false);
        }

        _activeButton.GetComponent<Image>().color = activeColor;
        SetActive(_activePanel, true);


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
    void AddListener(GameObject button, GameObject panel)
    {
        button.GetComponent<Button>().onClick.AddListener(ButtonBind(panel, button));
    }
    
}
