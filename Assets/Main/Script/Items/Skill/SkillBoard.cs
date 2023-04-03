using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class SkillBoard :MonoBehaviour
{
    public GameObject panel;
    private GameObject skillContainer;
    public SkillContainer[] skillContainers;
    public UnityAction onTargetSkillChange;
    private int index;

    private void Awake()
    {
        skillContainers = panel.GetComponentsInChildren<SkillContainer>();

    }
    void Start()
    {
        index = 0;
        skillContainer = PrefabManager.GetPrefabManager().GetUIPrefabByName("SkillContainer");
        AllContainerAddListener();

    }
    public Skill TargetSkill
    {
        get
        {
            return skillContainers[index].TargetSkill;
        }
    }

    public bool HasTargetSkill
    {
        get
        {
            return TargetSkill is not null;
        }
    }

    public SkillContainer TargetSkillContainer
    {
        get
        {
            return skillContainers[index];
        }
    }

    /// <summary>
    /// 遍历技能容器进行技能添加
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public bool AddSkill(Skill skill)
    {
        foreach (var skillContainer in skillContainers)
        {
            if (skillContainer.AddItem(skill.item2D))
            {
                Debug.Log(onTargetSkillChange);
                onTargetSkillChange();
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 切换技能容器
    /// </summary>
    public void ChangeContainer()
    {
        index++;
        index = (index + skillContainers.Length) % skillContainers.Length;
        onTargetSkillChange();
    }

    /// <summary>
    /// 切换目标技能
    /// </summary>
    /// <param name="index"> 输入小键盘数字键更换技能</param>
    public void SetTargetSkill(int index)
    {
        TargetSkillContainer.SetTargetSkillSpace(index-1);
        onTargetSkillChange();
    }
    public void AddSkillContainer()
    {
        Instantiate(skillContainer, panel.transform);
        skillContainers = GetComponentsInChildren<SkillContainer>();
    }

    /// <summary>
    /// 对每个技能容器添加监听
    /// </summary>
    private void AllContainerAddListener()
    {
        foreach (SkillContainer itemContainer in skillContainers)
        {
            itemContainer.onTargetSkillChange += onTargetSkillChange;
            itemContainer.AllSpaceAddListener();
        }
    }

}
