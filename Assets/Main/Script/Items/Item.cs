
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ҫ������ ����� �� ������װ��������
/// </summary>
public class Item
{
    public int id;
    public string tag;
    public int count;
    public int maxCount;
    public string consume;
    public Item2D item2D;
    public Item3D item3D;
    public string[] information;

    public static GameObject itemAssets;

    static Item(){

        itemAssets = GameObject.Find("ItemAssets");
    }
    /// <summary>
    /// ��ʼ������ 
    /// </summary>
    /// <param name="_id"> ����ID </param>
    /// <param name="_information"> ������Ϣ </param>
    public Item(int _id, string[] _information)
    {
        information = _information;

        id = GetIntFromInformation(ItemInformation.Id);
        maxCount = GetIntFromInformation(ItemInformation.MaxCount);
        tag = GetStringFromInformation(ItemInformation.Tag);
        consume = GetStringFromInformation(ItemInformation.Consume);

        GameObject gameObject2D = Object.Instantiate(PrefabManager.GetPrefabManager().GetUIPrefabByName("2DItem"));
        gameObject2D.GetComponent<Image>().sprite = ImageManager.GetImagerManager().GetItemImageByName(
                GetStringFromInformation(ItemInformation.ImageName)
            );
        item2D = gameObject2D.GetComponent<Item2D>();
        item2D.item = this;
        item2D.tag = tag;

        if(itemAssets == null) itemAssets = GameObject.Find("ItemAssets");
        GameObject gameObject3D =
            Object.Instantiate(PrefabManager.GetPrefabManager().GetItemByName(
                GetStringFromInformation(ItemInformation.PrefabName)
                ), itemAssets.transform);
        gameObject3D.SetActive(false);
        
        item3D = gameObject3D.GetComponent<Item3D>();
        item3D.item = this; 
    }


    // ��ȡ ͨ���ַ�����Ϣ��ȡ����

    public int GetIntFromInformation(ItemInformation info)
    {
        return int.Parse(information[(int)info]);
    }
    public float GetFloatFromInformation(ItemInformation info)
    {
        return float.Parse(information[(int)info]);
    }
    public string GetStringFromInformation(ItemInformation info)
    {
        return information[(int)info].Trim();
    }
    public void Destroy()
    {
        Object.Destroy(item2D);
        Object.Destroy(item3D);
    }
}
