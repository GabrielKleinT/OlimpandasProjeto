using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 checkpointPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position;
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = new Vector3(
            position.x,
            position.y + 1f,
            transform.position.z
        );
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = checkpointPosition;
    }
}