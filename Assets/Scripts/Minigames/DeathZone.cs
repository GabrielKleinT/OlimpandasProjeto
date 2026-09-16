using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerRespawn respawn =
            other.GetComponent<PlayerRespawn>();

        if (respawn != null)
        {
            respawn.Respawn();
            Debug.Log(other.name + " caiu e respawnou!");
        }
    }
}