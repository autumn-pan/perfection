using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class VignetteHandler : MonoBehaviour
{
    public PostProcessVolume volume;
    public Vignette vignette;
    public float vignetteIntensity = 0.7f;
    public float redIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume.enabled = true;
        volume.profile.TryGetSettings(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity = new UnityEngine.Rendering.PostProcessing.FloatParameter { value = vignetteIntensity };
    }
    
}
