using System.Collections;
using UnityEngine;

public class FloorFeedback : MonoBehaviour
{
    public GameObject rawImage;
    public Animator pete;

    public void ShowMessage()
    {
        Debug.Log("poop");
        StartCoroutine(HideAfterSeconds());
    }
    
    IEnumerator HideAfterSeconds()
    {
        rawImage.SetActive(true);
        pete.SetTrigger("Show");
        yield return new WaitForSeconds(3f);
        rawImage.SetActive(false);
    }
    
}
