
using System.Collections.Generic;
using UnityEngine;


public class MeshGenerator 
{

    private static MeshGenerator meshGenerator;
    public MeshGenerator()
    {
        

    }

    public static MeshGenerator Instance()
    {
        if(meshGenerator is null) meshGenerator = new MeshGenerator();  
        return meshGenerator;
    }
    public Mesh RandomGeneratorMesh(Mesh mesh , Planet planet)
    {
        
        List<Vector3> vertices = new List<Vector3>();   
        foreach(Vector3 vertice in mesh.vertices)
        {
            Vector3 newPoint = NoiseAddition(vertice, planet);
            vertices.Add(newPoint);
        }

        mesh.vertices = vertices.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();  
        return mesh;
    }
    
    public Vector3 NoiseAddition(Vector3 point, Planet planet)
    {
        point.Normalize();
        NoiseSetting setting = planet.noiseSetting;
        
        Vector3 normal = point.normalized;
 
        float noiseValue = (planet.noiseLayer.noises[0].Evaluate(normal*setting.noiseRoughness)+1)*.5f;
        float attenuation = setting.attenuation;
       
        for (int i = 1; i < setting.noiseLayerCount; i++)
        {
            noiseValue += (planet.noiseLayer.noises[i].Evaluate(normal * setting.noiseRoughness*i*2) + 1) *.5f*attenuation;
            attenuation *= setting.attenuation;
        }
        
        noiseValue = Mathf.Max(noiseValue, setting.minHeight);

        float mValue = (1 + noiseValue * setting.noiseStrength)*planet.radius;
        Vector3 value = point * (mValue);
        planet.AddValue(mValue);
        return value;
    }
}
