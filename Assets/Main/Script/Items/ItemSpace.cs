using UnityEngine;
using UnityEngine.Events;

// ����������
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

    // ������UI�����ڵ�ǰ �ռ�
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

    // ���õ�����������
    public void SetCharacter(Character _character)
    {
        character = _character;
    }

    // ��յ�����
    public Item2D SetItemNull()
    {
        Item2D _item2D = item2D;
        item2D = null;
        transform.DetachChildren(); 
        onItemOut();
        return _item2D; 
    }
   
}
