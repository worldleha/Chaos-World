using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateInformation : MonoBehaviour
{
    protected CharacterState characterState;

    public void SetCharacter(Character character)
    {
        characterState = character.CS;
    }
    public UnityAction<float> ChangeValue(SingleState _state)
    {
        SingleState state = _state;
        void ChangeValue(float value)
        {
            state.ChangeValue(value);
        }
        return ChangeValue;
    }
    public UnityAction<float> ChangeMaxValue(DoubleState _state)
    {
        DoubleState state = _state;
        void ChangeMaxValue(float value)
        {
            state.ChangeMaxValue(value);
        }
        return ChangeMaxValue;
    }
}
