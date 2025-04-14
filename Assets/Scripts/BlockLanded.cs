using UnityEngine;

public class BlockLanded : MonoBehaviour
{
    private Rigidbody rb;
    public bool hasLanded = false;  // Tracks whether the block is settled

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mark as landed only if the velocity is almost zero and it's no longer kinematic
        if (!hasLanded && rb != null && !rb.isKinematic && rb.linearVelocity.magnitude < 0.01f)
        {
            hasLanded = true; // Block has landed
        }
    }
}