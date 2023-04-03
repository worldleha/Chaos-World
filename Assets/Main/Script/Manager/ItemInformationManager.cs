using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public enum ItemInformation{
    Id = 0,
    Name,
    MaxCount,
    Type,
    Tag,
    NAction,
    SAction,
    Consume,
    PrefabName,
    ImageName,
    Health,
    SHealth,
    SAHealth,
    RHealth,
    SMana,
    SAMana,
    RMana,
    Shield,
    SShield,
    SAShield,
    SStrength,
    SAStrength,
    RStrength,
    SATemperature,
    MagicDamage,
    PhysicalDamage,
    AttackSpeed,
    MoveSpeed,
    SkillImageName,
}
public class ItemInformationManager
{
    private static string[] lines;
    static ItemInformationManager(){
        lines = ReadAllInfo().Split("\n");
    }
    public static string ReadAllInfo()
    {

        string path = Application.dataPath + "/Main/Data/items";
        StreamReader strr = new StreamReader(path);
        string str = strr.ReadToEnd();
        strr.Close();  
        return str;

    }
    public static string[] GetItemInformationById(int id)
    {
        string line = GetLineById(id);
        string[] informations = line.Split("\t");
        return informations;
    }
    public static string GetLineById(int id)
    {
       
        if (id <= lines.Length - 1)
            return lines[id];
        else
        {
            throw new System.Exception("id out of bound");
            //return null;
        }
    }
}
