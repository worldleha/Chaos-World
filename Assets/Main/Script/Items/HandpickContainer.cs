using UnityEngine;
using UnityEngine.Events;

public class HandpickContainer : ItemContainer
{
    public GameObject target;
    private ItemSpace targetItemSpace;
    private int index;
    private UnityAction onTargetInstrumentChange;

    private new void Awake()
    {
        base.Awake();
        onTargetInstrumentChange +=() => { Debug.Log("Hand Target Change"); };
        AllSpaceAddListener();
    }
    // Start is called before the first frame update
    new void Start()
    {
        
        base.Start();
        SetAllItemSpaceTag("Handy");
        index = 0;
        targetItemSpace = container[index];
        

    }

    public Instrument TargetItem
    {
        get {
            Item2D item2D = targetItemSpace.item2D;
            if (item2D)return (Instrument)item2D.item; ;
            return null;
        }
    }

    private void SetTargetPosition(ItemSpace itemSpace)
    {
        target.transform.position = itemSpace.transform.position;
    }
    public void SetTargetItemSpace(bool add)
    {
        if (add)
            index++;
        else
            index--;

        index = (index + container.Length) % container.Length;
        targetItemSpace = container[index];
        SetTargetPosition(targetItemSpace);
        onTargetInstrumentChange();
   
    }

    public bool GetTargetItemState()
    {
        return TargetItem is not null;
    }

    public void AddChangeEventListener(UnityAction ua)
    {
        onTargetInstrumentChange += ua;
        AllSpaceAddListener();
    }

    private void AllSpaceAddListener()
    {
        foreach(ItemSpace itemSpace in container)
        {
            itemSpace.onItemIn = onTargetInstrumentChange;
            itemSpace.onItemOut = onTargetInstrumentChange;
        }
    }

}
