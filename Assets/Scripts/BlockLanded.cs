using UnityEngine;

public class BlockLanded : MonoBehaviour
{
    private Rigidbody rb;
    public bool hasLanded = false;  // Tracks whether the block is settled

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Mark as landed only if the velocity is almost zero and it's no longer kinematic
        hasLanded = true; // Block has landed
        HighestObjectController highestObjectController = FindObjectOfType<HighestObjectController>();
        highestObjectController.Move(transform.position.y);
    }
}