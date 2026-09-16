using System.Collections;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private Transform statusVfxAnchor;
    [SerializeField] private GameObject stunStatusPrefab;
    [SerializeField] private GameObject slowStatusPrefab;

    private PlayerController controller;

    private Coroutine stunCoroutine;
    private Coroutine slowCoroutine;

    private GameObject stunVfxInstance;
    private GameObject slowVfxInstance;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void ApplyStun(float duration)
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }

        RemoveStunVfx();
        SpawnStunVfx();

        stunCoroutine = StartCoroutine(
            StunRoutine(duration)
        );
    }

    public void ApplySlow(
        float multiplier,
        float duration
    )
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        RemoveSlowVfx();
        SpawnSlowVfx();

        slowCoroutine = StartCoroutine(
            SlowRoutine(multiplier, duration)
        );
    }

    private IEnumerator StunRoutine(float duration)
    {
        controller.SetStunned(true);

        yield return new WaitForSeconds(duration);

        controller.SetStunned(false);

        RemoveStunVfx();

        stunCoroutine = null;
    }

    private IEnumerator SlowRoutine(
        float multiplier,
        float duration
    )
    {
        controller.SetMovementMultiplier(
            multiplier
        );

        yield return new WaitForSeconds(duration);

        controller.SetMovementMultiplier(1f);

        RemoveSlowVfx();

        slowCoroutine = null;
    }

    private void SpawnStunVfx()
    {
        if (
            stunStatusPrefab == null ||
            statusVfxAnchor == null
        )
        {
            return;
        }

        stunVfxInstance = Instantiate(
            stunStatusPrefab,
            statusVfxAnchor,
            false
        );
    }

    private void SpawnSlowVfx()
    {
        if (
            slowStatusPrefab == null ||
            statusVfxAnchor == null
        )
        {
            return;
        }

        slowVfxInstance = Instantiate(
            slowStatusPrefab,
            statusVfxAnchor,
            false
        );
    }

    private void RemoveStunVfx()
    {
        if (stunVfxInstance == null)
            return;

        Destroy(stunVfxInstance);

        stunVfxInstance = null;
    }

    private void RemoveSlowVfx()
    {
        if (slowVfxInstance == null)
            return;

        Destroy(slowVfxInstance);

        slowVfxInstance = null;
    }

    private void OnDisable()
    {
        RemoveStunVfx();
        RemoveSlowVfx();
    }
}