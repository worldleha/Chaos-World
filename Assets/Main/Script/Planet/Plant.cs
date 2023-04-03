using System;
using UnityEngine;


[Serializable]
public class Plant
{
    public string prefabName;
    private SerVector3 position;
    private SerQuaternion rotation;

    public void SavePlant(Transform transform)
    {
        position.Set(transform.position);
        rotation.Set(transform.rotation);
    }
    public Plant( Transform transform)
    {
        prefabName = transform.name;
        position = new SerVector3(transform.position);
        rotation = new SerQuaternion(transform.rotation);
    }
    public void LoadPlant(GameObject obj)
    {
        obj.transform.position = position.Vector_3;
        obj.transform.rotation = rotation.Quaternion_4;
    }
}
