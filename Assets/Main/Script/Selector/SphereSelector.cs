
using UnityEngine;

public class SphereSelector : Selector
{
    protected float radius;
    protected float scale;

    //添加一个球碰撞器 来控制
    protected SphereCollider _collider;

    // Start is called before the first frame update
    protected void Start()
    {
        _collider = GetComponent<SphereCollider>();
        Point = _collider.center;
        Radius = _collider.radius;
        layerMask = LayerMask.GetMask("Character");
        action += (GameObject obj) => { };
        Scale = transform.localScale.x;
    }
    public Vector3 Point
    {
        get { return point; }
        set { point = value; }
    }
    public float Radius
    {
        get { return radius; }
        set
        {
            radius = value;
        }
    }
    public float Scale
    {
        get { return scale; }
        set { scale = value; }  
    }
    
    protected void IsCollisionEnter()
    {
        Collider[] colliders = Physics.OverlapSphere(Point, Radius*Scale, layerMask);
        Debug.DrawLine(Point, Point + transform.forward * Radius * Scale,Color.red);
        foreach (Collider collider in colliders)
        {
            
            action(collider.gameObject);
        }
    }
  
   
    

    private void FixedUpdate()
    { 
        
        Point = _collider.bounds.center;
        IsCollisionEnter();
    }
}
