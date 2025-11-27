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
        velocity.x = Mathf.Round(direction.x * 100f) / 100f;
        velocity.y = Mathf.Round(direction.y * 100f) / 100f;
        velocity.z = Mathf.Round(direction.z * 100f) / 100f;
        //when standing still it might be some very small value 

        direction = velocity.normalized;
        //gets rid of the extra decimal places
        direction.x = Mathf.Round(direction.x * 100f) / 100f;
        direction.y = Mathf.Round(direction.y * 100f) / 100f;
        direction.z = Mathf.Round(direction.z * 100f) / 100f;


        speed = velocity.magnitude;

        oldPosition = body.position;
    }
}
