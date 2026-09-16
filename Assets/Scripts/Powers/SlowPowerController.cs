using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerPowerInventory))]
public class SlowPowerController : MonoBehaviour
{
    [Header("Controle")]
    [SerializeField] private KeyCode powerKey = KeyCode.Q;

    [Header("Disparo")]
    [SerializeField] private SlowProjectile projectilePrefab;
    [SerializeField] private Transform powerSpawn;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 2.5f;

    private PlayerController playerController;
    private PlayerPowerInventory inventory;

    private float nextUseTime = 0f;

    private void Awake()
    {
        playerController =
            GetComponent<PlayerController>();

        inventory =
            GetComponent<PlayerPowerInventory>();
    }

    private void Update()
    {
        if (!playerController.enabled)
            return;

        if (playerController.IsStunned)
            return;

        if (!Input.GetKeyDown(powerKey))
            return;

        if (Time.time < nextUseTime)
            return;

        if (!inventory.HasPower(
            PlayerPowerInventory.PowerType.Slow
        ))
        {
            return;
        }

        if (!Fire())
            return;

        inventory.ConsumePower();

        nextUseTime =
            Time.time + cooldown;
    }

    private bool Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError(
                $"{name}: Slow Projectile não configurado!"
            );

            return false;
        }

        if (powerSpawn == null)
        {
            Debug.LogError(
                $"{name}: PowerSpawn não configurado!"
            );

            return false;
        }

        float direction =
            playerController.FacingDirection;

        Vector3 localSpawn =
            powerSpawn.localPosition;

        Vector3 directedLocalSpawn =
            new Vector3(
                Mathf.Abs(localSpawn.x) * direction,
                localSpawn.y,
                localSpawn.z
            );

        Vector3 spawnPosition =
            transform.TransformPoint(
                directedLocalSpawn
            );

        SlowProjectile projectile =
            Instantiate(
                projectilePrefab,
                spawnPosition,
                Quaternion.identity
            );

        projectile.Initialize(
            gameObject,
            direction
        );

        return true;
    }
}