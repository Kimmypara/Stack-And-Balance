using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform baseTransform; // Assign this in Inspector

    private Transform currentBlock = null;
    private Rigidbody currentRigidbody = null;

    private bool isDragging = false;

    private Vector3 blockStartPosition = new Vector3(0f, 7f, -4.4f);
    private int blockCount = 0;

    void Start()
    {
        SpawnNewBlock();
    }

    void SpawnNewBlock()
    {
        Debug.Log("Spawning new block...");
        currentBlock = Instantiate(blockPrefab, blockStartPosition, Quaternion.identity, blockHolder);

        Renderer renderer = currentBlock.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Random.ColorHSV();
        }

        blockCount++;
        Debug.Log("Block Count: " + blockCount);

        if (blockCount % 10 == 0)
        {
            Debug.Log("Rotating base after 10 blocks!");
            baseTransform.Rotate(Vector3.up, 90f);
        }

        currentRigidbody = currentBlock.GetComponent<Rigidbody>();
        currentRigidbody.isKinematic = true;
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
            Plane dragPlane = new Plane(Vector3.up, currentBlock.position);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                currentBlock.position = new Vector3(hitPoint.x, currentBlock.position.y, currentBlock.position.z);
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Dragging released — dropping block");
            isDragging = false;
            currentRigidbody.isKinematic = false;

            currentBlock = null;
            currentRigidbody = null;

            Invoke("SpawnNewBlock", 0.8f);
        }
    }
}
