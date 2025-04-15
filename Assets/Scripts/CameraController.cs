using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera[] cameras;
    private int currentCamera = -1;
    
    public void Next()
    {
        if (currentCamera > -1)
        {
            cameras[currentCamera].Priority = 0;
        }

        currentCamera = (currentCamera + 1) % cameras.Length;
        cameras[currentCamera].Priority = 1;
    }
}
