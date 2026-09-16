using UnityEngine;

public class StunProjectile : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 3f;

    [Header("Stun")]
    [SerializeField] private float stunDuration = 1.5f;

    [Header("VFX")]
    [SerializeField] private GameObject impactPrefab;

    private Rigidbody2D rb;
    private Collider2D projectileCollider;
    private SpriteRenderer spriteRenderer;

    private GameObject owner;
    private float direction = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(
        GameObject projectileOwner,
        float facingDirection
    )
    {
        owner = projectileOwner;

        direction =
            facingDirection >= 0f
                ? 1f
                : -1f;

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX =
                direction < 0f;
        }

        IgnoreOwnerCollisions();

        rb.linearVelocity = new Vector2(
            direction * speed,
            0f
        );

        Destroy(gameObject, lifetime);
    }

    private void IgnoreOwnerCollisions()
    {
        if (owner == null || projectileCollider == null)
            return;

        Collider2D[] ownerColliders =
            owner.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D ownerCollider in ownerColliders)
        {
            Physics2D.IgnoreCollision(
                projectileCollider,
                ownerCollider,
                true
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner != null)
        {
            if (
                other.gameObject == owner ||
                other.transform.IsChildOf(owner.transform)
            )
            {
                return;
            }
        }

        SpawnImpact();

        PlayerStatusEffects statusEffects =
            other.GetComponent<PlayerStatusEffects>();

        if (statusEffects != null)
        {
            statusEffects.ApplyStun(
                stunDuration
            );
        }

        Destroy(gameObject);
    }

    private void SpawnImpact()
    {
        if (impactPrefab == null)
            return;

        Instantiate(
            impactPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}