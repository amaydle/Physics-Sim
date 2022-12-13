using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    // Declare variables
    public Vector3 launchVelocity;
    public float e;
    public float tolerance;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 gravitationalAcceleration = new Vector3(0, -9.81f, 0);

    private void Start()
    {
        position = transform.position;
        velocity = launchVelocity;
    }

    private void Update()
    {
        // Update the position based on the velocity and elapsed time
        position += velocity * Time.deltaTime;
        transform.position = position;

        // Update the velocity based on the gravitational acceleration and elapsed time
        velocity += gravitationalAcceleration * Time.deltaTime;

        // Check if the object has landed
        if (position.y <= 0)
        {
            velocity.y = -velocity.y * e;
            position.y = 0;
            transform.position = position;

            if (Mathf.Abs(velocity.y) <= tolerance)
            {
                velocity = Vector3.zero;
            }
        }
    }
}
