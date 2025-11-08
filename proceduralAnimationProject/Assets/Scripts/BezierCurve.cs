using UnityEngine;
using UnityEditor;
public class BezierCurve : MonoBehaviour
{
    //https://www.youtube.com/watch?v=Xwj8_z9OrFw&t=2s
    public LineRenderer lineRenderer;
    public Transform point0;
    public Vector3 point1;
    public Vector3 point2;
    public float arcHeight;
    //point 0 is the foot, point 1 is the target, point 2 is the step height point
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
        SetStepHeightPoint(arcHeight);
        DrawQuadraticCurve();
    }
    private void DrawLinearCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateLinearBezierPoint(t, point0.position, point1);
        }
    }
    [ContextMenu("Draw Quadratic")]
    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point2, point1);
        }
    }
    //don't really need all the parameters for these or just have the quadratic curve give the points anyway in the step function
    public Vector3 CalculateLinearBezierPoint(float t, Vector3 Point0,Vector3 Point1)
    {
        return Point0 + t * (Point1 - Point0);
    }

    public Vector3 CalculateQuadraticBezierPoint(float t, Vector3 Point0, Vector3 Point1, Vector3 Point2)
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

    public Vector3 CalculateCubicBezierPoint(float t, Vector3 Point0, Vector3 Point1, Vector3 Point2, Vector3 Point3)
    {
        //https://www.youtube.com/watch?v=RF04Fi9OCPc
        //I need to calculate a point along the curve but I do not know how I could have the step height calculate for both points.
        Vector3 A = CalculateQuadraticBezierPoint(t,Point0,Point1,Point2);
        Vector3 B = CalculateQuadraticBezierPoint(t, Point1, Point2, Point3);

        return Vector3.Lerp(A, B, t);
    }
    public void SetStepHeightPoint(float stepHeight)
    {
        //we need to somehow get the middle position between point 0 and 1 and then increase the position on the vertical axis by step height
        Vector3 midPoint = (point0.position + point1) / 2f;
        midPoint.y += stepHeight;
        point2 = midPoint;
    }

    private void OnDrawGizmos()
    {
        DrawQuadraticCurve();
        for (int i = 0; i < positions.Length -1;i++)
        {
            Debug.DrawLine(positions[i],positions[i + 1],Color.green);
        }
    }
}
