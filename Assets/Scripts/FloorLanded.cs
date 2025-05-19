using UnityEngine;

public class FloorLanded : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody rb;

    private AudioSource audioSource;
    public AudioClip hitGroundSound;

    [SerializeField] private GameObject thumbsUpPrefab; // Optional override (or use GameManager.Instance)

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

            // ✅ Only spawn thumbs up if this is the floor object
            if (CompareTag("Floor")) // Make sure your FloorPrefab has tag "Floor"
            {
                ShowThumbsUp();
            }
        }
    }

    void ShowThumbsUp()
    {
        if (GameManager.Instance == null) return;

        Camera cam = Camera.main;

        float forwardOffset = 0.59f;
        float heightOffset = -0.8f;
        float sideOffset = 0f;

        Vector3 spawnPos = cam.transform.position
                           + cam.transform.forward * forwardOffset
                           + cam.transform.up * heightOffset
                           + cam.transform.right * sideOffset;

        Quaternion rot = Quaternion.LookRotation(-cam.transform.forward);

        // Spawn the thumbs-up animation object
        GameObject thumbsUp = Instantiate(GameManager.Instance.thumbsUpPrefab, spawnPos, rot);
        thumbsUp.transform.SetParent(cam.transform, worldPositionStays: true);
        thumbsUp.transform.localScale = Vector3.one * 0.5f;
        thumbsUp.GetComponent<ThumbsUpController>()?.PlayAndDisappear();
        
        // Spawn the text 
        GameObject floorText = Instantiate(GameManager.Instance.floorCompleteTextPrefab, thumbsUp.transform);
        floorText.transform.localPosition = new Vector3(0, 1.8f, 0);  // Adjust Y offset to place text above
        floorText.transform.rotation = Quaternion.LookRotation(floorText.transform.position - cam.transform.position);
        floorText.transform.localScale = Vector3.one * 0.1f;

        
        Destroy(floorText, 3f);
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