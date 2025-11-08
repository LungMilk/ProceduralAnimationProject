using Unity.VisualScripting;
using UnityEngine;

public class SteppingLogic : MonoBehaviour
{

    public Transform body;
    public Transform target;
    public Transform foot;

    public float footSpacing;
    public LayerMask terrainLayer;
    private Vector3 currentPosition;
    private Vector3 oldPosition;
    private Vector3 newPosition;
    //distance between steps
    public float stepDistance;
    public float stepHeight;
    public float speed;

    public BezierCurve stepCurve;
    //public float forwardMovementPrediction;

    private float lerp;
    private void Start()
    {
        stepCurve.arcHeight = stepHeight;
    }
    private void Update()
    {
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            target.position = info.point;
            
        }
        //get the distance between our current foot position
        float distance = Vector3.Distance(foot.position, info.point);
        //get the distance between the old position and the farthest bounds in our forward direction
        float maxDistance = Vector3.Distance(foot.position, foot.position + (Vector3.forward * stepDistance));
        Debug.DrawLine(foot.position, foot.position + (Vector3.forward * stepDistance), Color.red);

        float percentInStep = Mathf.Clamp01(distance / maxDistance);
        print($"distance: {percentInStep}, maxDistance: {maxDistance}, percent: {percentInStep}");
        currentPosition = stepCurve.CalculateQuadraticBezierPoint(percentInStep, stepCurve.point1, stepCurve.point2, stepCurve.point0.position);

        //print(distance);
        if (distance > stepDistance)
        {
            //when we have overshot our step distance
            //this should be whatever happens at the end of a stride
            currentPosition = target.position;
            stepCurve.point1 = target.position;
            print("A");
        }
        foot.position = currentPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(target.position, 0.1f);
    }
    //void FootPosition()
    //{
    //    //this keep the feet on the ground
    //    Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
    //    if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
    //    {
    //        transform.position = info.point;
    //    }
    //}
}
