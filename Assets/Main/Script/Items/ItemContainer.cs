using UnityEngine;

/// <summary>
/// 道具容器
/// </summary>
public class ItemContainer : MonoBehaviour
{
    public GameObject panel;
    public ItemSpace[] container;

    protected void Awake()
    {
        container = panel.GetComponentsInChildren<ItemSpace>();
    }
    protected void Start()
    {
        
        SetAllItemSpaceTag("Item");
    }

    void Update()
    {
        
    }
    //暂时不用 获取面板的 ItemSpace
    protected ItemSpace[] LoadPanelSpace(GameObject panel)
    {
        ItemSpace[] itemSpaces = new ItemSpace[panel.transform.childCount];
        int i = 0;
        foreach (Transform t in panel.transform)
        {
            itemSpaces[i] = t.GetComponent<ItemSpace>();
            i++;
        }
        return itemSpaces;
    }
    // 向容器内添加对象
    public bool AddItem(Item2D item)
    {
        for (int i = 0; i < container.Length; i++)
        {
            if (container[i].item2D == null)
            {
                container[i].SetItemInSpace(item);
                return true;
            }
        }
        Debug.Log("背包已满!");
        return false;
    }

    // 设置所有道具空间的所有者
    public void SetAllItemSpaceCharacter(Character character)
    {
        foreach (ItemSpace itemSpace in container)
        {
            itemSpace.SetCharacter(character);

        }
    }

    // 设置所有道具空间的标签
    public void SetAllItemSpaceTag(string _tag)
    {
        foreach (ItemSpace itemSpace in container)
        {
            itemSpace.tag = _tag;
           
        }
    }
}
