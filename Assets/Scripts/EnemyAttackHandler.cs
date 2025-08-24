using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // The player
    public GameObject player;
    // The amount of damage to deal
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Awake()
    {
        player = GameObject.Find("Player");
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // Call player's takeDamage method
            PlayerDamageController playerDamageController = player.GetComponent<PlayerDamageController>();
            if (playerDamageController != null)
            {
                playerDamageController.takeDamage(damage); // Example damage value
            }
        }
    }
}
