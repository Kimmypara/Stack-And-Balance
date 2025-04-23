using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Play again button logic
    public void PlayAgain()
    {
        gameOverPanel.SetActive(false); // Hide the Game Over Panel
        Time.timeScale = 1f; // Unpause the game

        // Restart the game by reloading the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}