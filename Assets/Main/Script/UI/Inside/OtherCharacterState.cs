
using UnityEngine;
using UnityEngine.UI;
public class OtherCharacterState : MonoBehaviour
{
    private GameObject otherCharacterState;
    private GameObject stateBarObj;
    private Slider slider;
    private float maxState;
    private bool show = false;
    public float offsetY = 1f;
    public float time;
    public bool isActive = false;
    // Start is called before the first frame update
    public void Init()
    {
        isActive = true;
        otherCharacterState = GameObject.Find("WorldCanvas/OhterCharacter");
        var stateBar = PrefabManager.GetPrefabManager().GetUIPrefabByName("StateBar");
        stateBarObj = Instantiate(stateBar, otherCharacterState.transform);
        slider = stateBarObj.GetComponent<Slider>();
        stateBarObj.SetActive(false);
    }
    public bool Show
    {
        get { return show; }
        set 
        {
            
            stateBarObj.SetActive(value);
            show = value; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        if (show)
        {
            stateBarObj.transform.position = transform.position+offsetY*transform.up;
            stateBarObj.transform.rotation = Camera.main.transform.rotation;    
            if (Time.time - time > 5f)
            {
                Show = false;
            }
        }

    }

    public void UpdateStateMax(float value)
    {

        maxState = value;
    }
    public void UpdateStateBar(float value)
    {
        time = Time.time;
        Show = true;
        slider.value = value / maxState;
    }
    private void OnDestroy()
    {
        Destroy(stateBarObj);
    }
}
