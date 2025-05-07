using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera[] gameplayCameras;
    public CinemachineCamera gameOverCamera;

    private int currentCamera = -1;

    public void Next()
    {
        if (currentCamera > -1)
        {
            gameplayCameras[currentCamera].Priority = 0;
        }

        currentCamera = (currentCamera + 1) % gameplayCameras.Length;
        gameplayCameras[currentCamera].Priority = 1;
    }

    public void SwitchToGameOverCamera()
    {
        // Lower all gameplay camera priorities
        foreach (var cam in gameplayCameras)
        {
            cam.Priority = 0;
        }

        // Raise the Game Over camera priority
        if (gameOverCamera != null)
        {
            gameOverCamera.Priority = 2; // Must be higher than gameplay camera priority (1)
        }
    }
}