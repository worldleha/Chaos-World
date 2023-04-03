
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 道具的  UI 表示
/// </summary>
public class Item2D : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, ICanvasRaycastFilter,IPointerClickHandler
{

    public Item item;
    private Transform itemParent;
    private bool isRaycastLocationValid = true;


    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            EffectState es = new EffectState(transform.parent.GetComponent<ItemSpace>().character, item);
            EffectMap.GetInstance().GetEffect(item.consume)(es, null, null);
        }
    }
    // ui 标签有Item

    private void Swap(ItemSpace goSpace, Item2D goItem = null)
    {
        ItemSpace selfSpace = this.GetComponentInParent<ItemSpace>();
        if(goItem != null)
        {
            selfSpace.SetItemInSpace(goItem);
        }
        goSpace.SetItemInSpace(this);
    }
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRaycastLocationValid;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        isRaycastLocationValid = false;
        itemParent = this.transform.parent;
        transform.SetParent(GameObject.Find("Public").transform);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        SetParentAndPosition(transform, itemParent);
        isRaycastLocationValid = true;
        GameObject go = eventData.pointerCurrentRaycast.gameObject;

        if (go != null)
        {
            if (go.tag == "DropArea") 
            {
                ItemSpace itemSpace = itemParent.GetComponent<ItemSpace>();
                Transform dropPoint = itemSpace.character.transform.Find("DropPoint");
                item.item3D.GetComponent<PhysicsBase>().Drop(dropPoint.position);
                itemSpace.SetItemNull();

                return;
            }

            ItemSpace goItemSpace;
            Item2D goItem = null;
            if (go.GetComponent<ItemSpace>() != null)
                goItemSpace = go.GetComponent<ItemSpace>();
            else
            {
                goItemSpace = go.GetComponentInParent<ItemSpace>();
                goItem = go.GetComponent<Item2D>();
            }
            if(goItemSpace && goItemSpace!=itemParent.GetComponent<ItemSpace>())
            if (goItemSpace.tag == "Item" || goItemSpace.tag == this.tag)
                Swap(goItemSpace, goItem);
            
        }  
    }
    private void SetParentAndPosition(Transform childTransform,Transform parentTransform)
    {
        childTransform.SetParent(parentTransform);
        childTransform.position = parentTransform.position;
    }
}
