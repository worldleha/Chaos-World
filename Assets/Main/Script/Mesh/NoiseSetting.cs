using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSetting 
{

    public float noiseStrength;
    public float noiseRoughness;
    public float minHeight;
    public int noiseLayerCount;
    public float attenuation;

    public NoiseSetting(float noiseStrength, float noiseRoughness, float minHeight, int noiseLayerCount, float attenuation)
    {
        this.noiseStrength = noiseStrength;
        this.noiseRoughness = noiseRoughness;
        this.minHeight = minHeight;
        this.noiseLayerCount = noiseLayerCount;
        this.attenuation = attenuation;
    }

    public static NoiseSetting NewNoiseSettingFromInfo(string info)
    {
        string[] data = info.Split(" ");
        return new NoiseSetting(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), int.Parse(data[3]), float.Parse(data[4]));
    }
    public string GetInfo()
    {
        return $"{noiseStrength} {noiseRoughness} {minHeight} {noiseLayerCount} {attenuation}";
    }
    public NoiseSetting()
    {
        RandomSetting();
    }
    
    public void RandomSetting()
    {
        noiseStrength = Random.Range(0.2f, 0.8f);
        noiseRoughness = Random.Range(0.8f, 2.4f);
        minHeight = Random.Range(0.4f, 0.95f);
        noiseLayerCount = Random.Range(1,12);
        attenuation = Random.Range(0.2f, 0.8f);
    }
}
