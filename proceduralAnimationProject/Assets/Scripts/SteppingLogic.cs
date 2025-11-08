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
    //public float forwardMovementPrediction;

    private float lerp;

    private void Update()
    {
        //currently we are updating the current position every frame
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            target.position = info.point;

            float distance = Vector3.Distance(foot.position, info.point);
            Debug.DrawLine(foot.position, target.position);

            //print(distance);
            if (distance > stepDistance)
            {
                foot.position = target.position;
                print("A");
            }
        }
        //if (lerp < 1)
        //{
        //    Vector3 footPosition = Vector3.Lerp(oldPosition,newPosition + (Vector3.forward * forwardMovementPrediction),lerp);
        //    footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

        //    currentPosition = footPosition;
        //    lerp += Time.deltaTime * speed;
        //}
        //else
        //{
        //    oldPosition = newPosition;
        //}
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
