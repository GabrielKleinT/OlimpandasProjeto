using System.Collections;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerPowerInventory.PowerType powerType;

    [Header("Respawn independente")]
    [SerializeField] private float respawnDelay = 5f;

    private SpriteRenderer spriteRenderer;
    private bool isAvailable = true;

    private PowerSpawnManager spawnManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSpawnManager(
        PowerSpawnManager manager
    )
    {
        spawnManager = manager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAvailable)
            return;

        PlayerPowerInventory inventory =
            other.GetComponent<PlayerPowerInventory>();

        if (inventory == null)
            return;

        isAvailable = false;

        inventory.GrantPower(powerType);

        if (spawnManager != null)
        {
            spawnManager.NotifyPickupCollected();

            Destroy(gameObject);

            return;
        }

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        yield return new WaitForSeconds(
            respawnDelay
        );

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isAvailable = true;
    }
}