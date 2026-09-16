using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawnManager : MonoBehaviour
{
    [Header("Pickups")]
    [SerializeField] private PowerPickup stunPickupPrefab;
    [SerializeField] private PowerPickup slowPickupPrefab;

    [Header("Pontos de Spawn")]
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float playerCheckRadius = 0.8f;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 5f;

    private PowerPickup currentPickup;

    private void Start()
    {
        SpawnRandomPower();
    }

    public void NotifyPickupCollected()
    {
        currentPickup = null;

        StartCoroutine(
            RespawnRoutine()
        );
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(
            respawnDelay
        );

        while (!SpawnRandomPower())
        {
            yield return new WaitForSeconds(
                0.25f
            );
        }
    }

    private bool SpawnRandomPower()
    {
        List<Transform> availablePoints =
            GetAvailableSpawnPoints();

        if (availablePoints.Count == 0)
            return false;

        Transform selectedPoint =
            availablePoints[
                Random.Range(
                    0,
                    availablePoints.Count
                )
            ];

        PowerPickup selectedPrefab =
            Random.value < 0.5f
                ? stunPickupPrefab
                : slowPickupPrefab;

        if (selectedPrefab == null)
        {
            Debug.LogError(
                $"{name}: prefab de poder não configurado!"
            );

            return false;
        }

        currentPickup = Instantiate(
            selectedPrefab,
            selectedPoint.position,
            Quaternion.identity
        );

        currentPickup.SetSpawnManager(this);

        return true;
    }

    private List<Transform>
        GetAvailableSpawnPoints()
    {
        List<Transform> available =
            new();

        foreach (Transform point in spawnPoints)
        {
            if (point == null)
                continue;

            if (!HasPlayerNearby(point.position))
            {
                available.Add(point);
            }
        }

        return available;
    }

    private bool HasPlayerNearby(
        Vector2 position
    )
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(
                position,
                playerCheckRadius
            );

        foreach (Collider2D collider in colliders)
        {
            if (
                collider.GetComponent<
                    PlayerPowerInventory
                >() != null
            )
            {
                return true;
            }
        }

        return false;
    }
}