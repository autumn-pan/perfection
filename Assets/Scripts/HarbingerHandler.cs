using System;
using UnityEngine;

public class HarbingerHandler : MonoBehaviour
{
    public float range = 1;
    public float angle = (float)(Math.PI) / 2;
    public float speed = 0.05f;
    public Transform root;

    public float cooldown = 1;
    public float maxCooldown = 1;
    public GameObject glob;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            cooldown = maxCooldown;
            Instantiate(glob, transform.position, transform.rotation);
        }
        transform.position = new Vector2(transform.position.x, root.position.y + range * Mathf.Sin(Mathf.Rad2Deg * angle));
        angle += speed * Time.deltaTime;
    }
}
