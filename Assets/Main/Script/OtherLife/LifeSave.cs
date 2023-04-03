using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LifeSave
{
    

    public static SerCharacterData Save(Character character)
    {
        CharacterData data = character.GetComponent<CharacterData>();
        data.UpdateData();
        return new SerCharacterData(data);
        
    }
    public static void SaveLives(Character[] lives)
    {
        SerCharacterData[] datas = new SerCharacterData[lives.Length];
        int i = 0;  
        foreach(Character character in lives)
        {
            datas[i] = Save(character);
            i++;
        }

        FileOperator.CharacterDataSave(datas);

    }


}
