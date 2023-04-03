using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileOperator 
{

    public static BinaryFormatter bf;
    static FileOperator()
    {
        bf = new BinaryFormatter();
    }

    public static void ClearAllData()
    {
        string planetObjDataPath = GetPath(true, "PlanetObjData");
        string planetDataPath = GetPath(true, "PlanetData");
        string meshDataPath = GetPath(true, "MeshData");
        string characterDataPath = GetPath(true, "CharacterData");
        string savePath = GetPath(true, "Save");
        ClearFilePath(planetDataPath);
        ClearFilePath(meshDataPath);
        ClearFilePath(characterDataPath);   
        ClearFilePath(planetObjDataPath);
        ClearFilePath(savePath);
    }

    public static void ClearFilePath(string path)
    {
        if(!Directory.Exists(path))return;
        DirectoryInfo di = new DirectoryInfo(path);
        FileInfo[] files = di.GetFiles();
        foreach (FileInfo file in files)
        {
            file.Delete();
        }
    }

    public static void SavePlanetObject(string name, Plant[] plants)
    {
        string path = GetPath(true, "PlanetObjData");
        FileStream fs = File.Open(Path.Join(path, name), FileMode.OpenOrCreate, FileAccess.Write);
        bf.Serialize(fs, plants);
        fs.Close();
    }

    public static Plant[] LoadPlanetObject(string name)
    {
        string path = GetPath(true, "PlanetObjData" );
        FileStream fs = File.Open(Path.Join(path, name), FileMode.Open, FileAccess.Read);
        Plant[] plants = (Plant[])bf.Deserialize(fs);
        fs.Close();
        return plants;
    }

    public static void PlanetDataSave(string data)
    {
        string path = GetPath(true, "PlanetData");
        StreamWriter sw = new StreamWriter(Path.Join(path,"data"));   
        sw.Write(data);
        sw.Flush();
        sw.Close();
    }

    public static string PlanetDataLoad()
    {
        string path = GetPath(true, "PlanetData");
        StreamReader sr = new StreamReader(Path.Join(path,"data"));
        string data = sr.ReadToEnd();
        sr.Close();
        return data;
    }
    public static void MeshSave(string name,Mesh mesh)
    {
        string path = GetPath(true,"MeshData");
        FileStream fs = File.Open(Path.Join(path,name), FileMode.OpenOrCreate,FileAccess.Write);
        byte[] data = MeshSerializer.SerializeMesh(mesh);
        fs.Write(data);
        fs.Close();
    }

    public static Mesh MeshLoad(string name)
    {
        string path = GetPath(true, "MeshData" );
        FileStream fs = File.Open(Path.Join(path,name), FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, (int)fs.Length);
        fs.Close();
        Mesh mesh = MeshSerializer.DeserializeMesh(data);
        return mesh;
    }
    public static void CharacterDataSave(SerCharacterData[] datas)
    {
        string path = GetPath(true, "CharacterData");
        FileStream fs = File.Open(Path.Join(path, "data"), FileMode.OpenOrCreate, FileAccess.Write);
        bf.Serialize(fs, datas);
        fs.Close();
    }
    public static SerCharacterData[] CharacterDataLoad()
    {
        string path = GetPath(true, "CharacterData");
        FileStream fs = File.Open(Path.Join(path,"data"), FileMode.Open, FileAccess.Read);
        SerCharacterData[] datas = (SerCharacterData[])bf.Deserialize(fs);
        fs.Close();
        return datas;
    }
    public static void Save()
    {
        string path = GetPath(true, "Save");
        FileStream fs = File.Open(Path.Join(path,"game.save"), FileMode.OpenOrCreate);
        fs.Close();
    }
    public static bool IsSave()
    {
        string path = GetPath(true, "Save");
        return File.Exists(Path.Join(path, "game.save"));
    }
    /// <summary>
    /// 获取存储地址
    /// </summary>
    /// <param name="isPersistent"> 是否持久存储</param>
    /// <param name="path"> 路径</param>
    /// <returns></returns>
    public static string GetPath(bool isPersistent, string path)
    {
        if (isPersistent)
        {
            string newPath = Path.Combine(Application.persistentDataPath, path);
            if(!Directory.Exists(newPath))Directory.CreateDirectory(newPath);
            return newPath;
        }
        else
        {
            string newPath = Path.Combine(Application.dataPath, path);
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);
            return newPath;
        }

    }
}
