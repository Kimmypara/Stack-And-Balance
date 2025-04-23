using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    public GameObject howToPlayPanel;    
    public GameObject mainMenuPanel;     
    
   
    public GameObject gameplayObjects; 

    public void StartGame()
    {
        Debug.Log("Start Game pressed");
        
        // Hide the main menu and how to play panels
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);

        // Show the gameplay elements
        gameplayObjects.SetActive(true);
    }

    public void ShowHowToPlay()
    {
        Debug.Log("Show How to Play");
        howToPlayPanel.SetActive(true);
        mainMenuPanel.SetActive(false); // Hide the main menu buttons
    }

    public void HideHowToPlay()
    {
        Debug.Log("Hide How to Play");
        howToPlayPanel.SetActive(false);
        mainMenuPanel.SetActive(true); // Show the main menu buttons again
    }
}