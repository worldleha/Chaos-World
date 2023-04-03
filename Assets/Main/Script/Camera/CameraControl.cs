using Cinemachine;
using UnityEngine;

public enum ShakeState
{
    idle=0,
    walk,
    run,
}
public class CameraControl : MonoBehaviour
{
    private CinemachineFreeLook cf;
    private CinemachineVirtualCamera cv;
    private CinemachineImpulseListener impulseListener;

    private GameObject playerFull;
    private GameObject playerWithHand;
    public GameObject impulseObj;
    public float timeSpace = 1;
    private CinemachineBrain cb;
    private bool isThirdPerson;
    private CinemachineImpulseSource[] sources;
    private float startImpulseTime;

    // Start is called before the first frame update
    public void Awake()
    {
        this.enabled = false;
    }
    public void CameraInit(PlayerControl playerControl, CinemachineFreeLook _cf, CinemachineVirtualCamera _cv)
    {
        cf = _cf;
        cv = _cv;

        playerFull = playerControl.transform.Find("chaos_character").gameObject;
        playerWithHand = playerControl.transform.Find("chaos_character_hand").gameObject;

        playerWithHand.SetActive(false);

        isThirdPerson = true;
        cb = GetComponent<CinemachineBrain>();
        impulseListener = cv.GetComponent<CinemachineImpulseListener>();
        sources = impulseObj.GetComponentsInChildren<CinemachineImpulseSource>();
        cb.m_CameraActivatedEvent.AddListener(delegate (ICinemachineCamera to,ICinemachineCamera from) {

        });
        foreach (var source in sources)
        {
            source.GenerateImpulse();
        }
        FirstWalkShake(ShakeState.run);

        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startImpulseTime > timeSpace)
        {
            startImpulseTime = Time.time;

            foreach(var source in sources)
            {
                source.GenerateImpulse();
            }
        }
    }
    public void ChangeCamera(bool _isThirdPerson)
    {
        isThirdPerson = _isThirdPerson;
        cf.gameObject.SetActive(isThirdPerson);
        cv.gameObject.SetActive(!isThirdPerson);
        if (_isThirdPerson)
        {
            playerFull.SetActive(isThirdPerson);
            playerWithHand.SetActive(!isThirdPerson);
            CancelInvoke("ChangeFirst");
        }else
            Invoke("ChangeFirst",4);

    }
    
    public void FirstWalkShake(ShakeState state)
    {
        switch (state)
        {
            case ShakeState.idle:
                impulseListener.m_ChannelMask = 1;
                break;
            case ShakeState.walk:
                impulseListener.m_ChannelMask = 2;
                break;
            case ShakeState.run:
                impulseListener.m_ChannelMask = 4;
                break;
        }
    }
    
    public void ChangeFirst()
    {
        playerFull.SetActive(isThirdPerson);
        playerWithHand.SetActive(!isThirdPerson);
    }
}
