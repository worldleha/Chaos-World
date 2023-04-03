
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ¼¼ÄÜÈÝÆ÷
/// </summary>
public class SkillContainer : ItemContainer
{
    private ItemSpace targetItemSpace;
    [SerializeField]
    private int index;
    public UnityAction onTargetSkillChange;

    private new void Awake()
    {   
        base.Awake();
        onTargetSkillChange = () => { };
        targetItemSpace = container[0];
    }
    // Start is called before the first frame update
    new void Start()
    {

        base.Start();
        SetAllItemSpaceTag("Skill");
        index = 0;



    }

    public Skill TargetSkill
    {
        get
        {
            Item2D item2D = targetItemSpace.item2D;
            if (item2D) return (Skill)item2D.item; ;
            return null;
        }
    }


    public void SetTargetSkillSpace(int _index)
    {
        if (index < 0 || index > 5) throw new Exception("skill index out of bound");
        index = _index;
        targetItemSpace = container[index];
        onTargetSkillChange();

    }


    public bool GetTargetItemState()
    {
        
        return TargetSkill is not null;
    }

    public new bool AddItem(Item2D skill)
    {
        
        for (int i = 0; i < container.Length; i++)
        {
            if (container[i].item2D == null)
            {
                ((Skill)skill.item).SetSkillImage();
                container[i].SetItemInSpace(skill);
                return true;
            }
        }
        return false;
    }

    public void AllSpaceAddListener()
    {
        foreach (ItemSpace itemSpace in container)
        {
            itemSpace.onItemIn = onTargetSkillChange;
            itemSpace.onItemOut = onTargetSkillChange;
        }
    }

}
