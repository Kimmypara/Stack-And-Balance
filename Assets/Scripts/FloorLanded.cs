using UnityEngine;

public class FloorLanded : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!hasLanded && rb != null && rb.linearVelocity.magnitude < 0.1f)
        {
            hasLanded = true;
            NotifyHeight();
        }
    }

    void NotifyHeight()
    {
        HighestObjectController heightTarget = FindObjectOfType<HighestObjectController>();
        if (heightTarget != null)
        {
            float topY = GetComponent<Collider>().bounds.max.y;
            heightTarget.Move(topY);
        }
    }
}