using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverUIManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static GameOverUIManager instance;
    public CameraController cameraController; // Reference to camera controller

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

            // Call GameManager to stop block generation
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.TriggerGameOver();
            }
            else
            {
                Debug.LogWarning("GameManager not found in scene.");
            }
        }
        if (cameraController != null)
        {
            cameraController.SwitchToGameOverCamera();
        }
        else
        {
            Debug.LogWarning("Game Over Panel is not assigned in GameOverUIManager.");
        }
    }

    
  
}