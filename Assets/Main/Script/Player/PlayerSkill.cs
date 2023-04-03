using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 角色的技能释放类
/// </summary>
public class PlayerSkill : CharacterSkill
{

    public Image powerImage;
    public Image speedImage;
    private PlayerControl playerControl;
    public Image targetImage;
  
    private bool isQ;
    private bool isE;
    // Start is called before the first frame update


    public new void Init()
    {
        skillBoard = PanelManager.Instance.skillBoard;
        base.Init();
        playerControl = GetComponent<PlayerControl>();
        GameObject skill = GameObject.Find("MainCanvas/Skill");
       
        powerImage = skill.transform.Find("power").GetComponent<Image>();
        speedImage = skill.transform.Find("speed").GetComponent<Image>();
        targetImage = skill.transform.Find("box/target").GetComponent<Image>();

        skillBoard.onTargetSkillChange += OnTargetSkillChange;

        isQ = false;
        isE = false;
        power = 0;
        speed = 1;
        effectMap = EffectMap.GetInstance();
        playerControl = GetComponent<PlayerControl>();

        powerImage.transform.localScale = Vector3.one * Mathf.Log(2, power + 2) / 10;
        speedImage.transform.localScale = Vector3.one * Mathf.Log(2, speed + 1) / 10;
    }
    private void Update()
    {
        if (!isActive) return;
        if(isQ && !isE) power += (1+Mathf.Pow(power,2f*Time.deltaTime));
        else
        {
            power -= power*Time.deltaTime;
            power = Mathf.Max(power, 0);
        }

        if (isE && !isQ)
        {
            if (power > 1f)
            {
                speed += Mathf.Pow(speed,2f*Time.deltaTime);
            }
            else
            {
                speed = 1f;
            }
        }
        
        if (power > 1f)
        {
            playerControl.PA.HandControl(true);
        }
        else
            playerControl.PA.HandControl(false);

        powerImage.transform.localScale = Vector3.one * Mathf.Log(power+2,2)/10;
        speedImage.transform.localScale = Vector3.one* Mathf.Log(speed+1,2)/10;
    }
   
    public void Q(bool isPress)
    {
        if (targetSkill is null) return;
        isQ = isPress;
    }



    public void E(bool isPress)
    {
        if (targetSkill is null) return;
        if (power <=0.1f) return;
        isE = isPress;
;
        if (!isE && power>=0.5f&& speed> 1.2f)
        {
      ;     
            Emission();
            power = 0f;
            speed = 1f;
        }

      
    }
  
    /// <summary>
    /// 当目标技能更换时
    /// </summary>
    public new void OnTargetSkillChange()
    {

        if (!skillBoard.HasTargetSkill)
        {
            targetImage.sprite = null;
            targetImage.color = Color.clear;
            return;
        }
        Debug.Log(targetImage);
        targetImage.sprite = skillBoard.TargetSkill.item2D.GetComponent<Image>().sprite;
        targetSkill = skillBoard.TargetSkill;
        targetImage.color = Color.white;
    }
}
