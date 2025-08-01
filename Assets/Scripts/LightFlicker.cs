using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light flickerLight;
    public float minIntensity = 0.3f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        flickerLight = GetComponent<Light>();
        InvokeRepeating(nameof(Flicker), 0f, flickerSpeed);
    }

    void Flicker()
    {
        flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
    }
}
