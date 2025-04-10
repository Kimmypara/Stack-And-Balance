using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform blockPrefab;
    [SerializeField] private Transform blockHolder;

    private Transform currentBlock = null;
    private Rigidbody currentRigidbody = null;
    private Vector3 offset;

    private bool isDragging = false;

    private Vector3 blockStartPosition = new Vector3(0f, 7f, -4.4f);

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

        currentRigidbody = currentBlock.GetComponent<Rigidbody>();
        currentRigidbody.isKinematic = true; // Stay in place until released
    }

    void Update()
    {
        if (currentBlock == null || currentRigidbody == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Start dragging
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

        // Dragging logic
        if (isDragging && Input.GetMouseButton(0))
        {
            Plane dragPlane = new Plane(Vector3.up, currentBlock.position); // Horizontal drag
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                currentBlock.position = new Vector3(hitPoint.x, currentBlock.position.y, currentBlock.position.z);
            }
        }

        // Release block
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