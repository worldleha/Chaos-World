using UnityEngine;
using UnityEngine.Events;

// 单个道具栏
public class ItemSpace : MonoBehaviour
{
    public Item2D item2D;
    public UnityAction onItemOut;
    public UnityAction onItemIn;
    public Character character;

    private void Awake()
    {
        onItemIn += () => { };
        onItemOut += () => { item2D = null; };
    }
    private void Start()
    {

    }

    // 将道具UI放置在当前 空间
    public void SetItemInSpace(Item2D _item2D)
    {
        item2D = _item2D;
        ItemSpace itemSpace = item2D.GetComponentInParent<ItemSpace>();
        if (itemSpace != null) itemSpace.SetItemNull();
        transform.DetachChildren();
        _item2D.transform.SetParent(transform);
        _item2D.transform.position = transform.position;
        onItemIn();

    }

    // 设置道具栏所有者
    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    // 清空道具栏
    public Item2D SetItemNull()
    {
        Item2D _item2D = item2D;
        item2D = null;
        transform.DetachChildren(); 
        onItemOut();
        return _item2D; 
    }
   
}
