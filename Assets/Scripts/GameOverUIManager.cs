using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static GameOverUIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogWarning("Game Over Panel is not assigned in GameOverUIManager.");
        }
    }
}