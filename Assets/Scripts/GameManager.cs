using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject thumbsUpPrefab;
    [SerializeField] private Transform[] prefabs;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform baseTransform; // Assign this in Inspector
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject FloorPrefab;
    [SerializeField] private Transform floorHolder;
    [SerializeField] private Transform doorPrefab; // Assign in Inspector
    private bool hasSpawnedDoor = false;

    private int floorCount = 0;
    private bool isGameOver = false;

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

        Transform selectedPrefab;

        if (!hasSpawnedDoor)
        {
            selectedPrefab = doorPrefab;
            hasSpawnedDoor = true;
        }
        else
        {
            selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        }

        currentBlock = Instantiate(selectedPrefab, blockHolder.position, blockHolder.rotation, baseTransform);

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

        float highestY = baseTransform.position.y;

        // Find the highest Y from all landed blocks
        foreach (Transform block in blockHolder)
        {
            BlockLanded landed = block.GetComponent<BlockLanded>();
            if (landed != null && landed.hasLanded)
            {
                Collider col = block.GetComponent<Collider>();
                if (col != null)
                {
                    float blockTop = col.bounds.max.y;
                    highestY = Mathf.Max(highestY, blockTop);
                }
            }
        }

        // Find the highest floor Y as well
        foreach (Transform floor in floorHolder)
        {
            Collider col = floor.GetComponent<Collider>();
            if (col != null)
            {
                float floorTop = col.bounds.max.y;
                highestY = Mathf.Max(highestY, floorTop);
            }
        }

        // Spawn 10 units above the top of the structure
        float spawnHeight = highestY + 10f;
        Vector3 spawnPosition = new Vector3(baseTransform.position.x, spawnHeight, baseTransform.position.z);

        GameObject Floor = Instantiate(FloorPrefab, spawnPosition, Quaternion.identity, floorHolder);

        // Rigidbody and collider check
        Rigidbody rb = Floor.GetComponent<Rigidbody>();
        if (rb == null) rb = Floor.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        if (Floor.GetComponent<Collider>() == null)
            Floor.AddComponent<BoxCollider>();

        score++;
        SetCountText();
    }




    void Update()
    {
        if (isGameOver) return;

        if (currentBlock == null || currentRigidbody == null)
            return;
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
                Debug.Log($"Released block #{blockCount}, starting coroutine.");
                Vector3 hitPoint = ray.GetPoint(enter);
                hitPoint.y = currentBlock.position.y;
                currentBlock.position = hitPoint;
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Dragging released — dropping block");
            isDragging = false;
    
            currentBlock.GetComponent<BlockLanded>()?.SetReleased();

            currentRigidbody.isKinematic = false;

            currentBlock = null;
            currentRigidbody = null;

            if (blockCount % 5 == 0)
            {
                StartCoroutine(WaitAndSpawn());
            }
            else
            {
                StartCoroutine(SpawnAfterTime());
            }
        }

    }
    
    public void TriggerGameOver()
    {
        isGameOver = true;
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
