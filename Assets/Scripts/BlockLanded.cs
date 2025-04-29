using System;
using TMPro;
using UnityEngine;

public class BlockLanded : MonoBehaviour
{
    private Rigidbody rb;
    private TextMeshProUGUI countText;
    private int score = 0;
    public bool hasLanded = false; // Tracks whether the block is settled

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        countText = FindObjectOfType<TextMeshProUGUI>(); // You can assign this better later
        score = 0;
        SetCountText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // No need to add score here anymore
        if (other.CompareTag("Block"))
        {
            Debug.Log($"Collided with {other.name}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
        {
            hasLanded = true; // Block has landed
            score = score + 5; // Add 5 points on landing
            SetCountText();

            HighestObjectController highestObjectController = FindObjectOfType<HighestObjectController>();
            highestObjectController.Move(transform.position.y);
        }
    }

    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Score: " + score.ToString();
    }
       
    }
