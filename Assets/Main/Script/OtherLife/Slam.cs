using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : Character
{
    public SkillBoard skillBoard;
    public ItemContainer itemContainer;

    private EffectState effectState;   
    
    
    // Start is called before the first frame update
   
    public new void Init()
    {
        Debug.Log(skillBoard);
        base.Init();
        
        StartCoroutine(MoveSlam());

        skillBoard.AddSkill(new Skill(4, ItemInformationManager.GetItemInformationById(4)));
        effectState = new EffectState(this, skillBoard.TargetSkill);
        EffectMap.GetInstance().GetEffect("KnockAttack")(effectState, null, null);
    }

    // Update is called once per frames
    void Update()
    {
        
    }
    IEnumerator MoveSlam()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            CA.CharacterMove(new Vector2(Random.Range(-1f, 1f) * 2, Random.Range(-1f, 1f)) * 2);
            yield return new WaitForSeconds(5);
            CA.CharacterMove(Vector2.zero);

        }
    }
}
