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

    bool stepping = false;

    public BezierCurve stepCurve;
    //public float forwardMovementPrediction;

    private float lerp;
    private void Start()
    {
        stepCurve.arcHeight = stepHeight;
    }
    private void Update()
    {
        //first we establish our ray that we want to cast and its offsets with the body
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        //then we take that ray and hit the floor
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            target.position = info.point;
            //giving a position underneath the character
        }
        //Target is the target we want our foot to travel to
        //once the target point overreaches our threshold
        //we need to step
        //If the distance between where the foot currently is and our target position beyond where the foot is, is larger then our step distance, we should begin to take a step.
        //Vector3 footPosOnGround = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);
        //our distance before we are even stepping.
        float distance = Vector3.Distance(foot.position, info.point);
        if (distance > stepDistance && !stepping)
        {
            //whaty happens at the start of a step
            stepping = true;
            newPosition = target.position;
            oldPosition = foot.position;

            stepCurve.point1 = oldPosition;
            stepCurve.point0.position = newPosition;
        }


        if (stepping)
        {
            //this is now also useless as I have newPosition
            Vector3 thresholdPosition = new Vector3();
            thresholdPosition = oldPosition + oldPosition + (Vector3.forward * stepDistance);
            //can this not be step length/distance?
            //float maxDistance = Vector3.Distance(oldPosition, thresholdPosition);

            Debug.DrawLine(oldPosition, oldPosition + (Vector3.forward * stepDistance), Color.red);
            //this needs to be our percentage in the arc of our step.
            float percentInStep = Mathf.Clamp01(distance / stepDistance);

            //Some how we need to modify what percent in the step we are based on step duration;

            //how are we increasing the rate at which our step goes through the arc?
            print($"distance: {percentInStep}, maxDistance: {stepDistance}, percent: {percentInStep}");
            currentPosition = stepCurve.CalculateQuadraticBezierPoint(percentInStep, stepCurve.point1, stepCurve.point2, stepCurve.point0.position);
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
