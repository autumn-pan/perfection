using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public float width = 250;
    public float height = 45;
    public PlayerDamageController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float w = (player.health / player.maxHealth) * width;
        rectTransform.sizeDelta = new Vector2(w, height);
    }
}
