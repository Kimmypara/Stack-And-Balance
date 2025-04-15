using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] prefabs;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform baseTransform; // Assign this in Inspector
    [SerializeField] private CameraController cameraController;
    
    private Transform currentBlock = null;
    private Rigidbody currentRigidbody = null;

    private bool isDragging = false;

    private int blockCount = 0;

    void Start()
    {
        SpawnNewBlock();
        cameraController.Next();
    }

    
    void SpawnNewBlock()
    {
        
        
        Debug.Log("Spawning new block...");
        Transform selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        currentBlock = Instantiate(selectedPrefab, blockHolder.position, blockHolder.rotation, baseTransform);


        if (TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = Random.ColorHSV();
        }

        blockCount++;

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
        cameraController.Next();
        yield return new WaitForSeconds(2.1f);
        SpawnNewBlock();
    }
    
    private IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(0.8f);
        SpawnNewBlock();
    }
}
