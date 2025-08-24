using UnityEngine;

public class PlayerRespawnHandler : MonoBehaviour
{
    public Transform respawnLocation;
    public PlayerDamageController controller;
    public PressureHandler pressureHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.health <= 0)
        {
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        pressureHandler.pressure = pressureHandler.lowestPressure;
        controller.health = 100;
        transform.position = respawnLocation.position;
    }
}
