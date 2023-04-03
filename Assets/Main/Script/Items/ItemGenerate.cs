using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerate
{
    // Start is called before the first frame update

    public static Item GenerateItemById(int id)
    {
        string[] information = ItemInformationManager.GetItemInformationById(id);
        switch (information[(int)ItemInformation.Type])
        {
            case "Item":
                return new Item(id, information);
            case "Instrument":
                return new Instrument(id, information);
            case "Equipment":
                return new Equipment(id, information);
            case "Skill":
                return new Skill(id, information);
        }
        return new Item(id, information);
    }
    // Update is called once per frame

}
