using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RenderLIne : MonoBehaviour
{
    public Transform[] transforms;
    public LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = transforms.Length;
        for (int i = 0; i < transforms.Length; i++)
        {
            lineRenderer.SetPosition(i, transforms[i].position);
        }
    }
}
