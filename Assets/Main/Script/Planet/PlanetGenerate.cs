using System.Collections;
using UnityEngine;

public class PlanetGenerate : MonoBehaviour
{
    public GameObject planetObj;
    public GameObject planetAssets;
    public float distanceScale = 1000;
    public int maxCount;
    public Vector2 minMaxSize;
    public Vector2 minMaxMass;
    private MeshGenerator meshGenerator;
    private PrefabManager prefabManager;

    private static PlanetGenerate instance;

    public static PlanetGenerate Instance
    {
        get 
        {
            if (instance == null) throw new System.Exception("None PlanetGenerate Instance");
            return instance;
        }
    }

   
    // Start is called before the first frame update
    private void Awake()
    {
        meshGenerator = MeshGenerator.Instance();
        //StartCoroutine(GeneratePlanet());
        prefabManager = PrefabManager.GetPrefabManager();
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Planet GeneratePlanet()
    {
        GameObject planetGameObject;
        planetGameObject = Instantiate(planetObj, planetAssets.transform);
        return planetGameObject.AddComponent<Planet>();    

    }

    public Planet RandomGenerate(PlanetType type, int count)
    {
        Vector3 far = count* Vector3.up *distanceScale;
        Vector3 pos = Random.rotation * far;

        float radius = Random.Range(minMaxSize.x, minMaxSize.y);
        float mass = Random.Range(minMaxMass.x, minMaxMass.y);

        GameObject planetGameObject;
        planetGameObject = Instantiate(planetObj, planetAssets.transform);
        Planet planet = planetGameObject.AddComponent<Planet>();
        
        planet.Init(pos, mass, radius, Random.Range(1,1999999), type);
        foreach(var meshfilter in planet.meshFilters)
        {
            meshGenerator.RandomGeneratorMesh(meshfilter.mesh, planet);
        }
        planet.meshCollider.sharedMesh = planet.meshFilters[0].mesh;
        planet.UpdateGraphic();
        planet.GetComponent<LODGroup>().size = 2*planet.radius;

        RandomGeneratePlants(planet);
        

        return planet;
    }

    public GameObject GeneratePlant(string name)
    {
        GameObject prefab = prefabManager.GetPlanetItemByName(name);
        GameObject obj = Instantiate(prefab);
        obj.name = name;
        return obj;
    }
    public GameObject GeneratePlant(GameObject prefab)
    {
        
        GameObject obj = Instantiate(prefab);
        return obj;
    }

    public void GeneratePlantNum(string name, int num, Planet planet)
    {
        Transform parent = planet.transform.GetChild(0);
        GameObject prefab = prefabManager.GetPlanetItemByName(name);
        for (int i = 0; i < num; i++)
        {
            GameObject plant = GeneratePlant(prefab);
            plant.name = name;
            Transform transform = plant.transform;
            transform.parent = parent;
            transform.position = planet.GetRandomPos();
            transform.rotation = Quaternion.FromToRotation(transform.up, transform.position);
        }
    }
    public void GeneratePlants(Plant[] plants,Planet planet)
    {
        foreach(Plant plant in plants)
        {
            GameObject _plant = GeneratePlant(plant.prefabName);
            _plant.transform.parent = planet.transform.GetChild(0);
            plant.LoadPlant(_plant);
        }
    }
    

    public void RandomGeneratePlants(Planet planet)
    {
        PlanetType type = planet.type;
        Transform parent = planet.transform.GetChild(0);
        switch (type)
        {
            case PlanetType.Earth:
                
                GeneratePlantNum("LittleStone", Random.Range(500,1000), planet);
                GeneratePlantNum("Tree", Random.Range(200, 500), planet);
                GeneratePlantNum("Tree2", Random.Range(500, 1000), planet);
                GeneratePlantNum("Tree3", Random.Range(500, 1000), planet);
                GeneratePlantNum("Grass", Random.Range(10000, 20000), planet);
                
                break;

            case PlanetType.Fire:
                GeneratePlantNum("FireFruit", Random.Range(500, 2000), planet);
                break;

            case PlanetType.Stone:
                GeneratePlantNum("LittleStone", Random.Range(2000, 4000), planet);
                break;
        }
    }

}
