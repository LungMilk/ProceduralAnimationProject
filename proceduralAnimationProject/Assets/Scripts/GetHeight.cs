using Unity.VisualScripting;
using UnityEngine;

public class GetHeight : MonoBehaviour
{
    //this script will send a raycast downwards
    //the distance between the hit object and the ground will set how far up the hips will be from the ground
    public LayerMask terrainLayer;
    public Transform body;
    private float distanceToFloor;
    public float height;
    private Vector3 offsetPosition;
    private void Update()
    {
        //each frame I want to set the hips to some distance above the ground
        GetPositionDistance();
    }
    void GetPositionDistance()
    {
        //shoot a ray downward
        Ray ray = new Ray(body.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            //distance from point and the bodys position is the offset in which to maintain
            float distance = Vector3.Distance(hit.point, body.position);
            //how do we set our position to be this distance
            //take the hit point and increase it up to find our desired position
            if (distance < height)
            {
                height = distance;
                offsetPosition = hit.point + (height * body.up);
                body.position = offsetPosition;
            }
            
        }
    }
}
