
using UnityEngine;

public class Star : MonoBehaviour
{

    public MeshRenderer star;
    public int count = 1000;
    public const int offset = 16;
    public Vector2 scale = new Vector2(2,8);
    public float average = 2.5f;
    public Vector2 brightness;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        float distance = cam.farClipPlane-offset;

        for (int i = 0; i < count; i++)
        {
            MeshRenderer starMesh = Instantiate(star, Random.onUnitSphere*distance, Quaternion.identity, transform);
            float t = SmallestRandomValue(6);
            starMesh.transform.localScale = Vector3.one * Mathf.Lerp(scale.x, scale.y, t)*average;
            starMesh.material.color = Color.Lerp(Color.black, starMesh.material.color, Mathf.Lerp(brightness.x, brightness.y, t));
        }

    }

    float SmallestRandomValue(int iterations)
    {
        float r = 1;
        for (int i = 0; i < iterations; i++)
        {
            r = Mathf.Min(r, Random.value);
        }
        return r;
    }

    void LateUpdate()
    {
        transform.position = cam.transform.position;
    }
}