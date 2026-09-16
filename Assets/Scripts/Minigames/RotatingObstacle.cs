using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotatingObstacle : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float newRotation =
            rb.rotation +
            rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(newRotation);
    }
}