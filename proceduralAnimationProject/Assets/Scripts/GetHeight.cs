using Unity.VisualScripting;
using UnityEngine;

public class GetHeight : MonoBehaviour
{
    //this script will send a raycast downwards
    //the distance between the hit object and the ground will set how far up the hips will be from the ground
    public LayerMask terrainLayer;
    public Transform body;

    public float distanceToFloor;

    public float standingHeight;
    public float rayOffset;
    public float maxHeight;
    public float adjustSpeed;
    private void Update()
    {
        //each frame I want to set the hips to some distance above the ground
        //SetPositionToOffset();
        //DistanceToFloor();

        MaintainHeightAboveGround();
    }
    void DistanceToFloor()
    {
        //shoot a ray downward but move the origin up a bit as to prevent getting around being in the ground
        Ray ray = new Ray(body.position + (Vector3.up * 0.1f), Vector3.down);
        Debug.DrawRay(body.position + (Vector3.up * 0.1f),Vector3.down,Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            //distance from point and the bodys position is the offset in which to maintain
            float distance = Vector3.Distance(hit.point, body.position);
            //how do we set our position to be this distance
            //take the hit point and increase it up to find our desired positio
            //offsetPosition = hit.point * distance;
            //distance = distanceToFloor;
            ////I want this distance to the floor to be compared to the 
            if (distance < maxHeight)
            {
                Debug.DrawLine(hit.point, body.position,Color.green);
                body.position = hit.point + (body.up * maxHeight);
                //print(distance);
            }
        }
    }

    void MaintainHeightAboveGround()
    {
        Vector3 origin = body.position + Vector3.up * rayOffset;
        Ray ray = new Ray(origin, Vector3.down);

        Debug.DrawRay(origin, Vector3.down * 10f, Color.red);

        if (Physics.Raycast(ray,out RaycastHit hit, 10f, terrainLayer))
        {
            Vector3 targetPosition = hit.point + (Vector3.up * standingHeight);

            body.position = Vector3.Lerp(body.position, targetPosition, Time.deltaTime * adjustSpeed);
        }
    }
    //void SetPositionToOffset()
    //{
    //    //Set the body position to a place above the floor by some units.
    //    if (GetDistanceToFloor() < height)
    //    {
    //        print("Too close to ground");
    //        body.position = offsetPosition;
    //    }
    //}
}
