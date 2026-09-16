using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoundButton : MonoBehaviour
{
    public void NextRound()
    {
        if (GameManager.Instance.IsMatchFinished())
        {
            GameManager.Instance.ResetMatch();
        }

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}