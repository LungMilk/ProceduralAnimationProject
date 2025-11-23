using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public Transform body;

    private Vector3 oldPosition;
    public Vector3 velocity;
    public Vector3 direction;
    public float speed;

    private void Start()
    {
        oldPosition = body.position;
    }

    private void Update()
    {
        //classic velocity equation (d2 - d1) / t = v
        velocity = (body.position - oldPosition) / Time.deltaTime;
        //when standing still it might be some very small value 
        if (velocity.magnitude < 0.05f)
        {
            direction = Vector3.zero;
            speed = 0;
        }
        else
        {
            direction = velocity.normalized;
            speed = velocity.magnitude;
        }

        oldPosition = body.position;
    }
}
