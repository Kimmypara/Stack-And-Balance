using UnityEngine;

public class HighestObjectController : MonoBehaviour
{
    public void Move(float height)
    {
        if (transform.position.y < height)
        {
            transform.position = Vector3.up * height;
        }
    }
}
