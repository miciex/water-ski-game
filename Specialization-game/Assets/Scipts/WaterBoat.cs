using UnityEngine;

public class WaterBoat : MonoBehaviour
{
    public Transform Motor;
    public float SteerPower = 500f;
    public float Power = 5f;
    public float MaxSpeed = 20f;
    public float Drag = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected Rigidbody Rigidbody;
    protected Quaternion StartPosition;
    protected ParticleSystem ParticleSystem;
    void Start()
    {
        
    }

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        StartPosition = Motor.localRotation;
    }

    private void FixedUpdate()
    {
        var ForceDirection = transform.forward;
        var steer = 0;

        if (Input.GetKey(KeyCode.A))
        {
            steer = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            steer = -1;
        }

        Rigidbody.AddForceAtPosition(steer * transform.right * SteerPower / 100, Motor.position);

        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);

        if(Input.GetKey(KeyCode.W))
        {
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * MaxSpeed, Power);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            PhysicsHelper.ApplyForceToReachVelocity(Rigidbody, forward * -MaxSpeed, Power);
        }

        Motor.SetPositionAndRotation(Motor.position, transform.rotation * StartPosition * Quaternion.Euler(0, 0, -steer * 30));
        if(ParticleSystem != null)
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                ParticleSystem.Play();
            }
            else
            {
                ParticleSystem.Stop();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
