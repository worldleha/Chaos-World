using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public static List<Planet> planets;
    public PlanetGenerate planetGenerate;
    public PlanetManager instance;
    public PlanetSave planetSave;   
    private void Awake()
    {
        instance = this;
        planets = new List<Planet>();
    }
    private void Start()
    {
        planetGenerate = PlanetGenerate.Instance;
        planetSave = new PlanetSave();
    }
    // Start is called before the first frame update
    public static int GetPlanetCount() {
        return planets.Count;
    }

    public void Save()
    {
        
        planetSave.SavePlanets(planets.ToArray());
    }

    public IEnumerator<float> Load()
    {
        planets = new List<Planet>();
        IEnumerator<float> loadPlanets = planetSave.LoadPlanets();
        while (loadPlanets.MoveNext())
        {
            yield return loadPlanets.Current;
        }
        Debug.Log(planets.Count);
    }
    public IEnumerator<float> GeneratePlanet()
    {
        
        planets.Add(planetGenerate.RandomGenerate(PlanetType.Earth, 0));
        yield return 0.2f;
        planets.Add(planetGenerate.RandomGenerate(PlanetType.Ocean,1));
        yield return 0.4f;
        planets.Add(planetGenerate.RandomGenerate(PlanetType.Stone,2));
        yield return 0.6f;
        planets.Add(planetGenerate.RandomGenerate(PlanetType.Fire,3));
        yield return 0.8f;
        planets.Add(planetGenerate.RandomGenerate(PlanetType.Jungle,4));
        yield return 0.10f;
    }
    
    
    public static Planet GetPlanetFromType(PlanetType type)
    {
        foreach(Planet planet in planets)
        {
            if (planet.type == type)
                return planet;
        }
        return null;
    }
    public static Planet GetCharacterPlanet(Vector3 pos)
    {
        foreach (Planet planet in planets)
        {
            float distance = Vector3.Distance(pos, planet.pos);
            
            if(distance < planet.radius*4)
            {
                return planet;
            }
        }
        return null;
    }
}
