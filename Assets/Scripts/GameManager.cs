using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] prefabs;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform baseTransform; // Assign this in Inspector
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject FloorPrefab;
    [SerializeField] private Transform floorHolder;
  
    private TextMeshProUGUI countText;
    private int score = 0;
    
    private Transform currentBlock = null;
    private Rigidbody currentRigidbody = null;

    private bool isDragging = false;

    private int blockCount = 0;
    private int rotationCount = 0;

    void Start()
    {
        countText = FindObjectOfType<TextMeshProUGUI>(); // You can assign this better later
        score = 0;
        SetCountText();
        
        SpawnNewBlock();
        cameraController.Next();
    }

    
    void SpawnNewBlock()
    {
        
        
        Debug.Log("Spawning new block...");
        Transform selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        currentBlock = (Transform)Instantiate(selectedPrefab, blockHolder.position, blockHolder.rotation, baseTransform);


        if (TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }

        blockCount++;

        currentRigidbody = currentBlock.GetComponent<Rigidbody>();
        currentRigidbody.isKinematic = true;
        
        
    }
    
    void SpawnFloor()
    {
        Debug.Log("Spawning Floor Prefab...");

        // Find current top Y position from blocks or base
        float topY = baseTransform.position.y;

        foreach (Transform child in blockHolder)
        {
            if (child.position.y > topY)
            {
                topY = child.position.y;
            }
        }

        float spawnHeight = topY + 10f; // Spawn 10 units above current top

        Vector3 spawnPosition = new Vector3(baseTransform.position.x, spawnHeight, baseTransform.position.z);
        Transform parent = floorHolder != null ? floorHolder : baseTransform;

        GameObject floor = Instantiate(FloorPrefab, spawnPosition, Quaternion.identity, parent);

        Rigidbody rb = floor.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = floor.AddComponent<Rigidbody>(); // Make it fall if not already
        }

        rb.isKinematic = false;
        rb.useGravity = true;

        BoxCollider col = floor.GetComponent<BoxCollider>();
        if (col == null)
        {
            col = floor.AddComponent<BoxCollider>();
        }


        score += 1;
        Debug.Log("Floor: " + score);
        SetCountText();
    }



    void Update()
    {
        if (currentBlock == null || currentRigidbody == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == currentBlock)
                {
                    isDragging = true;
                    Debug.Log("Dragging started");
                }
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Plane dragPlane = new Plane(Camera.main.transform.forward, currentBlock.position);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                hitPoint.y = currentBlock.position.y;
                currentBlock.position = hitPoint;
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Dragging released — dropping block");
            isDragging = false;
            currentRigidbody.isKinematic = false;

            currentBlock = null;
            currentRigidbody = null;

            if (blockCount % 10 == 0)
            {
                StartCoroutine(WaitAndSpawn());
            }
            else
            {
                StartCoroutine(SpawnAfterTime());

            }
        }
    }

    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(0.8f);

        rotationCount++;

        // Every 4 rotations, spawn a floor
        if (rotationCount % 4 == 0)
        {
            SpawnFloor();
            yield return new WaitForSeconds(2f); // ⏳ Wait after floor spawns
        }

        cameraController.Next(); // ⏩ Rotate camera after the wait

        yield return new WaitForSeconds(2.1f); // Optional wait before spawning next block
        SpawnNewBlock();
    }

    
    private IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(0.8f);
        SpawnNewBlock();
    }
    
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Floor: " + score.ToString();
    }
}
