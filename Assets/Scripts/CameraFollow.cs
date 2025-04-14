using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform blockHolder; // The holder of the blocks, should be assigned
    public float yOffset = 5f; // The height offset for the camera
    public float smoothSpeed = 0.1f; // Camera smoothing speed

    private float highestBlockY;
    private float initialCamY;

    private Camera mainCamera;

    void Start()
    {
        if (blockHolder == null)
        {
            Debug.LogError("CameraFollow: BlockHolder not assigned!");
            return;
        }

        // Get the Camera component
        mainCamera = Camera.main;

        // Initial camera position
        initialCamY = transform.position.y;
        highestBlockY = initialCamY;
    }

    void LateUpdate()
    {
        // Track the highest block in the tower
        float maxY = highestBlockY;

        foreach (Transform block in blockHolder)
        {
            BlockLanded landedScript = block.GetComponent<BlockLanded>();

            if (landedScript != null && landedScript.hasLanded)
            {
                if (block.position.y > maxY)
                {
                    maxY = block.position.y;
                }
            }
        }

        // Check if the tower exceeds the camera's visible height
        float screenHeight = mainCamera.orthographicSize * 2;

        if (maxY > initialCamY + screenHeight / 2)
        {
            highestBlockY = maxY; // Move the camera to the new height
        }

        // Smoothly adjust the camera's Y position
        float targetY = Mathf.Max(initialCamY, highestBlockY + yOffset);
        Vector3 desiredPosition = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}