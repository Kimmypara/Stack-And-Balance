using UnityEngine;

public class FloorLanded : MonoBehaviour
{
    private bool hasLanded = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasLanded)
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
            float topY = GetComponent<Collider>().bounds.max.y + 2;
            heightTarget.Move(topY);
        }
    }
}