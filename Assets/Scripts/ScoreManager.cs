using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int totalScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
       
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
    }
}

