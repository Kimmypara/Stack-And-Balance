using System;
using TMPro;
using UnityEngine;

public class BlockLanded : MonoBehaviour
{
    private Rigidbody rb;
    public bool hasLanded = false; // Tracks whether the block is settled

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // No need to add score here anymore
        if (other.CompareTag("Block") || other.CompareTag("Floor"))
        {
            Debug.Log($"Collided with {other.name}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            hasLanded = true; // Block has landed

            HighestObjectController highestObjectController = FindObjectOfType<HighestObjectController>();
            highestObjectController.Move(transform.position.y);
        }
    }

    
       
}