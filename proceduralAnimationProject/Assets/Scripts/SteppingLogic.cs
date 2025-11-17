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
    public float stepDuration;
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

        //if the distance between where the foot is and the target position is farther then step distance we should begin stepping.
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

        //since we are now stepping
        if (stepping)
        {
            //we make a lerp timer to track its travel path along the curve
            lerp += Time.deltaTime * speed;
            //our bezier func only takes 0-1 so we think of this as a percent of time from a counter that continues to increase.
            float time = Mathf.Clamp01(lerp);

            currentPosition = stepCurve.CalculateQuadraticBezierPoint(time, stepCurve.point1, stepCurve.point2, stepCurve.point0.position);
            //whatever position we get from the curve is then applied to the foot position
            foot.position = currentPosition;

            //then when the intended step duration is exceeded we have stopped stepping and reset for another step.
            if(time >= stepDuration)
            {
                stepping = false;
                lerp = 0;
                print("Reset stepping");
            }
        }

        if (!stepping)
        {
            foot.position = target.position;
        }
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
