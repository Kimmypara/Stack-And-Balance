using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Settings")]
    public Light sun;
    [Tooltip("How far below the horizon the sun must be to trigger night")]
    public float nightThreshold = 0.1f; // Lower = later night

    [Header("Lamp Settings")]
    public string lampTag = "StreetLamp";
    private GameObject[] streetLamps;

    [Header("Night Effects")]
    public GameObject stars; // Assign a particle system or skybox object

    void Start()
    {
        // Find all lamps tagged in the scene
        streetLamps = GameObject.FindGameObjectsWithTag(lampTag);

        if (stars != null)
            stars.SetActive(false); // Ensure stars start off
    }

    void Update()
    {
        float dot = Vector3.Dot(sun.transform.forward, Vector3.down);

        bool isNight = dot < nightThreshold;

        SetLamps(isNight);
        ToggleStars(isNight);
    }

    void SetLamps(bool on)
    {
        foreach (GameObject lamp in streetLamps)
        {
            Light light = lamp.GetComponentInChildren<Light>();
            if (light != null)
                light.enabled = on;
        }
    }

    void ToggleStars(bool show)
    {
        if (stars != null)
            stars.SetActive(show);
    }
}