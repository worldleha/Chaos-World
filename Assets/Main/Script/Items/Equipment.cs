

public class Equipment : Item
{
    public float sahealth;
    public float shield;
    public float satemperature;
    public float samana;
    public float sastrength;

    public Equipment(int _id, string[] information) : base(_id, information)
    {

        sahealth = GetFloatFromInformation(ItemInformation.SAHealth);
        samana = GetFloatFromInformation(ItemInformation.SAMana);
        shield = GetFloatFromInformation(ItemInformation.Shield);
        sastrength = GetFloatFromInformation(ItemInformation.SAStrength);
        satemperature = GetFloatFromInformation(ItemInformation.SAStrength);
    }

}
