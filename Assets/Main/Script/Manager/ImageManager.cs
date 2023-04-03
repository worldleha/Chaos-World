using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public Sprite[] itemImages;
    public Sprite[] skillImages;
    private static ImageManager instance;

    private void Awake()
    {
        instance = this;
    }
    public static ImageManager GetImagerManager()
    {
        if (instance == null)throw new UnityException("enable layer is lower than awake");
        return instance;
    }
    public Sprite GetItemImageByName(string _name)
    {
        foreach(Sprite sprite in itemImages)
        {
           
            if (sprite.name.Equals(_name))
                return sprite;
        }
        return null;
    }
    public Sprite GetSkillImageByName(string _name)
    {
        foreach (Sprite sprite in skillImages)
        {

            if (sprite.name.Equals(_name))
            {
                return sprite;

            }
        }
        return null;
    }
    public Sprite GetItemImageById(int id)
    {
        if(id <= itemImages.Length)
            return itemImages[id-1];

        throw new System.Exception("id out of bound");
    }

}
