using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public PressureHandler pressureHandler;
    public float health = 100;
    public float maxHealth = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Passive heal
        health += Time.deltaTime * 0.3f;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        pressureHandler.AddPressure(damage * 0.05f);
        Debug.Log("Health: " + health);
    }
}
