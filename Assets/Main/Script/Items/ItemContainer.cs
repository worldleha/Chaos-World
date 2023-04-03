using UnityEngine;

/// <summary>
/// ��������
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
    //��ʱ���� ��ȡ���� ItemSpace
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
    // ����������Ӷ���
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
        Debug.Log("��������!");
        return false;
    }

    // �������е��߿ռ��������
    public void SetAllItemSpaceCharacter(Character character)
    {
        foreach (ItemSpace itemSpace in container)
        {
            itemSpace.SetCharacter(character);

        }
    }

    // �������е��߿ռ�ı�ǩ
    public void SetAllItemSpaceTag(string _tag)
    {
        foreach (ItemSpace itemSpace in container)
        {
            itemSpace.tag = _tag;
           
        }
    }
}
