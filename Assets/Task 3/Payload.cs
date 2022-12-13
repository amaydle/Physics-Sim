using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chainEnd;
    public bool inDropZone = false;
    private HingeJoint chainJoint;
    void Start()
    {
        chainJoint = chainEnd.GetComponents<HingeJoint>()[1];
    }

    // Update is called once per frame
    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding GameObject has the tag "chain"
        if (collision.gameObject.CompareTag("Chain"))
        {
            // Connect the hinge joint to the GameObject
            chainJoint.connectedBody = GetComponent<Rigidbody>();
            // payload.velocity = new Vector3(0f, 0f, 0f);
        }

        // Check if the Payload is within the dropzone 
        if (collision.gameObject.CompareTag("Dropzone"))
        {
            inDropZone = true;
        }
        else
        {
            inDropZone = false;
        }
    }
}
