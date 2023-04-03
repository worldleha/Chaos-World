using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanetType
{
    Stone = 0,
    Ocean,
    Earth,
    Fire,
    Jungle,
}
public class Planet : MonoBehaviour
{
    public Vector3 pos;
    public float mass;
    public float radius;
    public float sqrRadius;

    public float min;
    public float max;

    public PlanetType type;

    public NoiseLayer noiseLayer;
    public NoiseSetting noiseSetting;   
    public MeshCollider meshCollider;
    public MeshFilter[] meshFilters;

    public Renderer[] renderers;

    private Gradient gradient;

    const int resolution = 50;
    public Texture2D texture;   
    // Start is called before the first frame update
    public float Min
    {
        get { return min; }
        set { min = value; }
    }
    public float Max
    {
        get { return max; }
        set { max = value; }
    }

    public void AddValue(float value)
    {
        if(value>= Max)
        {
            Max = value;
        }
        if (value < Min)
        {
            Min = value;
        }
    }

    public void UpdateGraphic()
    {
        texture = new Texture2D(resolution, 1);
        Color[] colors = new Color[resolution]; 
        for(int i = 0; i < resolution; i++)
        {
            colors[i] = gradient.Evaluate(i/(resolution-1f));
        }
        texture.SetPixels(colors);
        texture.Apply();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetVector("_heightMinMax", new Vector4(Min, Max));
            renderer.material.SetTexture("_heightMap", texture);
        }
    }

    public void Init(Vector3 _pos, float _mass, float _radius, int seed, PlanetType _type, NoiseSetting _noiseSetting =null, float _min = float.MaxValue, float _max = float.MinValue)
    {
        type = _type;
        Min = _min;
        Max = _max;
        pos = _pos;
        transform.position = pos;
        mass = _mass;
        radius = _radius;
        sqrRadius = radius * radius;
        transform.position = pos;
        renderers = GetComponentsInChildren<Renderer>();
        gradient = PlanetSettingGenerate.gradientGenerate(type);
        if(_noiseSetting != null)
            noiseSetting = _noiseSetting;
        else
            noiseSetting = PlanetSettingGenerate.TypeToGenerate(type);
        
        noiseLayer = new NoiseLayer(noiseSetting.noiseLayerCount, seed);
        meshFilters = GetComponentsInChildren<MeshFilter>();
        renderers = GetComponentsInChildren<Renderer>();   
        meshCollider = GetComponentInChildren<MeshCollider>();  
    }


    public Quaternion GetRotation(Vector3 _pos)
    {
        return Quaternion.FromToRotation(Vector3.up, _pos - pos);
    }
    public Vector3 GetRandomPos()
    {
        int length = meshCollider.sharedMesh.vertices.Length;
        return pos + meshCollider.sharedMesh.vertices[Random.Range(0, length - 1)];
    }
}
