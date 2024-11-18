using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;
    public float underWaterDrag = 3;
    public float underWaterAngularDrag = 1;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float floatingPower = 15f;

    public float waterHeight = 0.0f;

    Rigidbody m_RigidBody;

    int floatersUnderWater = 0;

    bool underWater;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderWater = 0;
        for(int i = 0; i < floaters.Length; i++)
        {
            float diff = floaters[i].position.y - waterHeight;

            if (diff < 0)
            {
                m_RigidBody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floaters[i].position, ForceMode.Force);
                floatersUnderWater++;
                if (!underWater)
                {
                    underWater = true;
                    SwitchState(underWater);
                }
            }
        }  
        if(underWater && floatersUnderWater == 0)
        {
            underWater = false;
            SwitchState(underWater);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_RigidBody.linearDamping = underWaterDrag;
            m_RigidBody.angularDamping = underWaterAngularDrag;
        }else
        {
            m_RigidBody.linearDamping = airDrag;
            m_RigidBody.angularDamping = airAngularDrag;
        }
    }
}
