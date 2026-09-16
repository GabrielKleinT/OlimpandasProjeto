using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 2f;

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
            startPosition.x + offset,
            startPosition.y,
            startPosition.z
        );
    }
}