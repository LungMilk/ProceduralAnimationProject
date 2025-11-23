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

        //turn it into direction
        direction = velocity.normalized;
        speed = velocity.magnitude;

        oldPosition = body.position;
    }
}
