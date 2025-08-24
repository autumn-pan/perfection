using Unity.VisualScripting;
using UnityEngine;

public class PressureBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public float width = 250;
    public float height = 45;
    public PressureHandler handler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float w = (handler.pressure / handler.maxPressure) * width;
        rectTransform.sizeDelta = new Vector2(w, height);
    }
}
