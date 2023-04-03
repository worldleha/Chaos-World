using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInstrument: CharacterInstrument
{

    protected PlayerControl playerControl;
    public HandpickContainer handpickContainer;


    public new void Init()
    {
        base.Init();
        playerControl = GetComponent<PlayerControl>();    
        handpickContainer = playerControl.handpickContainer;
        AddInstrumentChangeListener(delegate ()
        {
            TargetInstrument = handpickContainer.TargetItem;
        });

        StartCoroutine(DestroyHit());

    }
    
    IEnumerator DestroyHit()
    {
        while (true)
        {
            yield return new WaitForSeconds(30-hitStates.Count);
            if (hitStates.Count > 10)
            {
                hitStates.RemoveAt(0);
            }
        }
    }


    /// <summary>
    /// 切换 上一槽位或下一槽位
    /// </summary>
    /// <param name="next"></param>
    public void ChangeIntrument(bool next)
    {
        handpickContainer.SetTargetItemSpace(next);
    }


    public void AddInstrumentChangeListener(UnityAction ua)
    {
        handpickContainer.AddChangeEventListener(ua);
    }

}
