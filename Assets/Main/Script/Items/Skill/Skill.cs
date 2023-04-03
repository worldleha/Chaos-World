
using UnityEngine.UI;
using UnityEngine;

public class Skill : Item
{
    public float smana;
    public float physicalDamage;
    public float magicDamage;
    public string saction;
    public Sprite skillImage;
    public Skill(int _id, string[] information) : base(_id, information)
    {
        smana = GetFloatFromInformation(ItemInformation.SMana);
        physicalDamage = GetFloatFromInformation(ItemInformation.PhysicalDamage);
        magicDamage = GetFloatFromInformation(ItemInformation.MagicDamage);

        saction = GetStringFromInformation(ItemInformation.SAction);
        skillImage = ImageManager.GetImagerManager().GetSkillImageByName(GetStringFromInformation(ItemInformation.SkillImageName));
    
    }

    public void SetSkillImage()
    {
        item2D.GetComponent<Image>().sprite = skillImage;
        consume = "None";
    }

}
