using UnityEngine;

public enum FootSide
{
    Left,
    Right,
}

public class FootSystem : MonoBehaviour
{
    //NO LONGER BEING USED
    //https://www.youtube.com/watch?v=FXhjhlNvvfw
    public LayerMask terrainLayer;
    public FootSystem otherFoot;
    public float stepDistance, stepHeight, stepLength, footSpacing, speed;
    public Transform body;
    public Vector3 footOffset;

    Vector3 oldPosition, newPosition, currentPosition;
    Vector3 oldNormal, currentNormal, newNormal;
    float lerp;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        oldPosition = newPosition = currentPosition = transform.position;
        oldNormal = currentNormal = newNormal = transform.up;
        //lerp is our progress in the step
        lerp = 1;
    }

    private void Update()
    {
        //updating position and normal
        transform.position = currentPosition;
        transform.up = currentNormal;

        //creating a ray that will be offset of the main body and be directed down
        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            if (Vector3.Distance(newPosition, hit.point) > stepDistance && !otherFoot.isMoving() && lerp >= 1)
            {
                lerp = 0;
                //inverseTransformPoint converts the given position from world to local
                int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;
                //calculate the new position the foot needs to be
                newPosition = hit.point + (body.forward * stepLength * direction) + footOffset;
                newNormal = hit.normal;
            }
        }
        //we can move the leg
        if (lerp < 1)
        {
            //lerp from old pos to new pos with our rate of time
            Vector3 tempPos = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPosition = tempPos;
            //our normal will determine what orientation the foot is placed at.
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            //set values for the next frame
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }
    public bool isMoving()
    {
        return lerp < 1;
    }
}
