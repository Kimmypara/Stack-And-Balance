using UnityEngine;
using UnityEngine.UI;
public class ChangeSound : MonoBehaviour
{
    private Sprite SoundOnImage;
    public Sprite SoundOffImage;
    public Button button;
    public bool isOn = true;
    
    public AudioSource audioSource;

    void Start()
    {
        SoundOnImage = button.image.sprite;
    }

    public void buttonClick()
    {
        if (isOn)
        {
            button.image.sprite = SoundOffImage;
            isOn = false;
            audioSource.mute = true;
        }
        else
        {
            button.image.sprite = SoundOnImage;
            isOn = true;
            audioSource.mute = false;
        }
    }
}
