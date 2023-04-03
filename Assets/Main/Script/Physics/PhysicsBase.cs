
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 gravity;
    private Vector3 gDirection;
    private float gravityValue;
    Planet planet;

    private bool hasRB = false;
    public bool IsDrop
    {
        get { return hasRB; }
    }
    // Update is called once per frame

    public void Drop(Vector3 dropPoint)
    {
        gameObject.SetActive(true);
        transform.position = dropPoint;
        UseRB();
    }

    public void Grab(ItemContainer container)
    {
        gameObject.SetActive(false);
        container.AddItem(GetComponent<Item3D>().item.item2D);
        CancelRB();
    }
    public bool OnPlanet
    {
        get
        {
            return planet is not null;
        }
    }
    private void Start()
    {

         
    }
 
    public void UseRB()
    {
        HasRB = true;
    }
    public void CancelRB()
    {
        HasRB = false;
    }
    public bool HasRB
    {
        get { return hasRB; }
        set 
        { 
            hasRB = value;
            if (hasRB) InitRigidbody();
            else Destroy(RB);
        }
    }
    private void FixedUpdate()
    {
        
        if (hasRB)
        {
            planet = PlanetManager.GetCharacterPlanet(RB.position);
            GravityUpdate();
            if (OnPlanet)
            {
                RB.AddForce(gravity);
            }
        }

    }
    public Rigidbody RB
    {
        get { return rb; }
    }
    private void InitRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = false;
       
        rb.interpolation = RigidbodyInterpolation.Interpolate;
       
        rb.useGravity = false;
    }
    private void GravityUpdate()
    {

        if (OnPlanet)
        {
            Vector3 _gDirection = (planet.pos - RB.position);
            float M = planet.mass;
            float value;
            value = M * (planet.sqrRadius / _gDirection.sqrMagnitude);
            _gDirection.Normalize();
            gDirection = _gDirection;
            gravityValue = value;
            gravity = _gDirection * value;
        }


    }
}
