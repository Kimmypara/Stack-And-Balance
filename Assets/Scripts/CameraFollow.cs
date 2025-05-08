using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform FloorHolder;     // Holder for floor prefabs
    public Transform blockHolder;     // Holder for falling blocks
    public float yOffset = 5f;        // How far above the top object the camera should hover
    public float smoothSpeed = 0.1f;  // How smooth the camera follows

    private float highestY;
    private float initialCamY;
    private Camera mainCamera;

    void Start()
    {
        if (blockHolder == null || FloorHolder == null)
        {
            Debug.LogError("CameraFollow: One or more holders not assigned!");
            return;
        }

        mainCamera = Camera.main;
        initialCamY = transform.position.y;
        highestY = initialCamY;
    }

    void LateUpdate()
    {
        float maxY = float.MinValue;

        // Check highest landed block
        foreach (Transform block in blockHolder)
        {
            BlockLanded landedScript = block.GetComponent<BlockLanded>();
            if (landedScript != null && landedScript.hasLanded)
            {
                maxY = Mathf.Max(maxY, block.position.y);
            }
        }

        // Check highest landed floor
        foreach (Transform floor in FloorHolder)
        {
            Rigidbody rb = floor.GetComponent<Rigidbody>();
            if (rb != null && rb.IsSleeping()) // Only consider floors that have landed
            {
                maxY = Mathf.Max(maxY, floor.position.y);
            }
        }

        // Prevent going below starting camera height
        highestY = Mathf.Max(highestY, maxY);
    
        float targetY = Mathf.Max(initialCamY, highestY + yOffset);
        Vector3 desiredPosition = new Vector3(transform.position.x, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

}