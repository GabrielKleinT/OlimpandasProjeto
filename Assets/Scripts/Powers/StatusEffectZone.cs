using UnityEngine;

public class StatusEffectZone : MonoBehaviour
{
    public enum EffectType
    {
        Stun,
        Slow
    }

    [SerializeField]
    private EffectType effectType;

    [Header("Duração")]
    [SerializeField]
    private float duration = 2f;

    [Header("Slow")]
    [Range(0.1f, 1f)]
    [SerializeField]
    private float slowMultiplier = 0.5f;

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerStatusEffects effects =
            other.GetComponent<
                PlayerStatusEffects
            >();

        if (effects == null)
            return;

        switch (effectType)
        {
            case EffectType.Stun:

                effects.ApplyStun(
                    duration
                );

                break;

            case EffectType.Slow:

                effects.ApplySlow(
                    slowMultiplier,
                    duration
                );

                break;
        }
    }
}