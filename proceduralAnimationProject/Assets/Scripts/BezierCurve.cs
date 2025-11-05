using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    //https://www.youtube.com/watch?v=Xwj8_z9OrFw&t=2s
    public LineRenderer lineRenderer;
    public Transform point0, point1, point3;

    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];

    private void Start()
    {
        lineRenderer.positionCount = numPoints;
        //DrawLinearCurve();
        DrawQuadraticCurve();
    }
    private void Update()
    {
        DrawQuadraticCurve();
    }
    private void DrawLinearCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateLinearBezierPoint(t, point0.position, point1.position);
        }
    }
    [ContextMenu("Draw Quadratic")]
    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position,point3.position);
        }
    }
    private Vector3 CalculateLinearBezierPoint(float t, Vector3 Point0,Vector3 Point1)
    {
        return Point0 + t * (Point1 - Point0);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 Point0, Vector3 Point1, Vector3 Point2)
    {
        // uu * p0 + 2 * u * t * p1 + tt * p2
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * Point0;
        p += 2 * u * t * Point1;
        p += tt * Point2;

        return p;
    }
}
