using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 1.5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        float offset =
            Mathf.PingPong(Time.time * speed, distance * 2f) - distance;

        transform.position = new Vector3(
            startPosition.x,
            startPosition.y + offset,
            startPosition.z
        );
    }
}