using UnityEngine;

public class ThumbsUpController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAndDisappear()
    {
        if (animator != null)
        {
            animator.SetTrigger("Show");
            Invoke(nameof(DestroySelf), 3f); // Adjust to match animation length
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}