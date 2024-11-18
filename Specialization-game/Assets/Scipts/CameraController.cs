using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    Transform Transform;
    public float CameraHeight = 15;
    public float CameraDistance = 10;
    public float CameraAngle = 45;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaX = Mathf.Sin(player.eulerAngles.y * Mathf.Deg2Rad) * CameraDistance;    
        var deltaZ = Mathf.Cos(player.eulerAngles.y * Mathf.Deg2Rad) * CameraDistance;
        Transform.position = new Vector3( x: player.position.x - deltaX, y: player.position.y + CameraHeight, z: player.position.z - deltaZ);
        //make the camera rotate same as the player
        Transform.rotation = (Quaternion.Euler(x: CameraAngle, y: player.eulerAngles.y, z: 0));
    }
}
