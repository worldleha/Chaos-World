
using UnityEngine;
using UnityEngine.Events;

public class BulletSelector : SphereSelector
{


    int count = 0;
    int maxCount = 1;
    Vector3 speed = Vector3.zero;
    float time;
    // Start is called before the first frame update
    protected new void Start()
    {
        time = Time.time;
        base.Start();
        action += SomeAction;
    }


    public void SetSpeed(Vector3 _speed)
    {
        speed = _speed;
    }
    public void SetPower(float power)
    {
        
        Scale =  Mathf.Log(power,8f);
        
    }

    private void SomeAction(GameObject obj)
    {
        count++;
    }
    protected void ToDestroy()
    {
        if(Time.time-time>10) Destroy(gameObject);
        if(count >= maxCount)Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime;
        Point = _collider.bounds.center;
        IsCollisionEnter();
        ToDestroy();
    }

}
