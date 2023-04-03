
using UnityEngine;
using UnityEngine.Events;

public class PlayerInformation : StateInformation
{

    public SingleState[] states;
    // Start is called before the first frame update
    public void Init(Character character)
    {
        SetCharacter(character);    
        states = GetComponentsInChildren<SingleState>();
        foreach (SingleState state in states)
        {
            DoubleState stated =state as DoubleState;
            if (stated is not null)
            {
                characterState.onStateChange[(int)stated.secondState] += ChangeMaxValue(stated);
            }

            characterState.onStateChange[(int)state.firstState] += ChangeValue(state);
        }

        characterState.touch();
    }

    
}
