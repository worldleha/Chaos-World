using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMap:MonoBehaviour 
{
    public Gradient[] planetGradients;
    public static Color whiteGreen;

    public static ColorMap colorMap;

    private void Awake()
    {
        colorMap = this;
    }
    static ColorMap()
    {

        whiteGreen = new Color(0.75f,1f,0.75f,1);

    }
    public static ColorMap Instance()
    {
        if (colorMap == null) return null;
        else return colorMap;
    }
    
    
}
