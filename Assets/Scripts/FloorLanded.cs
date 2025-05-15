using UnityEngine;

public class FloorLanded : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody rb;

    private AudioSource audioSource;
    public AudioClip hitGroundSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
       
        
        
        if (!hasLanded)
        {
            hasLanded = true;
            NotifyHeight();
            GetComponent<AudioSource>()?.PlayOneShot(hitGroundSound);
        }
    }

    void NotifyHeight()
    {
        HighestObjectController heightTarget = FindObjectOfType<HighestObjectController>();
        if (heightTarget != null)
        {
            float topY = GetComponent<Collider>().bounds.max.y + 2;
            heightTarget.Move(topY);
        }
    }
}