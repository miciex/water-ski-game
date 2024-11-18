using UnityEngine;

public class WaterFloat : MonoBehaviour
{
    public float AirDrag = 1;
    public float WaterDrag = 10;
    public Transform[] FloatPoints;
    public bool AttachToSurface;

    protected Rigidbody Rigidbody;
    protected Waves Waves;

    protected float WaterLine;
    protected Vector3[] WaterLinePoints;

    protected Vector3 CenterOffset;
    protected Vector3 SmoothVectorRotation;
    protected Vector3 TargetUp;

    public Vector3 Center {         get { return transform.position + transform.TransformDirection(CenterOffset); }
       }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        Waves = FindAnyObjectByType<Waves>();
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.useGravity = false;

        WaterLinePoints = new Vector3[FloatPoints.Length];
        for (int i = 0; i < FloatPoints.Length; i++)
        {
            WaterLinePoints[i] = FloatPoints[i].position;
        }
        CenterOffset = PhysicsHelper.GetCenter(WaterLinePoints) - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var newWaterLine = 0f;
        var pointUnderWater = false;

        for(int i = 0; i < FloatPoints.Length; i++)
        {
            var point = FloatPoints[i];
            WaterLinePoints[i] = point.position;
            WaterLinePoints[i].y = Waves.GetHeight(point.position);
            newWaterLine += WaterLinePoints[i].y / FloatPoints.Length;
            if (WaterLinePoints[i].y > point.position.y)
            {
                pointUnderWater = true;
            }
        }

        var waterLineDelta = newWaterLine - WaterLine;
        WaterLine = newWaterLine;

        //gravity
        var gravity = Physics.gravity;
        Rigidbody.linearDamping = AirDrag;
        if (WaterLine > Center.y)
        {
            Rigidbody.linearDamping = WaterDrag;
            if (AttachToSurface)
            {
                Rigidbody.position = new Vector3(Rigidbody.position.x, WaterLine - CenterOffset.y, Rigidbody.position.z);
            }
            else
            {
                gravity = -Physics.gravity;
                transform.Translate(Vector3.up * waterLineDelta * 0.5f);
            }
        }
        Rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(WaterLine - Center.y), 0, 1));

        TargetUp = PhysicsHelper.GetNormal(WaterLinePoints);
        if (pointUnderWater)
        {
            TargetUp = Vector3.SmoothDamp(transform.up, TargetUp, ref SmoothVectorRotation, 0.2f);
            Rigidbody.rotation = Quaternion.FromToRotation(transform.up, TargetUp) * Rigidbody.rotation;

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (FloatPoints == null || FloatPoints.Length == 0)
        {
            return;
        }

        for(int i = 0; i < FloatPoints.Length; i++)
        {
            if (FloatPoints[i] == null)
            {
                return;
            }

            if(Waves != null)
            {
               Gizmos.color = Color.red;
               Gizmos.DrawCube(WaterLinePoints[i], Vector3.one * 0.3f);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(FloatPoints[i].position, 0.1f);
        }
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(Center.x, WaterLine, Center.z), 0.3f);
        }

        
    }

    public bool isFloating()
    {
        return WaterLine > Center.y;
    }
}
