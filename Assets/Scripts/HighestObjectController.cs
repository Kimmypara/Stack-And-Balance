using UnityEngine;

public class HighestObjectController : MonoBehaviour
{
    public float verticalOffset = 0f;

    public void Move(float height)
    {
        float targetY = height + verticalOffset;
        if (transform.position.y < targetY)
        {
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        }
    }
}