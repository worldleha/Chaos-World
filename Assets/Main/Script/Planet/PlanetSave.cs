
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlanetSave
{
    public PlanetGenerate planetGenerate;
    private static PlanetSave instance;
    public static PlanetSave Instance
    {
        get
        {
            if (instance == null) throw new System.Exception("Please Init PlanetSave");
            return instance;
        }
    }
    public PlanetSave()
    {
        planetGenerate = PlanetGenerate.Instance;
        instance = this;
    }
    public static void SaveMesh(string id, Planet planet)
    {

        for (int i = 0; i < 3; i++)
        {
            FileOperator.MeshSave(id + "_" + i, planet.meshFilters[i].mesh);
        }
    }

    public static string GetNoiseSeed(Planet planet)
    {
        return planet.noiseLayer.seed.ToString();
    }


    public void SavePlanets(Planet[] planets)
    {
        StringBuilder sb = new StringBuilder();
        
        foreach (Planet planet in planets)
        {
            //Planet[] childrens = planet.transform.GetChild(0).get
            string id = GetNoiseSeed(planet);
            List<float> data = new List<float>();
            sb.Append(id);
            sb.Append("\t");
            data.Add(planet.pos.x);
            data.Add(planet.pos.y);
            data.Add(planet.pos.z);
            data.Add(planet.mass);
            data.Add(planet.radius);
            data.Add(planet.Min);
            data.Add(planet.Max);
            data.Add((int)planet.type);
            sb.Append(string.Join(' ', data));
            sb.Append("\t");
            sb.Append(planet.noiseSetting.GetInfo());
            sb.Append("\n");
            SaveMesh(id, planet);
            Transform mesh1 = planet.transform.GetChild(0);

            Plant[] plants = new Plant[mesh1.childCount];
            for(int i = 0; i < mesh1.childCount; i++)
            {
                plants[i] = new Plant(mesh1.GetChild(i));
            }

            SavePlants(id, plants);
        }

        FileOperator.PlanetDataSave(sb.ToString());
    }
    public IEnumerator<float> LoadPlanets()
    {

        List<Planet> planets = PlanetManager.planets = new List<Planet>();
        string[] planetInfos = FileOperator.PlanetDataLoad().Split("\n");
        int length = planetInfos.Length - 1;
        for (int i= 0; i < length; i++)
        {
            string info = planetInfos[i];
            string[] pinfos = info.Split("\t");
            string id = pinfos[0];
            int seed = int.Parse(id);
            string[] planetData = pinfos[1].Split(" ");
            Vector3 pos = new Vector3(float.Parse(planetData[0]), float.Parse(planetData[1]), float.Parse(planetData[2]));
            float mass = float.Parse(planetData[3]);
            float radius = float.Parse(planetData[4]);

            PlanetType type = (PlanetType)int.Parse(planetData[7]);
            NoiseSetting noiseSetting = NoiseSetting.NewNoiseSettingFromInfo(pinfos[2]);

            Planet planet = planetGenerate.GeneratePlanet();

            planet.Init(pos, mass, radius, seed, type, noiseSetting, float.Parse(planetData[5]), float.Parse(planetData[6]));

            for (int j = 0; j < 3; j++)
            {
                planet.meshFilters[j].mesh = FileOperator.MeshLoad(id + "_" + j);
            }
            planet.meshCollider.sharedMesh = planet.meshFilters[0].mesh;
            planet.UpdateGraphic();
            planet.GetComponent<LODGroup>().size = 2 * planet.radius;
            planetGenerate.GeneratePlants(FileOperator.LoadPlanetObject(id), planet);
            planets.Add(planet);
            yield return i / length;
        }

        Debug.Log("PlanetCount" + planets.Count);


    }

    public void SavePlants(string id, Plant[] plants)
    {
        FileOperator.SavePlanetObject(id, plants);
    }
}
