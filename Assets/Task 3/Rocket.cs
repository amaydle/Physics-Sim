using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10f;
    public float fuelLeft = 100.0f;

    // Sphere where the payload will be attached to 
    public GameObject lastJoin;
    // Connector aboove the sphere
    public Rigidbody lastConnector;
    
    public GameObject camara;
    public Vector3 gravity;
    public Rigidbody rb;

    private HingeJoint chainJoint;
    private Vector3 cameraInitialPosition;
    void Start()
    {
        // Fetch the Rigidbody component from the GameObject with this script attached
        rb = GetComponent<Rigidbody>();
        // Get the hinge joint component from the lastJoin object
        chainJoint = lastJoin.GetComponents<HingeJoint>()[1];
        
        // Set the physics gravity to the specified vector
        Physics.gravity = gravity;

        // Calculate the initial position of the camera relative to the rocket
        cameraInitialPosition = camara.transform.position - transform.position;

        // Invoke the UpdateFuelLevels function repeatedly, starting after 0 seconds, and repeating every 1 second
        InvokeRepeating("UpdateFuelLevels", 0, 1f);
    }

    void Update()
    {
        Vector3 movement = new Vector3(0f, 0f, 0f);

        if (Input.GetKey(KeyCode.W)) // move up
        {
            movement += new Vector3(0f, moveSpeed, 0f);
        }
        if (Input.GetKey(KeyCode.A)) // move left
        {
            movement -= new Vector3(moveSpeed, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D)) // move right
        {
            movement += new Vector3(moveSpeed, 0f, 0f);
        }
        // Drop the box connected to the hingeJoint, set the inital velocity of the box to zero
        if (Input.GetKey(KeyCode.Space))
        {
            chainJoint.connectedBody = lastConnector;
        }
        rb.velocity = movement;

        rb.AddForce(gravity * rb.mass);

        camara.transform.position = transform.position + cameraInitialPosition;
    }

    // This function updates the fuel levels by calculating the energy burn rate
    // based on player input and the mass of the connected body on the chain joint. 
    // The burn rate varies from 0.1 units/update when not thrusting, to 0.4 units/update 
    // for a 0.1 mass, to 6.0 units/update for a 20.0 mass payload. The fuelLeft variable is
    // decremented by the calculated energy burn rate.
    void UpdateFuelLevels()
    {
        float energyBurn = 0.0f;

        // Thrust is not applied
        if (!Input.GetKey(KeyCode.W))
        {
            energyBurn = 0.1f;
        }
        else
        {
            switch (chainJoint.connectedBody.mass)
            {
                case 0.1f:
                    // no payload
                    energyBurn = 0.4f;
                    break;
                case 1.0f:
                    // small payload
                    energyBurn = 0.7f;
                    break;
                case 10.0f:
                    // medium payload
                    energyBurn = 3.0f;
                    break;
                case 20.0f:
                    // large payload
                    energyBurn = 6.0f;
                    break;
            }
        }

        // Update the fuel level
        fuelLeft -= energyBurn;
    }
}
