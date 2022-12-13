using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] waypoints; // array to store the waypoints
    public float speed = 1f; // speed of the object
    private int currentWaypoint = 0; // the current waypoint the object is moving towards

    void Start() { }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 waypointPosition = waypoints[currentWaypoint].position;

        // calculate the direction vector towards the next waypoint
        Vector3 direction = new Vector3(
            waypointPosition.x - currentPosition.x,
            waypointPosition.y - currentPosition.y,
            waypointPosition.z - currentPosition.z
        );
        // normalize the direction vector
        float distance = Mathf.Sqrt(
            direction.x * direction.x + direction.y * direction.y + direction.z * direction.z
        );
        direction = new Vector3(
            direction.x / distance,
            direction.y / distance,
            direction.z / distance
        );

        // move the object in the calculated direction
        transform.position = new Vector3(
            currentPosition.x + direction.x * speed * Time.deltaTime,
            currentPosition.y + direction.y * speed * Time.deltaTime,
            currentPosition.z + direction.z * speed * Time.deltaTime
        );

        // check if the object has reached the current waypoint
        float distanceToWaypoint = Mathf.Sqrt(
            (currentPosition.x - waypointPosition.x) * (currentPosition.x - waypointPosition.x)
                + (currentPosition.y - waypointPosition.y)
                    * (currentPosition.y - waypointPosition.y)
                + (currentPosition.z - waypointPosition.z)
                    * (currentPosition.z - waypointPosition.z)
        );
        if (distanceToWaypoint < 0.1f)
        {
            // set the next waypoint as the current waypoint
            currentWaypoint++;

            // loop back to the first waypoint when the last waypoint is reached
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
        }
    }
}
