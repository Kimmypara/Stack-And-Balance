using UnityEngine;

public class HighestObjectController : MonoBehaviour
{
    public void Move(float height)
    {
        if (transform.position.y < height)
        {
            Vector3 newPos = new Vector3(transform.position.x, height, transform.position.z);
            transform.position = newPos;
        }
    }
}