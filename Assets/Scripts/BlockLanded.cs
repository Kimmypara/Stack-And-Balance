using System;
using TMPro;
using UnityEngine;

public class BlockLanded : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;

    public bool hasLanded = false;
    public AudioClip hitGroundSound;
    private bool hasBeenReleased = false;
    
    private float releaseTime;
    private bool soundPlayed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Record when this block was released (i.e., when it became dynamic)
        releaseTime = Time.time;

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        // No need to add score here anymore
        if (other.CompareTag("Block") || other.CompareTag("Floor"))
        {
          
            Debug.Log($"Collided with {other.name}");
        }
    }
    
    public void SetReleased()
    {
        hasBeenReleased = true;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenReleased || hasLanded) return;

        if (collision.collider.CompareTag("Floor") || 
            collision.collider.CompareTag("Ground") || 
            collision.collider.CompareTag("Block"))
        {
            hasLanded = true;
            Debug.Log("✅ Block landed on valid surface, playing sound.");
            if (hitGroundSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitGroundSound);
            }

            HighestObjectController height = FindObjectOfType<HighestObjectController>();
            height?.Move(transform.position.y);
        }
    }


    }
    