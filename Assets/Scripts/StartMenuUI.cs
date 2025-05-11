using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuUI : MonoBehaviour
{
    public GameObject howToPlayPanel;    
    public GameObject mainMenuPanel;     
    
   
    public GameObject gameplayObjects; 

    public void StartGame()
    {
        Debug.Log("Start Game pressed");
        SceneManager.LoadScene("StackAndBalance");
    }

    public void GoToStackAndBalance()
    {
        Debug.Log("Attempting to load StackAndBalance scene...");
        SceneManager.LoadScene("StackAndBalance");
    }

    public void GoToHowToPlay()
    {
        Debug.Log("Attempting to load HowToPlay scene...");
        SceneManager.LoadScene("HowToPlay");
    }

    public void GoToStartMenu()
    {
        Debug.Log("Attempting to load StartMenu scene...");
        SceneManager.LoadScene("StartMenu");
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