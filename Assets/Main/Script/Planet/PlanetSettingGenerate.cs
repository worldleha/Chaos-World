using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettingGenerate
{
    public static NoiseSetting TypeToGenerate(PlanetType type)
    {

        NoiseSetting noiseSetting = null;

        switch (type)
        {
            case PlanetType.Stone:
                noiseSetting = new NoiseSetting(0.5f, 1.4f, 0.9f, 8, 0.7f);
                break;
            case PlanetType.Ocean:
                noiseSetting = new NoiseSetting(0.1f, 1f, 0.8f, 2, 0.6f);
                break;
            case PlanetType.Earth:
                noiseSetting = new NoiseSetting(0.7f, 1.2f, 0.85f, 12, 0.5f);
                break;

            case PlanetType.Fire:
                noiseSetting = new NoiseSetting(0.8f, 1f, 0.5f, 4, 0.5f);
                break;
            case PlanetType.Jungle:
                noiseSetting = new NoiseSetting(0.3f, 0.8f, 0.8f,10, 0.9f);
                break;
        }
        if (noiseSetting == null) noiseSetting = new NoiseSetting();
        return noiseSetting;
    }

    public static Gradient gradientGenerate(PlanetType type)
    {
        return ColorMap.Instance().planetGradients[(int)type];
    }
}
