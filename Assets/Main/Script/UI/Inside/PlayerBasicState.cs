
using UnityEngine;


public class PlayerBasicState : StateInformation
{

    // Start is called before the first frame update
    private DoubleState[] dstates;
    private SingleState state;
    
    public void Init(Character character)
    {
        SetCharacter(character);
        state = transform.GetChild(2).GetComponent<SingleState>();
        dstates = GetComponentsInChildren<DoubleState>();

        characterState.onStateChange[(int)state.firstState]+= delegate (float value)
        {
            state.ChangeValue(value);
            state.ChangeColor(
                Color.Lerp(
                    Color.red, ColorMap.whiteGreen, Mathf.Max(
                       1 - Mathf.Abs(value - CharacterState.temperatureNormal), CharacterState.temperatureRange
                    ) / CharacterState.temperatureRange
                )
             );
         
        };
        foreach (DoubleState dstate in dstates)
        {
            characterState.onStateChange[(int)dstate.firstState] += ChangeValue(dstate);
            characterState.onStateChange[(int)dstate.firstState] += (value) => dstate.ChangeColor();
            characterState.onStateChange[(int)dstate.secondState] += ChangeMaxValue(dstate);
            characterState.onStateChange[(int)dstate.secondState] += (value) => dstate.ChangeColor();

        }
        characterState.touch();
    }


    


}
