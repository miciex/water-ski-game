using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    WaterFloat waterFloat; 
    public float rotationSpeed = 100;
    public float jumpForce = 10;
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if(Input.GetKey(KeyCode.RightArrow) && !waterFloat.isFloating())
        {
            //rotate the player
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.LeftArrow) && !waterFloat.isFloating())
        {
            //rotate the player left with quaternion
            transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);

        }
    }
}
