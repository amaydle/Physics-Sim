using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 1.0f;
    public float amplitude = 1.0f;
    public UiUpdater ui;
    private Vector3 initialPosition;
    void Start()
    {
        // Save the initial position of the cube
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the cube up and down
        transform.position =
            initialPosition + new Vector3(0, amplitude * Mathf.Sin(Time.time * speed), 0);

        // Spin the cube around the Y axis
        transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime * speed, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding GameObject has the tag Chain, Player or Payload.
        if (
            collision.gameObject.CompareTag("Chain")
            || collision.gameObject.CompareTag("Player")
            || collision.gameObject.CompareTag("Payload")
        )
        {
            ui.endTheGame("Rocket was hit by an Asteroid", false);
        }
    }
}
