
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class CharacterKinematic : MonoBehaviour
{
    public bool isActive = false;
    public float maxAngle;
    public float roughness = 0.1f;
    public float jumpForce;
    public float stepHeight = 0.2f;
    public Transform feet;
    public float rayRadius = .3f;
    private Rigidbody rb;
    private Vector3 gravity;
    //��������
    private Vector3 gDirection;
    //�����Ĵ�С
    private float gravityValue;

    //�������ٶȣ�����.������Y��ת��
    private Vector3 selfVelocity;
    //�������ٶȣ�����.��������Y��ת��
    private Vector3 selfYBaseVelocity;

    private Vector3 groundNormal;

    //�ɽ�ɫ����Y�����ת
    public Quaternion targetSelfYRotation;
    public Quaternion selfYRotation;

    //������ɵ���ת��
    [HideInInspector]
    public Quaternion gravityRotation;
    public Quaternion xzRotation;

    Planet planet;

    public bool isGround = true;

    public bool isRotateComplete = true;

    //�ɲ�����
    private LayerMask walkableLayerMask;

    public UnityAction<float> onMove;
    public UnityAction<Quaternion> onSelfRotate;
    public UnityAction<Quaternion> onSelfRotateComplete;


    public Rigidbody RB
    {
        get
        {
            if (rb == null) throw new System.Exception("Rigidbody is null");
            return rb;
        }
    }

    public bool OnPlanet
    {
        get
        {
            return planet is not null;
        }
    }
    public Quaternion XZRotation
    {
        get
        {
            return xzRotation;
        }
    }
    public Quaternion InverseXZRotation
    {
        get
        {
            return Quaternion.Inverse(XZRotation);
        }
    }
   
    public void Init()
    {
        isActive = true;
        CharacterData data = GetComponent<CharacterData>();
        InitRigidbody(data);
      
        planet = PlanetManager.GetCharacterPlanet(RB.position);
        targetSelfYRotation = selfYRotation = data.rotation;
        gravityRotation = transform.rotation;
        walkableLayerMask = LayerMask.GetMask("Planet")|LayerMask.GetMask("Solid");
        onSelfRotate += (angle) => { };
        onSelfRotateComplete += (angle) => { };
        GravityUpdate();
        
    }

    void Update()
    {
        if (!isActive) return;
        GravityUpdate();

        //����
        MovePlayer();
        
    }

    private void FixedUpdate()
    {
        if (!isActive) return;
        planet = PlanetManager.GetCharacterPlanet(RB.position);
        if(OnPlanet)
            RB.AddForce(gravity);
        isGround = IsGrounded();
        if (isGround) xzRotation = gravityRotation;
        FromGRotate();
        RotatePlayer();

    }

    // ��ʼ��Rigidbody
    private void InitRigidbody(CharacterData data)
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.position = data.position;
        rb.velocity = data.velocity;

    }

    //����������Ϣ
    private void GravityUpdate()
    {
        
        if (OnPlanet)
        {
            
            Vector3 _gDirection = (planet.pos-RB.position);
            float M = planet.mass;
            float value;
            value =  M *(planet.sqrRadius/_gDirection.sqrMagnitude) ;
            _gDirection.Normalize();
            gDirection = _gDirection;
            gravityValue = value;
            gravity = _gDirection * value;
        }


    }

    public Vector3 TurnWorldSpaceIntoPlayer(Vector3 worldSpace)
    {
        return transform.InverseTransformDirection(worldSpace);
    }
    public Vector3 TurnPlayerSpaceIntoWorld(Vector3 playerSpace)
    {
        return transform.TransformDirection(playerSpace);
    }

    /// <summary>
    /// ���̲�����ת
    /// </summary>
    /// <param name="_direction"></param>
    /// <param name="_cameraAngleY"></param>
    public void LookAt(Vector3 _direction)
    {
        isRotateComplete = false;
        targetSelfYRotation = Quaternion.LookRotation(_direction);
    }




    /// <summary>
    /// ����������������������ת
    /// </summary>
    private void FromGRotate()
    {
        if (OnPlanet)
        {
            Quaternion gravityRotationDelta = Quaternion.FromToRotation(transform.up, -gDirection);
            gravityRotation = gravityRotationDelta * gravityRotation;
            
        }

    }

    /*

    /// <summary>
    /// ���� ���� ���ͷ�� ����ƫ��
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, - gDirection*20);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, transform.up*20);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(this.transform.position, gravityRotation * Vector3.up * 20);

        //Gizmos.color = Color.white;
        //Gizmos.DrawRay(this.transform.position + transform.up=,groundNormal * 20);
    }
    */

    /// <summary>
    /// ���ƽ�ɫ��ת ÿ֡����
    /// </summary>
    private void RotatePlayer()
    {

        onSelfRotate(selfYRotation);
        selfYRotation = Quaternion.Lerp(selfYRotation,targetSelfYRotation, roughness);
        IsRotateComplete();
        
        RB.rotation = gravityRotation*selfYRotation;
 
    }
    
    /// <summary>
    /// �жϽ�ɫ��ת���
    /// </summary>
    private void IsRotateComplete()
    {
        if(!isRotateComplete)
        if(Quaternion.Angle(selfYRotation, targetSelfYRotation) < 1f)
        {
            onSelfRotateComplete(selfYRotation);
            isRotateComplete = true;
        }
    }

    /// <summary>
    /// ���ö������Ƶ��ƶ��ٶ�
    /// </summary>
    /// <param name="velocity"></param>
    public void SetAnimationVelocity(Vector3 velocity)
    {
        selfVelocity = velocity;
    }

    public void SetVelocityBaseSelfYRotate(Vector3 velocity)
    {
        selfYBaseVelocity = velocity;
    }

    /// <summary>
    /// �ƶ���ɫ  ��rigidbody interpolate Ӱ�� ƽ��
    /// </summary>
    private void MovePlayer()
    {
        Vector3 moveVelocity = selfVelocity+ gravityRotation * selfYRotation * selfYBaseVelocity;
        StepMove(moveVelocity);  
    }
    
    private void StepMove(Vector3 moveVelocity)
    {
        Quaternion angle = Quaternion.FromToRotation(transform.up, groundNormal);
        Vector3 bodyPoint = transform.position+0.03f*transform.up;
        Vector3 velocity = angle*moveVelocity;
        float height = 0;
        RaycastHit hit;
        RaycastHit hit2;

        Ray ray = new Ray(bodyPoint, velocity);
        Ray ray2 = new Ray(bodyPoint, velocity);


        bool b = Physics.Raycast(ray, out hit, 1f);
        bool a = Physics.Raycast(ray2, out hit2, 1f);
        Debug.DrawRay(bodyPoint, velocity, Color.blue);
        Debug.DrawRay(hit.point, hit.normal, Color.red);
      
        while (a)
        {
            height += 0.02f;
            Vector3 add = transform.up * height;
            ray2 = new Ray(transform.position + add, velocity);
            a = Physics.Raycast(ray2, out hit2, 1f);
      
            if (height > stepHeight)
            {
                rb.MovePosition(rb.position + velocity * Time.deltaTime);
                return;
            }
        }

        if (b && (Vector3.Dot(-hit.normal, velocity.normalized) > 0.7f) && (Vector3.Distance(hit.point, bodyPoint) < 0.3f))
        {
            rb.MovePosition(velocity * Time.deltaTime + transform.position+transform.up*height*0.5f);
        }
        else
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        
    }
    /// <summary>
    ///  �ж��Ƿ��ڵ�����
    /// </summary>
    /// <returns>is ground?</returns>
    public bool IsGrounded()
    { 
        
        const float groundedRayDst = .2f;
        bool grounded = false;

        if (OnPlanet)
        {
            float relativeYVelocity = Vector3.Dot(transform.up,rb.velocity);
            if (relativeYVelocity <=  .5f)
            {
                RaycastHit hit;
                Vector3 offsetToFeet = feet.position - transform.position;
                Vector3 rayOrigin = rb.position + offsetToFeet + transform.up * rayRadius;
                Vector3 rayDir = -transform.up;
                grounded = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, groundedRayDst, walkableLayerMask);
                groundNormal = hit.normal;  
            }
        }
        //Debug.Log(grounded);
        return grounded;
    }
    
    public void Jump()
    {

       
        RB.AddForce(transform.up*jumpForce, ForceMode.Impulse);
    }

     
}
