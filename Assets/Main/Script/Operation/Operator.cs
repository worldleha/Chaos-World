using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operator 
{

    public static Vector2 ScreenCenter
    {
        get { return new Vector2(Screen.width / 2, Screen.height / 2); }
    }
/// <summary>
/// ºöÂÔÐ¡µÄ,Ïû¶¶
/// </summary>
/// <param name="vector3"></param>
/// <returns></returns>
    public static Vector3 IgnoreSmallVelocityValue(Vector3 vector3, float ignoreNumber = 0.01f)
    {
        if (Mathf.Abs(vector3.z) < ignoreNumber) vector3.z = 0;
        if (Mathf.Abs(vector3.x) < ignoreNumber) vector3.x = 0;
        if (Mathf.Abs(vector3.y) < ignoreNumber) vector3.y = 0;
        return vector3;
    }

    public static bool IsSmallVector2(Vector2 v2, float ignoreNumber = 1f)
    {
        if (Mathf.Abs(v2.x) < ignoreNumber && Mathf.Abs(v2.y) < ignoreNumber) return true;
        return false;
 
    }

    public static Vector3 Vector2ToVector3XZ(Vector2 v2)
    {
        return new Vector3(v2.x, 0, v2.y);
    }

    public static float isClockwise(Vector3 v1, Vector3 v2, Vector3 up, float min = 2.5f)
    {
        float angle = Vector3.Angle(v1, v2);
        if (angle<min)return 0;
        return Vector3 .Dot (Vector3.Cross(v1, v2),up)>0?angle:-angle ;

    }
    public static float AverageValue(Vector3 v3)
    {
        return (v3.x + v3.y + v3.z) / 3;
    }
}
