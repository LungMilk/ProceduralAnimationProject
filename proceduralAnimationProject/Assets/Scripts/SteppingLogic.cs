using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class SteppingLogic : MonoBehaviour
{
    public Transform body;
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

    public bool stepping = false;
    //start true to avoid stuff;
    public bool toldToStep = true;

    //public BezierCurve stepCurve;
    //public float forwardMovementPrediction;
    public Vector3 stepDirection;

    private float lerp;
    private void Start()
    {
        //foot.parent = null;
        //stepCurve.arcHeight = stepHeight;
    }
    private void Update()
    {
        UpdatedStepping();
        //if (stepping)
        //{
        //    Stepping();
        //    return;
        //}
        ////first we establish our ray that we want to cast and its offsets with the body
        ////smoothDir = Vector3.Lerp(smoothDir, stepDirection.normalized, Time.deltaTime * directionSmoothing);
        //RaycastHit info = FindPositionOnFloor();
        ////Target is the target we want our foot to travel to
        ////once the target point overreaches our threshold
        ////we need to step
        ////If the distance between where the foot currently is and our target position beyond 

        ////Vector3 footPosOnGround = new Vector3(oldPosition.x, oldPosition.y, oldPosition.z);
        ////our distance before we are even stepping.

        ////if the distance between where the foot is and the target position is farther then 
        //float distance = Vector3.Distance(foot.position, target.position);
        //if (distance > stepDistance)
        //{
        //    canStep = true;
        //    InitializeStepValues();
        //}
        ////we want the foot to stop moving
        //if (!stepping && !canStep)
        //{
        //    //newPosition = target.position;
        //    //foot.position = newPosition;
        //}
    }
    //we will change the name and get rid of anything else later.
    private void UpdatedStepping()
    {
        foot.position = currentPosition;
        //body position is the hips + (which side fo the leg we are on) + (our travel direction * speed)
        Vector3 origin = body.position + (body.right * footSpacing) + (stepDirection.normalized);

        Ray ray = new Ray(origin, Vector3.down);
        //then we take that ray and hit the floor
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value) && !stepping)
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance)
            {
                lerp = 0;
                newPosition = info.point;
            }
        }

        //this is what needs to be known??
        //stepping
        if (!toldToStep)
        {
            return;
        }

        MoveFoot();
    }
    private RaycastHit FindPositionOnFloor()
    {
        transform.position = currentPosition;

        //body position is the hips + (which side fo the leg we are on) + (our travel direction * speed)
        Vector3 origin = body.position + (body.right * footSpacing) + (stepDirection.normalized);

        Ray ray = new Ray(origin, Vector3.down);
        //then we take that ray and hit the floor
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value) && !stepping)
        {
            if(Vector3.Distance(newPosition,info.point) > stepDistance) {
                lerp = 0;
                newPosition = info.point;
            }
        }
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;
        }
        else 
        {
            oldPosition = newPosition;
        }

        return info;
    }

    public void MoveFoot()
    {
        if (lerp < 1)
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;
            print("Stepping");
            stepping = true;
        }
        else
        {
            oldPosition = newPosition;
            stepping = false;
            toldToStep = false;
            print("Finished a step");
        }
    }
    public void StartStep()
    {
        stepping = true;
        lerp = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.1f);
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
