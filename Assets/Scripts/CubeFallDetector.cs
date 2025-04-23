using UnityEngine;

public class CubeFallDetector : MonoBehaviour
{
    private GameObject gameOverPanel;
    public float fallThreshold = -5f;

    void Start()
    {
        GameObject panelObject = GameObject.Find("GameOverPanel");
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        GameOverUIManager.instance.ShowGameOver();
    }

}