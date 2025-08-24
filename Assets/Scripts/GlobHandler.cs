using System.Collections;
using UnityEngine;

public class GlobHandler : MonoBehaviour
{
    public Transform target;
    public Vector2 dir;
    public Quaternion targetRotation;
    public float speed = 10;
    public GameObject deathEffect;
    public GameObject player;

    public bool moving = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)dir, speed * Time.deltaTime);

    }

    void Awake()
    {
        target = GameObject.Find("Player").transform;
        player = GameObject.Find("Player");

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (collision.gameObject == player)
        {
            Debug.Log("Goober");
            // Call player's takeDamage method
            PlayerDamageController playerDamageController = player.GetComponent<PlayerDamageController>();
            if (playerDamageController != null)
            {
                playerDamageController.takeDamage(20); // Example damage value
                StartCoroutine(Die());
            }
        }

    }
    IEnumerator Die()
    {
        moving = false;
        deathEffect.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    } 


}
