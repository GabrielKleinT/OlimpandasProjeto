using System.Collections;
using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text medalText;
    [SerializeField] private TMP_Text nextButtonText;

    [Header("Câmera")]
    [SerializeField]
    private MultiplayerCameraFollow multiplayerCamera;

    [Header("Tempos")]
    [SerializeField]
    private float victoryDisplayDelay = 1.1f;

    private bool raceFinished = false;

    private void Awake()
    {
        // Se esquecer de arrastar a câmera,
        // tenta encontrar automaticamente.
        if (multiplayerCamera == null)
        {
            multiplayerCamera =
                FindFirstObjectByType<
                    MultiplayerCameraFollow
                >();
        }
    }

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (raceFinished)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerController winner =
            other.GetComponent<PlayerController>();

        if (winner == null)
            return;

        raceFinished = true;

        int playerId = winner.PlayerId;

        bool matchFinished =
            GameManager.Instance.AddMedal(
                playerId
            );

        // Primeiro trava a prova.
        FreezeAllPlayers(winner);

        // Depois aproxima no vencedor.
        if (multiplayerCamera != null)
        {
            multiplayerCamera.FocusOnWinner(
                winner.transform
            );
        }

        if (matchFinished)
        {
            victoryText.text =
                $"PLAYER {playerId}\nCAMPEÃO!";

            nextButtonText.text =
                "JOGAR NOVAMENTE";
        }
        else
        {
            victoryText.text =
                $"PLAYER {playerId}\nVENCEU!";

            nextButtonText.text =
                "PRÓXIMA PROVA";
        }

        medalText.text =
            $"MEDALHAS\n" +
            $"{GameManager.Instance.GetScore()}";

        // Espera a animação aparecer
        // antes de mostrar o painel.
        StartCoroutine(
            ShowVictoryPanel()
        );

        Debug.Log(
            $"PLAYER {playerId} VENCEU A PROVA!"
        );
    }

    private IEnumerator ShowVictoryPanel()
    {
        yield return new WaitForSeconds(
            victoryDisplayDelay
        );

        victoryPanel.SetActive(true);
    }

    private void FreezeAllPlayers(
        PlayerController winner
    )
    {
        PlayerController[] players =
            FindObjectsByType<PlayerController>(
                FindObjectsSortMode.None
            );

        foreach (
            PlayerController player in players
        )
        {
            bool isWinner =
                player == winner;

            player.FinishRace(isWinner);

            Rigidbody2D rb =
                player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity =
                    Vector2.zero;

                rb.angularVelocity = 0f;
                rb.simulated = false;
            }

            player.enabled = false;
        }
    }
}