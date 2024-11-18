using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    WaterFloat waterFloat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Awake()
    {
        waterFloat = GetComponent<WaterFloat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && waterFloat.isFloating())
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
}
